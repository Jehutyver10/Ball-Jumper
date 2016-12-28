using UnityEngine;
using System.Collections;

public class PlayerIdle : StateMachineBehaviour {
		Weapon weapon;
		GameObject weaponTrail;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.GetComponent<PlayerController>().AllowAttack();
		animator.applyRootMotion = true;
		//this is the only way I could think to get the weapon renderer, weapon collider, and weapon trail to work for the last attack. don't ask me why.
		weapon = animator.GetComponentInChildren<Weapon>(); //finds the player's weapon
		weaponTrail = weapon.transform.FindChild("Player Weapon Trail").gameObject;

		weaponTrail.SetActive(false);
		weapon.active = false;
		weapon.GetComponent<Renderer>().enabled = false;
		weapon.GetComponent<Collider>().enabled = false;
		weapon.downswing = false;
		weapon.upswing = false;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
