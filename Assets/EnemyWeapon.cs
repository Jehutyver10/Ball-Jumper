using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {
	public bool active = false, knockback = false;
	public float damage = 0, comboDamage = 0, burstDamage = 0, force = 0;
	public int shieldCounter;
	// Use this for initialization
	void Start () {
		shieldCounter = 0;
		//find all the weapons that the enemy has
//		for(int i = 0; i < transform.root.GetComponentsInChildren<EnemyWeapon>().Length; i++){
//			transform.root.GetComponentsInChildren<EnemyWeapon>()[i].name = transform.root.GetComponentsInChildren<EnemyWeapon>()[i].name + " " + i.ToString(); 
//		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col){
//		if(isColliding){
//			return;
//		}
//		isColliding = true;

		if(active){
//			if(col.transform.root.GetComponentInChildren<Weapon>()){
//				print("Getting here");
//
//				if(col.transform.root.GetComponent<Animator>().GetBool("Can Clash")){
//					SendMessageUpwards("OnWeaponsClash");
//
//				}
//			}
			if(col.GetComponentInParent<PlayerController>() && active){//avoid doing damage when weapon clashing
				if(col.GetComponentInParent<PlayerController>().shielding){
					shieldCounter += 1;
					print(shieldCounter);
				}else{
					col.GetComponentInParent<Health>().TakeDamage(damage);
					shieldCounter = 0;
				}
				active = false;
			}
		
			if(col.GetComponentInParent<Rigidbody>() && knockback){//checks if this attack should and can knock the target back
				col.GetComponentInParent<Rigidbody>().AddForce(transform.root.transform.forward * force, ForceMode.Impulse);
					print("knocking back");
			}
		}
	}

}
