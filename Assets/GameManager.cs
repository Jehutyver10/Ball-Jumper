using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject enemy;
	public HealthText enemyText;
	public bool paused = false;
	public Text subWeaponMenu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//TODO get rid of this; it's only for testing
		if(Input.GetButtonDown("Activate Enemies")){
			if(GameObject.FindObjectOfType<Enemy>() == null){
				GameObject newEnemy = Instantiate(enemy, enemy.transform.position, Quaternion.identity) as GameObject;
//				if (newEnemy.GetComponent<Health> ()) {
//					enemyText.health = newEnemy.GetComponent<Health> ();
//				}
			}
		}
	
	}

	public void Pause(){
		Time.timeScale = 0;
		paused = false;
	}

	public void Unpause(){
		Time.timeScale = 1;
		paused = true;
	}
		
	public void	ShowSubMenu(string[] subweaponList){
		Pause ();
		foreach (string subweapon in subweaponList) {
			if (!subWeaponMenu.text.Contains (subweapon)) {
				subWeaponMenu.text = subWeaponMenu.text + "\n" + subweapon;
			}
		}
	}
	public void CollapseSubMenu(string selectedSubWeapon){
		Unpause ();
		subWeaponMenu.text = selectedSubWeapon;
	}
}
