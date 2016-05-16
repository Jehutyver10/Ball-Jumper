using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Projectile))]
public class Bomb : MonoBehaviour {
	// Use this for initialization

	PlayerController player;
	public Animator anim;
	public bool begunCharge = false;

	void Awake(){
		anim = GetComponent<Animator>();
		player = FindObjectOfType<PlayerController>();
	}
	// Update is called once per frame
	void Update () {
		if(!Input.GetButton("Shoot") && begunCharge){
			anim.SetTrigger("Launch");
		}
	}
	void PrepareToFireBomb(){
		begunCharge = true;
	}
	void Launch(){
		Debug.Log("launching");
		Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		transform.rotation = q * transform.rotation;
		print(GetComponent<Projectile>().speed);
		GetComponent<Rigidbody>().AddForce(transform.forward * GetComponent<Projectile>().speed, ForceMode.Impulse);
		print(GetComponent<Rigidbody>().velocity);
		player.GetComponent<Animator>().SetBool("Bomb Launched", true);
	}
}

