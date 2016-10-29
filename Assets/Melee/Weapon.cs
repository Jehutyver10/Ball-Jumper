using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float damage = 100, comboDamage = 150,dashDamage = 200,  burstDamage = 500, force;
	public bool active = false, knockback = false, isColliding = false, canClash = false, upswing, downswing;
	// Use this for initialization
	void Start () {
		if(GetComponentInParent<PlayerController>()){
			Physics.IgnoreCollision(GetComponentInParent<PlayerController>().GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		isColliding = false;
	
	}

	void OnTriggerEnter(Collider col){
//		if(isColliding){
//			return;
//		}
//		isColliding = true;

		if(active){
			if(col.transform.root.GetComponentInChildren<EnemyWeapon>()){
				if(col.transform.root.GetComponent<Animator>().GetBool("Can Clash")){
					SendMessageUpwards("OnWeaponsClash");
					col.SendMessageUpwards("OnWeaponsClash");
					active = false;
					col.transform.root.GetComponentInChildren<EnemyWeapon>().active = false;
					return;
				}
			}
			if(col.GetComponentInParent<Health>() && active){
				col.GetComponentInParent<Health>().TakeDamage(damage);
				active = false;
			}

			if(col.GetComponentInParent<Rigidbody>()){//checks if this attack should and can knock the target back
				if(knockback){
					if(!upswing && !downswing || upswing && downswing){
						col.GetComponentInParent<Rigidbody>().AddForce(transform.root.transform.forward * force, ForceMode.Impulse);
						print("knocking back");
					} else if(upswing && !downswing){
						col.GetComponentInParent<Rigidbody>().AddForce(transform.root.transform.up * force * 10);
						print("knocking up");
					} else if(!upswing && downswing){
						col.GetComponentInParent<Rigidbody>().AddForce(-transform.root.transform.up * force * 10);
						print("knocking down");
					}
				}
			}
		}
	}
}
