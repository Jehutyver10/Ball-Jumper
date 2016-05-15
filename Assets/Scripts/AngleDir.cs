using UnityEngine;
using System.Collections;

public class AngleDir : MonoBehaviour {
//returns -1 for left, 1 for right, 0 for forward/backward
	public static float AngleDirection(Vector3 forward, Vector3 targetDir, Vector3 up){
		Vector3 perp = Vector3.Cross(forward, targetDir);
		float dir = Vector3.Dot(perp, up);
		if(dir > 0.0f){
			return 1.0f;
		} else if (dir < 0.0f){
			return -1.0f;
		} else {
			return 0.0f;
		}
	}	
}
