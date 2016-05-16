using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Projectile))]
public class Bomb : MonoBehaviour {
	// Use this for initialization

	public bool collided = false;
	Vector3 pos;
	Quaternion rot;
	Vector3 sca;
	void Awake () {
		pos = transform.localPosition;
		rot = transform.localRotation;
		sca = transform.localScale;
		Physics.IgnoreCollision(FindObjectOfType<PlayerController>().GetComponent<Collider>(), 
		GetComponent<Collider>()); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Reset(){
		if(collided){
			collided = false;
			transform.localPosition= pos;
			transform.localRotation = rot; 
			transform.localScale = sca;
			gameObject.SetActive(false);
		}
	}
}
