using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockoutPunch : Projectile {
	public float knockback = 1; 
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(shooter.transform.forward * speed, ForceMode.Impulse);

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col){
		if(isColliding){
			return;
		}
		isColliding = true;
		if(col.GetComponentInParent<Health>()){
			col.GetComponentInParent<Health>().TakeDamage(damage);
		}
		if (col.GetComponentInParent<Rigidbody>()) {
			col.GetComponentInParent<Rigidbody> ().AddForce (col.transform.forward * -1 * knockback, ForceMode.Impulse);
		}
		Destroy(this.gameObject);
	}

	void OnCollisionEnter(Collision col){
		Destroy (gameObject);
	}
}
