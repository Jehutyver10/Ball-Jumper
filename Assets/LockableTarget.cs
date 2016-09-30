using UnityEngine;
using System.Collections;

public class LockableTarget : MonoBehaviour {
	public float positionFromPlayer;
	public bool grabbable = true, grabbed = false;
	// Use this for initialization
	void Start () {
		
		gameObject.tag = "Lockable";
	}

	public void setPositionFromPlayer(GameObject player){
		positionFromPlayer = transform.InverseTransformPoint(player.transform.position).x;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
