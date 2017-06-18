using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour {
	
	int coins; 
	public UILabel coinsLabel;
	public static CoinsController controller;
	
	void Awake () {
		controller = this;
	}
	
	public void Start(){
		clear();
	}

	public void clear(){
		this.coins = 0;
		coinsLabel.text = "0000";
	}

	public void add(int coin){
		this.coins+=coin;
		if(this.coins < 10)	
			coinsLabel.text = "000" + coins.ToString();
		else if(this.coins < 100)
			coinsLabel.text = "00" + coins.ToString();
		else if(this.coins < 1000) 
			coinsLabel.text = "0" + coins.ToString();
		else 
			coinsLabel.text = coins.ToString();
	}
}
