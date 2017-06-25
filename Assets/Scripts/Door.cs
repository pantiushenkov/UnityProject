using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	Vector3 my_pos,rabit_pos;
	
	public int level;
	LevelStats stats;
	
	void Start(){

	}

	void Update () {
		rabit_pos = HeroRabit.lastRabit.transform.position;
		my_pos = this.transform.position;

	
		if(Vector3.Distance(rabit_pos, my_pos) < 1.5){
			MySceneManager.loadScene("Level" + level);	
		}
	}
}
