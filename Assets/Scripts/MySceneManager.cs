using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour {

	public static void loadScene(string scene){
		SceneManager.LoadScene(scene);
	}
	
	public static string getCurrentScene(){
		return SceneManager.GetActiveScene().name;
	}
}
