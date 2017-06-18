using System.Collections;
using System.Collections.Generic;
using UnityEngine;	

public class DeathHere : MonoBehaviour {

		void OnTriggerEnter2D(Collider2D collider) {
		HeroRabit rabit = collider.GetComponent<HeroRabit>();
			Debug.Log(rabit);
			if(rabit != null && !HeroRabit.isDying){
				LevelController.current.onRabitDeath(rabit);
			}
		}
}
