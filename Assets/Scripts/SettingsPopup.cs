using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour {

	public Sprite soundOnSprite = null;
	public Sprite soundOffSprite = null;
	public Sprite musicOnSprite = null;
	public Sprite musicOffSprite = null;
	public MyButton soundButton = null;
	public MyButton closeButton = null;
	public MyButton musicButton = null;
	
	void Start () {
		isSoundOn = SoundManager.Instance.isSoundOn();
		isMusicOn = SoundManager.Instance.isMusicOn();
		
		UI2DSprite sound = soundButton.GetComponent<UI2DSprite> ();
		sound.sprite2D = isSoundOn ?  soundOnSprite : soundOffSprite;
		UI2DSprite music = musicButton.GetComponent<UI2DSprite> ();
		music.sprite2D = isMusicOn ?  musicOnSprite : musicOffSprite; 
		soundButton.signalOnClick.AddListener (this.onSound);
		musicButton.signalOnClick.AddListener (this.onMusic);
		closeButton.signalOnClick.AddListener (this.onClose);
	}

	void onClose(){
		Destroy(this.gameObject);
	}
	
	void Update (){
		toggleMusic();
		toggleSound();
	}

	bool isMusicOn;
	
	void onMusic(){
		isMusicOn = ! SoundManager.Instance.isMusicOn();
		SoundManager.Instance.setMusicOn (isMusicOn);
		toggleMusic();
	}

	void toggleMusic(){
		UI2DSprite sprite = musicButton.GetComponent<UI2DSprite> ();
		sprite.sprite2D = isMusicOn ?  musicOnSprite : musicOffSprite; 
	}
	
	bool isSoundOn;

	void onSound(){
		isSoundOn = ! SoundManager.Instance.isSoundOn();
		SoundManager.Instance.setSoundOn (isSoundOn);
		toggleSound();
	}

	void toggleSound(){
		UI2DSprite sprite = soundButton.GetComponent<UI2DSprite> ();
		sprite.sprite2D = isSoundOn ?  soundOnSprite : soundOffSprite;
	}

}
