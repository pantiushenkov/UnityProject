using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExit : MonoBehaviour {

	public int level;	
	public GameObject winPrefab;
	
	void OnTriggerEnter2D(Collider2D collider){
		HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if (rabit != null){
        
		LevelStats stats = LevelController.current.getStats();
		if(stats == null ) Debug.Log("Statsk is null");
		stats.levelPassed = true;
	
		if(FruitsController.controller.allCollected())
			stats.hasAllFruits = true;			 
		
		PlayerPrefs.SetInt("coins", CoinsController.controller.getCoins());
        //string str = JsonUtility.ToJson(stats);
		///PlayerPrefs.SetString(currentLevelName, str);
		
		PlayerPrefs.Save();

		GameObject parent = UICamera.first.transform.parent.gameObject;
		GameObject obj = NGUITools.AddChild (parent, winPrefab);
		WinPopup popup = obj.GetComponent<WinPopup>();
		
		}
	}
}
