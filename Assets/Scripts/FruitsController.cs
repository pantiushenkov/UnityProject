using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsController : MonoBehaviour {
	
	public static FruitsController controller;
	public UILabel fruitsLabel;

	int fruits;
	int maxFruits;
	int count;
		
	void Awake () {
		controller = this;
	}
	
	public void Start(){
		clear(0);
	}

	public void clear(int number){
		this.fruits = number;
		maxFruits = GameObject.FindGameObjectsWithTag("Fruit").Length;
		update();
	}

	public void update(){
		fruitsLabel.text = this.fruits + "/" + this.maxFruits;	
	}


	public void add(int fruit){
		this.fruits+=fruit;	
		update();
	}

}
