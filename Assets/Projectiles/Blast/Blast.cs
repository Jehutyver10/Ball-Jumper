using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Projectile))]
public class Blast : MonoBehaviour {
	float speed;
	public Enemy target;
	float endTime, timeLimit = 5;

	// Use this for initialization
	void Start () {
		endTime= Time.time;

		speed = GetComponent<Projectile>().speed;
	}

	void Awake(){
		transform.position = GameObject.FindObjectOfType<PlayerController>().transform.position +
		GameObject.FindObjectOfType<PlayerController>().transform.forward;
	}
	// Update is called once per frame
	void Update () {
		if(target){
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
			transform.LookAt(target.transform);
		} else{
			transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
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

		}

	}


}
