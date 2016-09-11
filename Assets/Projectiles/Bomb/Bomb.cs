using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Projectile))]
public class Bomb : MonoBehaviour {
	float initialDamage;
	Vector3 forward;
	PlayerController player;
	bool canFire = false, launched = false;
	Animator anim;
	Projectile projectile;
	// Use this for initialization

	void Awake(){
		transform.parent = GameObject.Find("Projectiles").transform;
		launched = false;
	}
	void Start () {
		projectile = GetComponent<Projectile>();
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
			launched = true;
	}

	void OnCollisionEnter(Collision col){
		player.canBomb = true;
		if(launched){
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter(Collider col){
		player.canBomb = true;
	}
	public void SetDamage(float damage){
		projectile.damage = damage;
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
