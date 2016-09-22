using UnityEngine;
using System.Collections;
public class Melee : StateMachineBehaviour {
	GameObject target, player;
	Weapon weapon;


	public float MeleeLimit = 5;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		target = animator.GetComponent<PlayerController>().target;//finds the target of the player
		player = animator.GetComponent<PlayerController>().gameObject;//finds player
		weapon = animator.GetComponentInChildren<Weapon>(); //finds the player's weapon

		weapon.ActivateOrDeactivate();
	
	}
		
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if(target){
			if(Vector3.Distance(player.transform.position, target.transform.position) > MeleeLimit){
				player.GetComponent<CharacterController>().Move(player.transform.forward * Time.deltaTime * player.GetComponent<PlayerController>().speed);
			}
		}

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		weapon.ActivateOrDeactivate();
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
