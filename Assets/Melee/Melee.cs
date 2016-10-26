using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Melee : StateMachineBehaviour {
	GameObject target, weaponTrail;
	PlayerController player;
	Weapon weapon;
	public bool isCombo, isDashAttack, isChargeAttack, isLastHit, isPenultimateHit, canAttack = true, downswing, upswing;
	public float MeleeLimit = 4;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		target = animator.GetComponent<PlayerController>().target;//finds the target of the player
		player = animator.GetComponent<PlayerController>();//finds player
		weapon = animator.GetComponentInChildren<Weapon>(); //finds the player's weapon
		weaponTrail = weapon.transform.FindChild("Player Weapon Trail").gameObject;
		weapon.active = true;
		if(downswing){
			weapon.downswing = true;
		}
		if(upswing){
			weapon.upswing = true;
		}
		weapon.GetComponent<MeshRenderer>().enabled = true;
		weapon.GetComponent<BoxCollider>().enabled = true;
		weaponTrail.SetActive(true);
		animator.SetBool("Can Clash", true);
		MeleeLimit = 2f;
	}	
		
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(isLastHit){
			weapon.knockback = true;
		}


		if(isCombo){
			player.penultimateAttack = true;
			if(!isLastHit){
				if(isPenultimateHit){
					if(CrossPlatformInputManager.GetAxis("Altitude") > 0 && canAttack){
						animator.SetTrigger("Upswing");
						canAttack = false;

					}else if(CrossPlatformInputManager.GetAxis("Altitude") < 0 && canAttack){
						animator.SetTrigger("Downswing");
						canAttack = false;

					}

				}
				if(CrossPlatformInputManager.GetButtonDown("Attack") && canAttack){
					Debug.Log("here");
					animator.SetTrigger("Continue Combo");
					canAttack = false;
				}
			}

		}
			animator.ResetTrigger("Begin Melee Combo");

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		weaponTrail.SetActive(false);
		weapon.active = false;
		weapon.GetComponent<MeshRenderer>().enabled = false;
		weapon.GetComponent<BoxCollider>().enabled = false;
		canAttack = true;
		player.penultimateAttack = false;
		weapon.knockback = false;
		if(isDashAttack){
			player.isBoosted = false;
		}
		weapon.downswing = false;
		weapon.upswing = false;

	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(target){
			if(Vector3.Distance(player.transform.position, target.transform.position) > MeleeLimit){
				player.GetComponent<CharacterController>().Move(player.transform.forward * Time.deltaTime * player.speed);
			}
		}
	}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}












