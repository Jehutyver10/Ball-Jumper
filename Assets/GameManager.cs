using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject enemy;
	public HealthText enemyText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//TODO get rid of this; it's only for testing
		if(Input.GetButtonDown("Activate Enemies")){
			if(GameObject.FindObjectOfType<Enemy>() == null){
				GameObject newEnemy = Instantiate(enemy, enemy.transform.position, Quaternion.identity) as GameObject;
				enemyText.health = newEnemy.GetComponent<Health>();
			}
		}
	
	}
}
