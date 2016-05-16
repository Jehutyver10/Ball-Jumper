using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider collider){
		if(collider.GetComponent<Projectile>()){
			if(collider.GetComponent<Bomb>()){
				collider.GetComponent<Bomb>().collided = true;
			}else{
				Destroy(collider.gameObject);
			}
		}
	}
}
