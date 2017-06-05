using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current;
	int coins = 0;
	int fruits = 0;
	int crystals = 0;
	Vector3 startingPosition;
	
	public void setStartPosition(Vector3 pos){
		this.startingPosition = pos;
	}
	
	public void onRabitDeath(HeroRabit rabit){ 
		rabit.transform.position = this.startingPosition;
	}
	// Use this for initialization
	void Awake () {
		current = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addCoins(int coin){
		this.coins+=coin;	
	}
	
	public void addFruits(int fruit){
		this.fruits+=fruit;	
	}

	public void addCrystals(int crystal){
		this.crystals+=crystal;	
	}
}
