using UnityEngine;
using System.Collections;

public class EnemyLaser : Projectile {

	// Use this for initialization
	void Start () {

	}

	void OnTriggerEnter(Collider col){
		if(isColliding){
			return;
		}
		isColliding = true;
		if(col.GetComponentInParent<Health>() && col.GetComponentInParent<PlayerController>()){
			if(!col.GetComponentInParent<PlayerController>().shielding){
				col.GetComponentInParent<Health>().TakeDamage(damage);
			}else{
				print("Hit shield.");
			}
		}
		if(col.gameObject != shooter.gameObject && !col.GetComponent<Shredder>()){
			
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
	}

	public new void SetShooter(GameObject owner){
		shooter = owner;
		if(GetComponent<Collider>()){
			Physics.IgnoreCollision(GetComponent<Collider>(), shooter.GetComponent<Collider>());
		} else if(GetComponentInChildren<Collider>()){
			Physics.IgnoreCollision(GetComponent<Collider>(), shooter.GetComponent<Collider>());
		}
	}
}
