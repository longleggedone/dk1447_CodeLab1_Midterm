using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagerScript : MonoBehaviour {

	//consts for saving to Player Prefs
	private const string PREF_TEST_KEY = "test";
	private const string PREF_HIGH_SCORE = "highScorePref";

	//private var for score
	private int score;

	//public property Score
	public int Score{
		get{
			return score;	
		}

		set{
			score = value;

			//if score > HighScore, make HighScore = score
			if(score > HighScore){
				HighScore = score;
			}

			//print out the score
			Debug.Log(score);
		}
	}

	//private var for highScore
	private int highScore = 33;

	//Property for HighScore
	public int HighScore{
		get{
			//before we get the highScore, load it from the PlayerPrefs
			highScore = PlayerPrefs.GetInt(PREF_HIGH_SCORE);
			return highScore;
		}

		set{
			//if we get a new high score, print "Confetti!!!"
			Debug.Log("Confetti!!!");
			highScore = value;
			//save the new highScore to PlayerPrefs
			PlayerPrefs.SetInt(PREF_HIGH_SCORE, highScore);
		}
	}

	//static var for singleton 
	public static ScoreManagerScript instance;

	// Use this for initialization
	void Start () {
		//if this is the first intance of the singleton
		//instance will not be set yet
		if(instance == null){
			//set instance to this instance of ScoreManager
			instance = this;
			//Dont destroy this gameObject when you go to a new scene
			DontDestroyOnLoad(this);
		} else { //otherwise, if we already have a singleton
			//Destroy the new one, since there can only be one
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
