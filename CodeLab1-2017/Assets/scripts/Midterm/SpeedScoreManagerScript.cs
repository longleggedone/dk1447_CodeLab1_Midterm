using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedScoreManagerScript : MonoBehaviour {


	//consts for saving to Player Prefs
	private const string PREF_TEST_KEY = "test";
	private const string PREF_HIGH_SCORE = "highScorePref";

	//private var for score
	private float score;

	//public property Score
	public float Score{
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
		//	Debug.Log(score);
		}
	}

	//private var for highScore
	private float highScore;

	//Property for HighScore
	public float HighScore{
		get{
			//before we get the highScore, load it from the PlayerPrefs
			highScore = PlayerPrefs.GetFloat(PREF_HIGH_SCORE);
			return highScore;
		}

		set{
			//if we get a new high score, print "Confetti!!!"
			//Debug.Log("Confetti!!!");
			highScore = value;
			//save the new highScore to PlayerPrefs
			PlayerPrefs.SetFloat(PREF_HIGH_SCORE, highScore);
		}
	}



	public Text scoreText;
	public Text highScoreText;
	public Text finalScoreText;

	public GameObject player;
	public static SpeedScoreManagerScript instance;

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
		Score = player.GetComponent<PlayerScript>().moveSpeed; //gets move speed from player script
		scoreText.text = "MPH: " + Score.ToString("F3"); //converts speed to a string with 3 decimal places, zeroes allowed
		//print("MPH: " + HighScore);
//		finalScoreText.text = "Your Score - MPH: " + Score.ToString("F3");
//		highScoreText.text = "High Score - MPH: " + HighScore.ToString("F3");
	}

	public void PostScores(){
		finalScoreText.text = "Your Score - MPH: " + Score.ToString("F3");
		highScoreText.text = "High Score - MPH: " + HighScore.ToString("F3");
	}
}
