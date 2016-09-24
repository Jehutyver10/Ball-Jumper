using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float damage = 100, comboDamage = 150,dashDamage = 200,  burstDamage = 500, force;
	public bool active = false, knockback = false, isColliding = false;
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
	public void ActivateOrDeactivate(){
		active = !active;
	}

	void OnTriggerEnter(Collider col){
		if(isColliding){
			return;
		}
		isColliding = true;

		if(active){
			if(col.GetComponentInParent<Health>()){
				col.GetComponentInParent<Health>().TakeDamage(damage);
			}
			if(col.GetComponent<EnemyWeapon>()){
				if(col.GetComponent<EnemyWeapon>().isActive){
					SendMessageUpwards("OnWeaponsClash");
				}
			}
			if(col.GetComponentInParent<Rigidbody>() && knockback){//checks if this attack should and can knock the target back
				col.GetComponentInParent<Rigidbody>().AddForce(transform.root.transform.forward * force, 
				ForceMode.Impulse);
			}
		}
	}
}
