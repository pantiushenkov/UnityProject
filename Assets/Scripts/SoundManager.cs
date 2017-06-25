using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	
	public static SoundManager Instance = new SoundManager();
	public AudioClip music;
	public static AudioSource musicSource;	
	
	public static bool is_sound_on;
	public static bool is_music_on;

	public bool isSoundOn() {
		return is_sound_on;
	}
	
	public void setSoundOn(bool val) {
		is_sound_on = val;
		PlayerPrefs.SetInt ("sound", is_sound_on ? 1 : 0);
		PlayerPrefs.Save ();
	}
	
	public bool isMusicOn() {
		return is_music_on;
	}
	
	public void setMusicOn(bool val) {
		is_music_on = val;
		this.toggleMusic();
		PlayerPrefs.SetInt ("music", is_music_on ? 1 : 0);
		PlayerPrefs.Save ();
	}

	void Awake(){
		is_sound_on = PlayerPrefs.GetInt ("sound", 1) == 1;
		is_music_on = PlayerPrefs.GetInt ("music", 1) == 1;
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.clip = music;
		musicSource.loop = true;
	}

	void Start(){
		setMusicOn(is_music_on);
		setSoundOn(is_sound_on);
	}
	
	void toggleMusic(){
		if(this.isMusicOn())
			musicSource.Play ();
		else 
			musicSource.Stop ();
	}
}
