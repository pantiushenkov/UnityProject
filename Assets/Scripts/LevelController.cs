using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
	
	public static LevelController current;
	Vector3 startingPosition;
	LevelStats stats;

	public void setStartPosition(Vector3 pos){
		this.startingPosition = pos;
	}
	
	public void onRabitDeath(HeroRabit rabit){
		if(MySceneManager.getCurrentScene() != "ChooseLevel")
			LifesController.controller.decreaseLives(); 
		rabit.transform.position = this.startingPosition;
	}
	
	void Awake () {
		current = this;
		string str = PlayerPrefs.GetString ("stats", null);
		this.stats = JsonUtility.FromJson<LevelStats> (str);
		if(this.stats != null) {
			this.stats = new LevelStats ();
		}
	}

}
