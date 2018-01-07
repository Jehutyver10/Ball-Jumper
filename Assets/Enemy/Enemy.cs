using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
[RequireComponent (typeof (Health))]
public class Enemy : LockableTarget {
	private float probability;

	public bool canBeHomedInOn, active = false, alive = false, playerDashed = false;
	public float shotsPerSecond, meleeLimit, detectionRange, speed, smoothing = 1; 
	public GameObject laser;
	public EnemyWeapon weapon;
	public PseudoEnemy pseudo;
	public List<string> PossiblePursuingCommands;

	Animator anim;
	Health health;



	public GameObject target;
	// Use this for initialization

	void Awake(){
		PossiblePursuingCommands = new List<string> () { "Dash", "Strafe", "Attack" };
		SetTarget();
	}
	void Start () {
		anim = GetComponent<Animator>();
		health = GetComponent<Health>();
		weapon = GetComponentInChildren<EnemyWeapon>();
		pseudo = GetComponentInChildren<PseudoEnemy>();
		StartCoroutine ("Idle");
	}

	// Update is called once per frame
	void Update(){
		probability = Time.deltaTime * shotsPerSecond;
		if(alive){

			transform.LookAt(target.GetComponent<PlayerController>().pseudo.transform);
		
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


		}

		//TODO remove this in final version; it's only for testing
		if(Input.GetButtonDown("Activate Enemies")){
			alive = !alive;
			anim.SetBool("Activated", !anim.GetBool("Activated"));
		}
	}

	IEnumerator Idle () { //business as usual until the target appears within range
		while(Vector3.Distance(transform.position, target.transform.position) > detectionRange)
		{
			active = false;
			//TODO put some patrol code here or something? 
			yield return null;
		}
		active = true;
		StartCoroutine ("Pursue");
	}

	IEnumerator Pursue () {//once detected);
		while (active && Vector3.Distance( transform.position, target.transform.position) > meleeLimit) {//if outside the melee limit but activated
			if (Vector3.Distance (transform.position, target.transform.position) < detectionRange) {//if not yet approached
				transform.position = Vector3.Lerp (transform.position, target.transform.position, smoothing * Time.deltaTime);
				transform.LookAt (target.transform.position);
				anim.ResetTrigger ("Melee Attack");
				if (Random.value < probability) {
					Shoot ();
				}
				yield return null;
			} else { 
				StartCoroutine ("Idle");
				yield break;
			}
		}
		StartCoroutine ("MeleeAttack");
	}

	IEnumerator MeleeAttack(){
		while (active && Vector3.Distance (transform.position, target.transform.position) < meleeLimit) {
			anim.SetTrigger ("Melee Attack");
			yield return null;
		}
		StartCoroutine ("Pursue");

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

}
