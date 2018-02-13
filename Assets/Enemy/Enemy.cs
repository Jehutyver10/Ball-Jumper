using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
[RequireComponent (typeof (Health))]
public class Enemy : LockableTarget {
	private float probability;

	public bool canBeHomedInOn, active = false, alive = false, playerDashed = false, stunned = false;
	public float shotsPerSecond, meleeLimit, detectionRange, shotSpeed, currentSpeed = 1, dashSpeed, rushSpeed, actionPeriod; 
	public GameObject laser;
	public EnemyWeapon weapon;
	public PseudoEnemy pseudo;
	public List<string> PossibleMovingCommands;
	bool nextActionDecided = false;
	public enum State {Idle, Stunned, Move, Rush, Dash, Strafe, Attack, Block};
	public State currentState = State.Idle;
	Animator anim;
	Health health;
	CharacterController cc;

	private bool readyForNextState = true, rushing = false;

	public GameObject target;
	// Use this for initialization

	void Awake(){
		PossibleMovingCommands = new List<string> () { "Dash", "Strafe", "Attack"};
		SetTarget();
		cc = GetComponent<CharacterController> ();
	}
	void Start () {
		anim = GetComponent<Animator>();
		health = GetComponent<Health>();
		weapon = GetComponentInChildren<EnemyWeapon>();
		pseudo = GetComponentInChildren<PseudoEnemy>();
		//StartCoroutine ("Idle");
	}

	// Update is called once per frame
	void Update(){
		probability = Time.deltaTime * shotsPerSecond;
		if (alive) {
			print ("check 1");

			transform.LookAt (new Vector3 (target.GetComponent<PlayerController>().pseudo.transform.position.x,
				target.GetComponent<PlayerController> ().pseudo.transform.position.y, 
				target.GetComponent<PlayerController> ().pseudo.transform.position.z));
			
		
//			if(active){
//				if(Vector3.Distance(target.transform.position, transform.position) > meleeLimit){ //check if outside melee limit
//					anim.ResetTrigger("Melee Attack");
//					if(Random.value < probability){
//						Shoot();
//					}
//				}else{
//					anim.SetTrigger("Melee Attack");
//				}
//			}
		
			if (WithinMidrange ()) {
				if (WithinMeleeRange()) {
					if (readyForNextState) {
						currentState = (State)System.Enum.Parse (typeof(State), PossibleMovingCommands [Random.Range (0, 3)]);
						readyForNextState = false;
					}
				} else {
					currentState = State.Move;
				}
			} else {
				currentState = State.Rush;
			}

		}
		//TODO remove this in final version; it's only for testing
		if(Input.GetButtonDown("Activate Enemies")){
			alive = !alive;
			anim.SetBool("Activated", !anim.GetBool("Activated"));
		}
	}

	void FixedUpdate(){
		if (alive) {

			if (!rushing) {
				ActOnState ();
			}
		}

	}

	void Move(){
		cc.Move (transform.forward * currentSpeed);
		print ("Moving");
		readyForNextState = true;

	}

	IEnumerator Rush(Vector3 destination){
		rushing = true;
		while (Vector3.Distance (transform.position, destination) > 1) {
			print ("rushing");
			cc.Move ((destination - transform.position).normalized * rushSpeed);
			yield return null;
		}
		rushing = false;
		Reset ();
	}
	void Reset(){
		currentState = State.Idle;
		readyForNextState = true;

	}
	void ActOnState(){
		switch(currentState){
		case State.Stunned:
			print ("stunned");
			break;
		case State.Move:
			Move ();

			break;
		case State.Rush:
			if (!rushing) {
				StartCoroutine (Rush (target.GetComponent<PlayerController> ().pseudo.transform.position));
			}
			break;
		case State.Dash:
			print ("dashing");
			break;
		case State.Strafe:
			print ("strafing");
			break;
		case State.Attack:
			print ("attacking");
			break;
		case State.Block:
			print ("blocking");
			break;
		default:
			print ("idle");
			break;

		}
	}



	bool WithinMidrange(){
		if (Vector3.Distance (transform.position, target.transform.position) > detectionRange) {
			return false;
		} else {
			return true;
		}
	}
	bool WithinMeleeRange(){
		if (Vector3.Distance (transform.position, target.transform.position) > meleeLimit) {
			return false;
		} else {
			return true;
		}
	}




	void SetTarget(){
		target = GameObject.FindObjectOfType<PlayerController>().gameObject;
	}

	void OnWeaponsClash(){
		if(weapon.damage == weapon.comboDamage){
			anim.SetTrigger("Clash");
			print("Clashing");
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.GetComponent<Environment>()){
			health.TakeDamage(col.impulse.magnitude * 2);
		}
	}
	void Shoot(){
		GameObject shot = Instantiate(laser, transform.position + transform.forward, Quaternion.identity) as GameObject;
		shot.transform.parent = this.transform;
		shot.GetComponent<Projectile>().SetShooter(this.gameObject);
		//Quaternion q = Quaternion.FromToRotation(Vector3.up, transform.forward);
		//shot.transform.rotation = q * shot.transform.rotation;
		shot.GetComponent<Rigidbody>().AddForce(transform.forward * shot.GetComponent<Projectile>().speed);

	}

	//
	//	IEnumerator Idle () { //business as usual until the target appears within range
	//		while(Vector3.Distance(transform.position, target.transform.position) > detectionRange)
	//		{
	//			active = false;
	//			//TODO put some patrol code here or something? 
	//			yield return null;
	//		}
	//		active = true;
	//		StartCoroutine ("Pursue");
	//	}
	//
	//	IEnumerator Pursue () {//once detected);
	//		while (active && !WithinMeleeRange()) {//if outside the melee limit but activated
	//			if (Vector3.Distance (transform.position, target.transform.position) < detectionRange) {//if not yet approached
	//
	////				if (Random.value < probability) {
	////					Shoot ();
	////				}
	//				yield return null;
	//			} else { 
	//				StartCoroutine ("Idle");
	//				yield break;
	//			}
	//		}
	//		StartCoroutine ("MeleeAttack");
	//	}
	//
	//	IEnumerator MeleeAttack(){
	//		while (active && Vector3.Distance (transform.position, target.transform.position) < meleeLimit) {
	//			yield return null;
	//		}
	//		StartCoroutine ("Pursue");
	//	}

}
