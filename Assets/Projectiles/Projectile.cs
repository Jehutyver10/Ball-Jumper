using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float speed = 1000, damage = 1;	// Use this for initialization
	public bool isChargeAttack, isColliding;

	public GameObject shooter;
	void Start () {
		foreach(Collider col in FindObjectsOfType<CharacterController>()){
			Physics.IgnoreCollision(GetComponent<Collider>(), col);
		}
	}
	
	// Update is called once per frame
	void Update () {
		isColliding = false;
		
	}

	void OnTriggerEnter(Collider col){
		if(isColliding){
			return;
		}
		isColliding = true;
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
