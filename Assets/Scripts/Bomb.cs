using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Projectile))]
public class Bomb : MonoBehaviour {
	// Use this for initialization
	void Awake () {
		Physics.IgnoreCollision(FindObjectOfType<PlayerController>().GetComponent<Collider>(), 
		GetComponent<Collider>()); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
