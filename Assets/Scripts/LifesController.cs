using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesController : MonoBehaviour {

	public List<UI2DSprite> heartsList;
	public Sprite life;
	public Sprite lifeUsed;
	int lives = 3;
	public static LifesController controller;
	public GameObject loosePrefab;
	
	void Awake(){
		controller = this;
	}

	void Start () {
		updateLives();
	}

	void updateLives(){
		for(int i=0;i<3;i++){
			heartsList[i].sprite2D = i < lives ? life : lifeUsed;
		}
	}

	public void decreaseLives(){
		lives -= 1;
		updateLives();
		if(lives == 0){
			GameObject parent = UICamera.first.transform.parent.gameObject;
			GameObject obj = NGUITools.AddChild (parent, loosePrefab);
			LoosePopup popup = obj.GetComponent<LoosePopup>();
		}
	}
}
