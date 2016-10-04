using UnityEngine;
using System.Collections;

public class EnemyMeleeAttack : StateMachineBehaviour {
		EnemyWeapon weapon;
		GameObject weaponTrail;
		public bool isCombo, isChargeAttack, resetCounter, lastHit;
     //OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		weapon = animator.GetComponentInChildren<EnemyWeapon>(); //finds the player's weapon
		weaponTrail = weapon.transform.FindChild("Enemy Weapon Trail").gameObject;
		weapon.active = true;
		weaponTrail.SetActive(true);
		if(isCombo){
			weapon.damage = weapon.comboDamage;
			animator.SetBool("Can Clash", true);
		} else if(isChargeAttack){
			weapon.damage = weapon.burstDamage;
		}

	}

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	 //OnStateExit is called before OnStateExit is called on any state inside this state machine
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		weaponTrail.SetActive(false);
		weapon.active = false;
		if(weapon.shieldCounter >=4 && lastHit){
			animator.SetTrigger("Charge Slash");
		} 
		if(resetCounter){
			weapon.shieldCounter = 0;
		}
	}

	// OnStateMove is called before OnStateMove is called on any state inside this state machine
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called before OnStateIK is called on any state inside this state machine
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMachineEnter is called when entering a statemachine via its Entry Node
	//override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
	//
	//}

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
	//
	//}
}
