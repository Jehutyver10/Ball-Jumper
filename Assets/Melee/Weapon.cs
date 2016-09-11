using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float damage = 100;
	public float comboDamage = 150;
	public float dashDamage = 200;
	public float burstDamage = 500;
	public bool active = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Activate(){
		active = true;
	}
	public void Deactivate(){
		active = false;
	}

	void OnTriggerEnter(Collider col){
		if(active){
			if(col.GetComponent<Health>()){
				col.GetComponent<Health>().TakeDamage(damage);
			}
		}
	}
}
