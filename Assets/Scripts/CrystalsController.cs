using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsController : MonoBehaviour {
	
	public List<UI2DSprite> crystalsList;
	public static CrystalsController controller;

	Dictionary<int,bool> obtainedCrystals = new Dictionary<int,bool>(); 
	Dictionary<int,Sprite> coloredCrystals = new Dictionary<int,Sprite>(); 

	void Awake () {
		controller = this;
	}
	
	public void Start(){
		clear();
	}

	public Sprite crystalBlue;
	public Sprite crystalGreen;
	public Sprite crystalRed;
	public Sprite crystalEmpty; 
	
	public List<UI2DSprite> getCrystalsList(){
		return crystalsList;
	}
	
	public void clear(){
		coloredCrystals[0] = crystalBlue;
		coloredCrystals[1] = crystalGreen;
		coloredCrystals[2] = crystalRed;
		update();
	}	

	void update(){
		for(int i=0;i<3;i++){
			crystalsList[i].sprite2D = obtainedCrystals.ContainsKey(i) ? coloredCrystals[i] : crystalEmpty;
		}
	}
	
	public void add(int index){
		obtainedCrystals[index] = true;
		update();
	}
}
