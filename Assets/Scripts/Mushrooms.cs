using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrooms : Collectable {
	
	protected override void OnRabitHit(HeroRabit rabit){
		rabit.scale(1);
		this.CollectedHide();
	}
}
