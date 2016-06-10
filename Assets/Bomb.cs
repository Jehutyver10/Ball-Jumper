using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Fire(){
		Debug.Log("Fire");
		Vector3 forward = GameObject.FindObjectOfType<PlayerController>().transform.forward;
		Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		transform.rotation = q * transform.rotation;
		GetComponent<Rigidbody>().AddForce(forward * GetComponent<Projectile>().speed, ForceMode.Impulse);
	}
}

