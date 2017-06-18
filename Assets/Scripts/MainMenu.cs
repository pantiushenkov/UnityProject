using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public MyButton playButton;
	
	void Start () {
		playButton.signalOnClick.AddListener (this.onPlay);
	}

	void onPlay(){
		MySceneManager.loadScene("ChooseLevel");
	}
}
