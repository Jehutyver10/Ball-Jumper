using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	Vector3 forward;
	PlayerController player;
	bool canFire = false;
	Animator anim;
	// Use this for initialization

	void Awake(){
		transform.parent = GameObject.Find("Projectiles").transform;
	}
	void Start () {
		anim = GetComponent<Animator>();
		anim.applyRootMotion = false;
		player = GameObject.FindObjectOfType<PlayerController>();
		forward = player.transform.forward;
		anim.SetTrigger("Make Bomb");

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Boost") == 0){
			anim.SetTrigger("Fire Bomb");
		}
	
		if(canFire){
			Shoot();
		}
	}

	void Fire(){
			player.anim.SetTrigger("Bomb Launched");
			player.makingBomb = false;
			anim.applyRootMotion = true;
			canFire = true;
	}

	void OnCollisionEnter(Collision col){
		player.canBomb = true;
	}
	void OnTriggerEnter(Collider col){
		player.canBomb = true;
	}
	void Shoot(){
		if(player.target){
			transform.LookAt(player.target.transform, Vector3.up);
			GetComponent<Rigidbody>().AddForce(transform.forward * GetComponent<Projectile>().speed);

		}else{
			GetComponent<Rigidbody>().AddForce(forward * GetComponent<Projectile>().speed);
		}
		//Quaternion q = Quaternion.FromToRotation(Vector3.up, forward);
		//transform.rotation = q * transform.rotation;
	}
}
