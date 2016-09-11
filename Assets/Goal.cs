using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {


	void OnTriggerEnter(Collider col)
	{
		if(col.GetComponent<PlayerController>()){
			Debug.Log ("You win!");
			Destroy (gameObject);
		}
		//TODO: make it so the sword only destroys things while attacking
//		if(col.gameObject.GetComponent<Projectile>()||col.gameObject.GetComponent<Weapon>()){
//			//print ("Destroyed " + name + " by " + col.gameObject.name);
//			//Destroy(gameObject);
//		}

	}

}
