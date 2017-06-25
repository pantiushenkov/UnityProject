using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoosePopup : MonoBehaviour {
	public AudioClip music;
	public static AudioSource musicSource;	
	public MyButton menuButton = null;
	public MyButton replayButton = null;
	
	
	void Start () {
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.clip = music;
		musicSource.loop = true;
		isMusicOn = SoundManager.Instance.isMusicOn();
	
		if(isMusicOn)
			musicSource.Play ();
		else 
			musicSource.Stop ();
			
		menuButton.signalOnClick.AddListener (this.onMenu);
		replayButton.signalOnClick.AddListener (this.onReplay);
	}


	bool isMusicOn;
	
	void onMenu(){
		MySceneManager.loadScene("MainMenu");
		Destroy(this.gameObject);	
	}
	
	void onReplay(){
		MySceneManager.loadScene(MySceneManager.getCurrentScene());
		Destroy(this.gameObject);	
	}
}
