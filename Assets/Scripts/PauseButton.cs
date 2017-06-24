using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

	public GameObject settingsPrefab;
	public MyButton pauseButton;

	void Start () {
		pauseButton.signalOnClick.AddListener (this.showSettings);
	}

	void showSettings() {
		Debug.Log("EKEKEK");
		GameObject parent = UICamera.first.transform.parent.gameObject;
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		SettingsPopup popup = obj.GetComponent<SettingsPopup>();
	}
}
