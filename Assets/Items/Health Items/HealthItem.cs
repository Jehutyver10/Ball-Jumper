using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : LockableTarget{
	public float healValue = 500;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider col){
		if (col.GetComponentInParent<PlayerController> ()) {
			col.GetComponentInParent<Health> ().RestoreHealth (healValue);
			Destroy (gameObject);

		}

	}
}
