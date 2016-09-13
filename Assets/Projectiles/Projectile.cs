using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {
	public float speed = 1000, damage = 1;	// Use this for initialization
	public bool isChargeAttack;//

	private GameObject shooter;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.GetComponent<Health>()){
			col.GetComponent<Health>().TakeDamage(damage);
			Destroy(gameObject);
		}
	}

	public void SetShooter(GameObject owner){
		shooter = owner;
		if(GetComponent<Collider>()){
			Physics.IgnoreCollision(GetComponent<Collider>(), shooter.GetComponentInChildren<Collider>());
		} else if(GetComponentInChildren<Collider>()){
			Physics.IgnoreCollision(GetComponentInChildren<Collider>(), shooter.GetComponentInChildren<Collider>());

		}
	}
}
