using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerController: MonoBehaviour {

	private GameObject target;
	private bool isLocked;
	private Rigidbody rb;
	private CameraFollow cam;
	private Animator anim;
	private float boostSpeed, normalSpeed, checkLock, lockOffTime,
	newLockTime; 
	private int targeter;
	public GameObject bullet;

	public float speed, newLockLimit = 1, rotationSpeed =1;
	public float boostMultiplier;
	public float strength;
//	private bool jumped = false;
	public float newY, lockLimit = 2;
	public bool isBoosted, canShoot;

	void Start (){
		anim = GetComponent<Animator>();
		targeter = 0;
		lockOffTime = Time.time;
		newLockTime = Time.time;
		checkLock = Mathf.Round(Input.GetAxis("LockOn"));
		isLocked = false;
		target = null;

		normalSpeed = speed;
		boostSpeed = speed * boostMultiplier;
		rb = GetComponent<Rigidbody> ();
		cam = FindObjectOfType<CameraFollow>();
		newY = 0;
	}

//	void OnTriggerEnter (Collider other){
//		jumped = false;
//}


	void FixedUpdate () {
		Move();
		LockOn();
		cam.AdjustDamping();
		Shoot();

	}


	void Move(){
		MoveHorizontal();
		MoveVertical();
		RightStick();
		Boost();
	}

	void MoveHorizontal(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical") ;
		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical) ;
		rb.AddRelativeForce (movement*speed);

	}
	void RightStick(){
		if(!isLocked){
			float rotateHorizontal = Input.GetAxis("Right Horizontal");
			transform.RotateAround (transform.localPosition, Vector3.up, rotateHorizontal * rotationSpeed);			
		}else{
			if(Input.GetAxis("Right Horizontal") != 0){
				float checkNewLockTime = Time.time;
				if(checkNewLockTime - newLockTime > newLockLimit){
					targeter += (int) Mathf.Round(Input.GetAxis("Right Horizontal"));
					HandleLock();
			}

			}
		}
		newY = transform.rotation.eulerAngles.y;

	}

	void Boost(){
		float boostCheck = Mathf.Round(Input.GetAxisRaw("Boost"));
		if (boostCheck > 0){
			if(!isBoosted){
				cam.anim.SetTrigger("Boost");
				isBoosted = true;
			}
			speed = boostSpeed;
		} else {
			isBoosted = false;
			speed = normalSpeed;
		}
	}

	void MoveVertical(){
		transform.rotation = Quaternion.Euler (0, newY, 0);
		if (Input.GetButton("Jump")) {// determines if ball is in midair
			rb.AddForce (0f, Input.GetAxis("Jump")* strength, 0, ForceMode.Impulse);
		}
		else if(Input.GetButton("Descend")) {
			rb.AddForce(0f,  -Input.GetAxis("Descend")* strength, 0, ForceMode.Impulse); 
		}

	}

	void LockOn(){
		if(CanLockOn()){
			if(Mathf.Round(Input.GetAxis("LockOn")) > 0){
				HandleLock();
			}
		} else{
			if(Mathf.Round(Input.GetAxis("LockOn")) > 0 && isLocked){ //holding down button while locked on
				
				if(canLockOff()){
					lockOffTime = Time.time;
					target = null;
					isLocked = false;
					cam.SlowDamping();
				}
			}else{
				lockOffTime = Time.time;
			}
		}
		if (isLocked == true) {
			if(target){
				transform.LookAt (target.transform);
			}
			else{
				isLocked = false;
				HandleLock();
			}
		}
	}

	private bool CanLockOn(){
		float lockOn =  Mathf.Round(Input.GetAxis("LockOn"));
		if(lockOn != checkLock){
			checkLock = Mathf.Round(Input.GetAxis("LockOn"));
			return true;
		}return false;

	}
	void HandleLock(){
		GameObject[] targets = Targets(GameObject.FindGameObjectsWithTag("Lockable"));
		if (isLocked == false) {//to lock on in the beginning
			if(targets.Length >0){
				cam.SlowDamping();
				target = GameObject.FindGameObjectWithTag("Lockable");
				isLocked = true;
			}
		} else {// to switch targets;
			if(targets.Length > 1){
	
				if(Mathf.Round(Input.GetAxis("LockOn")) > 0){
					cam.SlowDamping();
					targeter = 0;
					if(target != targets[0]){
						target = targets[0];
					}else{
						target = targets[1];
					}
				}else if(Mathf.Round(Input.GetAxis("Right Horizontal")) != 0){
					cam.SlowDamping();
					if(targeter < 0){//index out of range
						targeter = targets.Count() -1;
					}
					target = targets[targeter % targets.Count()];
					newLockTime = Time.time;
				}
			}
		}
	}

	private bool canLockOff(){
		float checkTime = Time.time;
		if(Mathf.Abs(checkTime - lockOffTime) >= lockLimit){
			lockOffTime = Time.time;
			return true;
		}
			return false;
	}

	private GameObject[] Targets(GameObject[] targets){//sorts game objects in an array by distance
		List<GameObject> sortedTargets = new List<GameObject>();
		GameObject[] newArray = targets;
		for(int i = 0; i <targets.Length; i++){
			sortedTargets.Add(Target(newArray.ToList()));
			List<GameObject> tempArray = new List<GameObject>();
			for(int j = 0; j < newArray.Length; j++){
				if(sortedTargets[i] != newArray[j]){
					tempArray.Add(newArray[j]);
				}
			}
			newArray = tempArray.ToArray();
		}
		return sortedTargets.ToArray();
	}

	private GameObject Target(List<GameObject> targets){
		GameObject target = null;
		float distance = 100000000;//cheating a bit with large distance
		foreach(GameObject possibleTarget in targets){
			float possibleDistance = AngleDir.AngleDirection(transform.forward, possibleTarget.transform.position, Vector3.up);
			possibleDistance = possibleDistance * Vector3.Angle(possibleTarget.transform.position - transform.position, transform.forward);
			if(possibleDistance < distance){
				target = possibleTarget;
				distance = possibleDistance;
			}
		}
		return target;
	}

	void Shoot(){
		if(Input.GetButton("Shoot") && canShoot){
			if(!isBoosted){//stationary shot
				anim.SetTrigger("Shoot Bullet");
			}
		}
	}

	void AllowShoot(){
		canShoot = true;
	}
	void ShootBullet(){
		canShoot = false;
		Debug.Log("Shooting");
		GameObject shot;
		shot = Instantiate(bullet, transform.position + transform.forward, Quaternion.identity) as GameObject;
		shot.transform.parent = GameObject.Find("Projectiles").transform;
		Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		shot.transform.rotation = q * shot.transform.rotation;
		shot.GetComponent<Rigidbody>().AddForce(transform.forward * shot.GetComponent<Projectile>().speed);

	}
}
