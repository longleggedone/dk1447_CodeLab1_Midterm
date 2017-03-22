using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wk3GameManager : MonoBehaviour {

	//Make a private int score
	private int score;

	//Make a public property "Score" that prints to
	//the console when you get/set it's value
	public int Score{
		get{
			Debug.Log("Some wants the scores");
			return score;
		}

		set{
			score = value;

			Debug.Log("score was set to: " + score);
		}
	}

	//private var, to show it can only be seen in this script
	private int intIdontWantAnyOneElseToChangeOrSee;

	//public static instance for a singleton
	public static Wk3GameManager instance;

	// Use this for initialization
	void Start () {
		//if this is the first intance of the singleton
		//instance will not be set yet
		if(instance == null){
			//set instance to this instance of Wk3GameManager
			instance = this;
			//Dont destroy this gameObject when you go to a new scene
			DontDestroyOnLoad(this);
		} else {//otherwise, if we already have a singleton
			//Destroy the new one, since there can only be one
			Destroy(gameObject);
		}

		//add 10 to score, look, it gets printed in the console!
		Score += 10;

//		Time.deltaTime =6767;  //This line won't compile, because deltaTime is read-only
		
	}
	
	// Update is called once per frame
	void Update () {
		//if the 'c' key is pressed
		if(Input.GetKeyDown(KeyCode.C)){
			//Load Scene "Week3-2"
			SceneManager.LoadScene("Week3-2");
		}
	}
}
