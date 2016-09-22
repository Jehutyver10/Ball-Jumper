using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {
	public float speed = 1000, damage = 1;	// Use this for initialization
	public bool isChargeAttack;//

	public GameObject shooter;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if(col.GetComponentInParent<Health>()){
			col.GetComponentInParent<Health>().TakeDamage(damage);
		}
		Destroy(this.gameObject);
	}

	public void SetShooter(GameObject owner){
		shooter = owner;
		foreach(Collider col in shooter.transform.root.GetComponentsInChildren<Collider>()){
			Physics.IgnoreCollision(GetComponentInChildren<Collider>(), col);
		}
	}
}
