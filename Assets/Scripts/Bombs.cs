using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : Collectable {

	protected override void OnRabitHit(HeroRabit rabit){
		rabit.die();
		this.CollectedHide();
	}
}
