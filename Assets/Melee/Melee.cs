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
		weaponTrail = weapon.transform.Find("Player Weapon Trail").gameObject;
		weapon.active = true;

		if(downswing){
			weapon.downswing = true;
		}
		if(upswing){
			weapon.upswing = true;
		}
		weapon.GetComponent<Renderer>().enabled = true;
		weapon.GetComponent<Collider>().enabled = true;
		weaponTrail.SetActive(true);		

		player.isAttacking = true;
		animator.applyRootMotion = false;
		animator.SetBool("Can Clash", true);
		MeleeLimit = 2f;
	}	
		
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(isLastHit){		
			player.isLastAttack = true;
			weapon.knockback = true;
		}

		if(isCombo){
			player.penultimateAttack = true;
			if(!isLastHit){
				if(isPenultimateHit){
					if(Mathf.RoundToInt(CrossPlatformInputManager.GetAxis("Altitude")) > 0 && canAttack){
						
						animator.SetTrigger("Upswing");
						canAttack = false;

					}else if(Mathf.RoundToInt(CrossPlatformInputManager.GetAxis("Altitude")) < 0 && canAttack){
						animator.SetTrigger("Downswing");
						canAttack = false;

					}

				}
				if(CrossPlatformInputManager.GetButtonDown("Attack") && canAttack){
					animator.SetTrigger("Continue Combo");
					canAttack = false;
				}
			}

		}
			animator.ResetTrigger("Begin Melee Combo");

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		canAttack = true;
		player.penultimateAttack = false;
		weapon.knockback = false;
		if(isDashAttack){
			player.isBoosted = false;
		}


	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(target){
			if(Vector3.Distance(player.transform.position, target.transform.position) > MeleeLimit){
				player.transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z));
				if (!player.isLastAttack) {
					player.GetComponent<CharacterController> ().Move ((target.transform.position - player.transform.position) * Time.deltaTime * player.speed);

				}
			}
		}
	}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}












