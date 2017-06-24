using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour {

	public MyButton soundButton = null;
	public MyButton closeButton = null;
	public MyButton musicButton = null;
	
	void Start () {
		soundButton.signalOnClick.AddListener (this.onSignal);
		musicButton.signalOnClick.AddListener (this.onMusic);
		closeButton.signalOnClick.AddListener (this.onClose);
	}

	void onClose(){
		Destroy(this.gameObject);
	}
	
	void onMusic(){
		Debug.Log("onMusic");
	}
	
	void onSignal(){
		Debug.Log("onMusic");
	}
	
}
