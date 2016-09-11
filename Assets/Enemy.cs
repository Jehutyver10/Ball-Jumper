using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public bool canBeHomedInOn;
	// Use this for initialization
	void Start () {
	
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
