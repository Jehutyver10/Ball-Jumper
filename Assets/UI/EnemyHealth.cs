using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : HealthText {
	PlayerController player;
	// Use this for initialization
	void Awake () {
		player = GameObject.FindObjectOfType<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () {
		if (player.target) {
			if(player.target.GetComponent<Enemy>()){
				GetComponent<Text>().text = associatedObjectName + ": " + player.target.GetComponent<Health>().health;
			}
		} else {
			GetComponent<Text> ().text = null;
		}
	}
}
