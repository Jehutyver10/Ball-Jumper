using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HealthText : MonoBehaviour {
//TODO delete this whole thing after testing is complete and replace with a goddamn healthbar or something
		public Text text;
		public Health health;
		public string associatedObjectName;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = associatedObjectName + ": " + health.health;
	}
}
