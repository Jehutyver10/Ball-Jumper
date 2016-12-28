using UnityEngine;
using System.Collections;

public class Grabbable : MonoBehaviour {
	public bool grabbed;

	//put this on the top level
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(grabbed){
			if(GetComponent<Enemy>()){
				GetComponent<Enemy>().activated = false;
			}
		}
	}
}
