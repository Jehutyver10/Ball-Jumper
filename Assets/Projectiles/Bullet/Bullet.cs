using UnityEngine;
using System.Collections;

public class Bullet : Projectile {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(shooter.transform.forward * speed, ForceMode.Impulse);
	}
	
	// Update is called once per frame

}
