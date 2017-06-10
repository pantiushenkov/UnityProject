using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : Collectable {

	protected override void OnRabitHit(HeroRabit rabit){
		Debug.Log("OnRabitHit");
		rabit.triggerDie();
		this.CollectedHide();
	}
}
