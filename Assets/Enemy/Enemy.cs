using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Health))]
public class Enemy : LockableTarget {
	public bool canBeHomedInOn;
	Health health;
	// Use this for initialization
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
	void Update () {
	
	}
}
