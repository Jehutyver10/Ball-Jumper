using UnityEngine;
using System.Collections;

public class Bomb : Projectile {
	float initialDamage;
	Vector3 forward;
	PlayerController player;
	bool canFire = false, launched = false;
	Animator anim;
	Projectile projectile;
	public float maxDamage = 1000;
	// Use this for initialization

	void Awake(){
		transform.parent = GameObject.Find("Projectiles").transform;
		launched = false;
		damage = 300;
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
		if (player.target) {
			transform.position = Vector3.MoveTowards (transform.position, player.target.transform.position, speed * Time.deltaTime);
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
			transform.parent = null;
			player.canBomb = true;
	
	}

	void OnCollisionEnter(Collision col){
		player.canBomb = true;
		if(launched){
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter(Collider col){
		if(isColliding){
				return;
			}
		if(launched){

			isColliding = true;
			if(col.GetComponentInParent<Health>()){
				col.GetComponentInParent<Health>().TakeDamage(damage);
			}
			Destroy(gameObject);	
		}

	}

	public void SetDamage(float damage){
		projectile.damage = damage;
	}
	void Shoot(){
		if(player.target){
			transform.LookAt(player.target.transform, Vector3.up);
			GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);

		}else{
			GetComponent<Rigidbody>().AddForce(forward * speed, ForceMode.VelocityChange);
		}
		//Quaternion q = Quaternion.FromToRotation(Vector3.up, forward);
		//transform.rotation = q * transform.rotation;
	}
}
