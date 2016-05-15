using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {


	void OnTriggerEnter(Collider col)
	{
		if(col.GetComponent<PlayerController>()){
			Debug.Log ("You win!");
			Destroy (gameObject);
		}
	}

}
