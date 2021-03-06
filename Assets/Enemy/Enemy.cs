﻿using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Health))]
public class Enemy : LockableTarget {
	public bool canBeHomedInOn, alive = false, activated = false;
	public float shotsPerSecond, meleeLimit, detectionRange, speed;
	public GameObject laser;
	public EnemyWeapon weapon;
	public PseudoEnemy pseudo;

	Animator anim;
	Health health;



	public GameObject target;
	// Use this for initialization

	void Awake(){
		SetTarget();
	}
	void Start () {
		anim = GetComponent<Animator>();
		health = GetComponent<Health>();
		weapon = GetComponentInChildren<EnemyWeapon>();
		pseudo = GetComponentInChildren<PseudoEnemy>();
	}

	// Update is called once per frame
	void Update(){
		if(activated){
			transform.LookAt(target.GetComponent<PlayerController>().pseudo.transform);
			float probability = Time.deltaTime * shotsPerSecond;
			if(Mathf.Abs(Vector3.Distance(target.transform.position, transform.position)) < detectionRange){//check if player is within enemy's detection range
				alive = true;
			}else{	
				alive = false;
			}
			if(alive){
				if(Vector3.Distance(target.transform.position, transform.position) > meleeLimit){ //check if outside melee limit
					anim.ResetTrigger("Melee Attack");
					if(Random.value < probability){
						Shoot();
					}
				}else{
					anim.SetTrigger("Melee Attack");
				}
			}
		}

		//TODO remove this in final version; it's only for testing
		if(Input.GetButtonDown("Activate Enemies")){
			activated = !activated;
			anim.SetBool("Activated", !anim.GetBool("Activated"));
		}
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
