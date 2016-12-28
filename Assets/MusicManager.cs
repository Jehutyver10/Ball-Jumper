using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	public AudioClip[] playlist; 
	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = playlist[0];
		audioSource.loop = true;
		audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		ToggleMusic();
		
	}
	//for testing and user convenience only
	void ToggleMusic(){
		if(Input.GetKeyDown(KeyCode.M)){
			if(!audioSource.isPlaying){
				audioSource.UnPause();
			}else{
				audioSource.Pause();
			}
		}
	}
}
