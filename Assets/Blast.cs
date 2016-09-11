using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour {
	Rigidbody rb;
	public float speed = 1f;
	public Enemy target;
	float endTime, timeLimit = 5;

	// Use this for initialization
	void Start () {
		endTime= Time.time;

		speed = GetComponent<Projectile>().speed;
		rb = GetComponent<Rigidbody>();
	}

	void Awake(){
		transform.position = GameObject.FindObjectOfType<PlayerController>().transform.position +
		GameObject.FindObjectOfType<PlayerController>().transform.forward;
	}
	// Update is called once per frame
	void Update () {
		if(target){
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position + transform.forward, speed * Time.deltaTime);
			transform.LookAt(target.transform);
		}
		if(Time.time - endTime > timeLimit){
			Destroy(gameObject);
			print("Self-destructing");
		}
	}

	public void setTarget(Enemy enemy){
		target = enemy;
		}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.GetComponent<Enemy>()){
			print ("Collided with " + col.gameObject.name);
			Destroy(col.gameObject);	
			Destroy(gameObject);
		}
	}


}
