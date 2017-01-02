using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : HealthText {

	// Use this for initialization
	void Awake () {
		text = GetComponent<Text> ();
		health = GameObject.FindObjectOfType<PlayerController> ().GetComponent<Health> ();
		text.text = associatedObjectName + ": " + health.health;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
