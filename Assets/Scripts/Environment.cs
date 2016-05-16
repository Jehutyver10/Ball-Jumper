using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collider){
		
		if(collider.gameObject.GetComponent<Projectile>()){
			if(collider.gameObject.GetComponent<Bomb>()){
				collider.gameObject.GetComponent<Bomb>().collided = true;
			}else{
				Destroy(collider.gameObject);
			}
		}
	}
}
