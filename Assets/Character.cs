using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [HideInInspector]
    Rigidbody rb;
    CharacterController charChon;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move()
    {

    
    }

    private void OnAnimatorMove()
    {
            
    }

}
