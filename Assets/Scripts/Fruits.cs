﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : Collectable {

	protected override void OnRabitHit(HeroRabit rabit){
		FruitsController.controller.add(1);
		this.CollectedHide();
	}
}
