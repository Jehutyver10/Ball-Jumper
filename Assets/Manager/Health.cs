using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float health = 1000;
	public float maxHealth = 1000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (health > maxHealth) {
			health = maxHealth;
		}
	}

	public void TakeDamage(float damage, bool knockback = false){
		health -= damage;
		if(health <= 0f){
			Destroy(gameObject);
		}
		if(GetComponent<Animator>()){
			GetComponent<Animator>().SetTrigger("Take Damage");
		}
	}

	public void RestoreHealth(float healthRestored){
		if (health + healthRestored > maxHealth) {
			health = maxHealth; //prevents overflow
		} else {
			health += healthRestored;
		}

	}

}
