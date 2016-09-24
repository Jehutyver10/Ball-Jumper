using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {
	public bool isActive = false;
	// Use this for initialization
	void Start () {
		//find all the weapons that the enemy has
		for(int i = 0; i < transform.root.GetComponentsInChildren<EnemyWeapon>().Length; i++){
			//transform.root.GetComponentsInChildren<EnemyWeapon>()[i].name = transform.root.GetComponentsInChildren<EnemyWeapon>()[i].name + " " + i.ToString(); 
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
