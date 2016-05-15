using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float speed = 1000;
	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GameObject.FindObjectOfType<Projectile>().GetComponent<Collider>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
