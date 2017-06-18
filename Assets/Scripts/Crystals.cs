using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : Collectable {
	
	public enum Color {
		Blue = 0,
		Green = 1,
		Red = 2
	}
	
	public Color color;

	protected override void OnRabitHit(HeroRabit rabit){
		CrystalsController.controller.add((int)color);
		this.CollectedHide();
	}
}
