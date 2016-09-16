using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Health))]
public class Enemy : LockableTarget {
	public bool canBeHomedInOn;
	public float shotsPerSecond;
	public GameObject laser;
	Health health;

	private GameObject target;
	// Use this for initialization

	void Awake(){
		SetTarget();
	}
	void Start () {
		health = GetComponent<Health>();
	}

	void OnBecameInvisible() {
       	canBeHomedInOn = false;
    }
    void OnBecameVisible() {
       	canBeHomedInOn = true;
    }
	// Update is called once per frame
	void Update(){
		transform.LookAt(target.transform);
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			Shoot();
		}
	}

	void SetTarget(){
		target = GameObject.FindObjectOfType<PlayerController>().gameObject;
	}
	void Shoot(){
		GameObject shot = Instantiate(laser, transform.position + transform.forward, Quaternion.identity) as GameObject;
		shot.GetComponent<Projectile>().SetShooter(this.gameObject);
		Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		shot.transform.rotation = q * shot.transform.rotation;
		shot.GetComponent<Rigidbody>().AddForce(transform.forward * shot.GetComponent<Projectile>().speed);

	}
}
