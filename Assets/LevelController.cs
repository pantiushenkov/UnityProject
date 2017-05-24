using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current;
	// Use this for initialization
	void Awake () {
		current = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
