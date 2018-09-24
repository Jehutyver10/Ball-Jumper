using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
[RequireComponent (typeof (Health))]
public class Enemy : LockableTarget {
	private float probability;

	public bool canBeHomedInOn, active = false, alive = false, playerDashed = false, stunned = false;
	public float shotsPerSecond, meleeLimit, detectionRange, shotSpeed, normalSpeed = 1, dashSpeed, rushSpeed, actionPeriod, minDistance; 
	public GameObject laser;
	public EnemyWeapon weapon;
	public PseudoEnemy pseudo;
	public List<string> PossibleMovingCommands;
	bool nextActionDecided = false;
	public enum State {Idle, Stunned, Move, Rush, Dash, Strafe, Attack, Block};
	public enum AttackType{Combo, Dash, Burst};
	AttackType attackType = AttackType.Combo;
	public State currentState = State.Idle;
	Animator anim;
	Health health;
	CharacterController cc;

	private bool readyForNextState = true, CR_active = false;
	public Vector3 targetPosition;

	public GameObject target;
	// Use this for initialization

	void Awake(){
		PossibleMovingCommands = new List<string> () { "Dash", "Strafe", "Attack"};
		SetTarget();
		cc = GetComponent<CharacterController> ();
	}
	void Start () {
		anim = GetComponent<Animator>();
		health = GetComponent<Health>();
		weapon = GetComponentInChildren<EnemyWeapon>();
		pseudo = GetComponentInChildren<PseudoEnemy>();
        GameManager.main.Enemies.Add(this);
		//StartCoroutine ("Idle");
	}

	// Update is called once per frame
	void Update(){
		probability = Time.deltaTime * shotsPerSecond;
		if (alive) {
			print ("check 1");

			transform.LookAt (new Vector3 (target.GetComponent<PlayerController>().pseudo.transform.position.x,
				target.GetComponent<PlayerController> ().pseudo.transform.position.y, 
				target.GetComponent<PlayerController> ().pseudo.transform.position.z));
			
		
//			if(active){
//				if(Vector3.Distance(target.transform.position, transform.position) > meleeLimit){ //check if outside melee limit
//					anim.ResetTrigger("Melee Attack");
//					if(Random.value < probability){
//						Shoot();
//					}
//				}else{
//					anim.SetTrigger("Melee Attack");
//				}
//			}
		
			if (WithinMidrange ()) {
				if (WithinMeleeRange()) {
					
					if (readyForNextState) {
						currentState = (State)System.Enum.Parse (typeof(State), PossibleMovingCommands [Random.Range (0, 3)]);
						readyForNextState = false;
						print ("ready for next state");
					}
				} else {
					currentState = State.Move;
				}
			} else {
				currentState = State.Rush;
			}

		}
		//TODO remove this in final version; it's only for testing
		if(Input.GetButtonDown("Activate Enemies")){
			alive = !alive;
			anim.SetBool("Activated", !anim.GetBool("Activated"));
		}
	}

	void FixedUpdate(){
		if (alive) {

			if (!CR_active) {
				ActOnState ();
			}
		}

	}

//	void Move(){
//		cc.Move (transform.forward * currentSpeed);
//		print ("Moving");
//		readyForNextState = true;
//
//	}

