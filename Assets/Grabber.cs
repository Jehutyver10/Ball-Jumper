using UnityEngine;
using System.Collections;

public class Grabber : MonoBehaviour {
	GameObject grabbedObject;
	//this should be attached to the hand that grabs objects

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if(col.GetComponentInParent<Grabbable>()){
			GetComponentInParent<PlayerController>().Grab(col.gameObject);
		}
	}
}
