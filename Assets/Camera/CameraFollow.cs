using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Animator anim;
	public float damping = 1;
	public float dampDelay = 2;

	private float dampTime, checkTime;
	private GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		dampTime = Time.time;
		player = FindObjectOfType<PlayerController>().gameObject;
		anim = GetComponent<Animator>();
		offset =  player.transform.position - transform.position;
	}

	
	// Update is called once per frame
	void LateUpdate () {
		Follow();
	}

	void Follow(){
		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = player.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);
		Quaternion rotation = Quaternion.Euler(0, angle, 0);
		transform.position = player.transform.position - (rotation * offset);
		transform.LookAt(player.transform);
	}

	public void AdjustDamping(){
		if(damping <100){
			checkTime = Time.time;
			if(checkTime - dampTime >= dampDelay){
				damping = 100;
			}
		}
	}

	public void SlowDamping(){
		damping = 10;
		dampTime = Time.time;
	}
}