	IEnumerator Move(){
		CR_active = true;
		float timer = 0;
		float moveTime = 3;
		while (timer <moveTime) {
			if (Vector3.Distance (transform.position, target.transform.position) > minDistance) {
				cc.Move (transform.forward * normalSpeed);
			}
			print ("moving");
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		CR_active = false;
		Reset ();
	}
	IEnumerator Rush(Vector3 destination){
		CR_active = true;
		while (Vector3.Distance (transform.position, destination) > 1) {
			print ("rushing");
			if (Vector3.Distance (transform.position, target.transform.position) > minDistance) {
				cc.Move ((destination - transform.position).normalized * rushSpeed);
			} else {
				break;
			}
			yield return new WaitForFixedUpdate();
		}
		CR_active = false;
		Reset ();
	}

	IEnumerator Dash(){
		CR_active = true;
		float timer = 0;
		float dashTime = 1;
		while (timer < dashTime) {
			if (Vector3.Distance (transform.position, target.transform.position) > minDistance) {
				cc.Move (transform.forward * dashSpeed);
			}
			print ("dashing");
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		CR_active = false;
		Reset ();
	}

	IEnumerator Strafe(){
		CR_active = true;
		float timer = 0;
		float strafeTime = 2;
		while (timer < strafeTime) {
			cc.Move (transform.right * normalSpeed/4);
			print ("strafing");
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate ();
		}
		timer = 0;
		while (timer < strafeTime) {
			cc.Move (transform.right * -1 * normalSpeed/4);
			print ("strafing");
			timer += Time.deltaTime;
			yield return new WaitForFixedUpdate ();
		}
		CR_active = false;
		Reset ();
	}

	IEnumerator Attack(){
		CR_active = true;
		float attackDistance = 1;
		int swings = 0;
		while (swings < 4) {
			print ("attacking");
			if (Vector3.Distance (targetPosition, transform.position) > attackDistance) {
				cc.Move ((targetPosition - transform.position).normalized * normalSpeed); 
			}
			anim.SetTrigger ("Melee Attack");
			swings = weapon.noDamageSwingCount;
			yield return null;
		}
		CR_active = false;
		Reset ();

	}
	public void Reset(){
		StopAllCoroutines ();
		currentState = State.Idle;
		readyForNextState = true;
		anim.ResetTrigger ("Melee Attack");
		CR_active = false;
	}
	void ActOnState(){
		switch(currentState){
		case State.Stunned:
			CR_active = true;
			print ("stunned");
			break;
		case State.Move:
			StartCoroutine (Move());

			break;
		case State.Rush:
			if (!CR_active) {
				StartCoroutine (Rush (target.GetComponent<PlayerController> ().pseudo.transform.position));
			}
			break;
		case State.Dash:
			if (!CR_active) {
				StartCoroutine (Dash ());
			}
			break;
		case State.Strafe:
			if (!CR_active) {
				StartCoroutine (Strafe());
			}

			break;
		case State.Attack:
			if (!CR_active) {
				StartCoroutine (Attack ());
			}
			break;
		case State.Block:
			print ("blocking");
			break;
		default:
			print ("idle");
			break;

		}
	}



	bool WithinMidrange(){
		if (Vector3.Distance (transform.position, target.transform.position) > detectionRange) {
			return false;
		} else {
			return true;
		}
	}
	bool WithinMeleeRange(){
		if (Vector3.Distance (transform.position, target.transform.position) > meleeLimit) {
			return false;
		} else {
			return true;
		}
	}



	public void Hit(bool hit){
		
	}

	void SetTarget(){
		target = GameObject.FindObjectOfType<PlayerController>().gameObject;
	}

	void OnWeaponsClash(){
		if(weapon.damage == weapon.comboDamage){
			anim.SetTrigger("Clash");
			print("Clashing");
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.GetComponent<Environment>()){
			health.TakeDamage(col.impulse.magnitude * 2);
		}
	}
	void Shoot(){
		GameObject shot = Instantiate(laser, transform.position + transform.forward, Quaternion.identity) as GameObject;
		shot.transform.parent = this.transform;
		shot.GetComponent<Projectile>().SetShooter(this.gameObject);
		//Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		//shot.transform.rotation = q * shot.transform.rotation;
		shot.GetComponent<Rigidbody>().AddForce(transform.forward * shot.GetComponent<Projectile>().speed);

	}

	//
	//	IEnumerator Idle () { //business as usual until the target appears within range
	//		while(Vector3.Distance(transform.position, target.transform.position) > detectionRange)
	//		{
	//			active = false;
	//			//TODO put some patrol code here or something? 
	//			yield return null;
	//		}
	//		active = true;
	//		StartCoroutine ("Pursue");
	//	}
	//
	//	IEnumerator Pursue () {//once detected);
	//		while (active && !WithinMeleeRange()) {//if outside the melee limit but activated
	//			if (Vector3.Distance (transform.position, target.transform.position) < detectionRange) {//if not yet approached
	//
	////				if (Random.value < probability) {
	////					Shoot ();
	////				}
	//				yield return null;
	//			} else { 
	//				StartCoroutine ("Idle");
	//				yield break;
	//			}
	//		}
	//		StartCoroutine ("MeleeAttack");
	//	}
	//
	//	IEnumerator MeleeAttack(){
	//		while (active && Vector3.Distance (transform.position, target.transform.position) < meleeLimit) {
	//			yield return null;
	//		}
	//		StartCoroutine ("Pursue");
	//	}

}
