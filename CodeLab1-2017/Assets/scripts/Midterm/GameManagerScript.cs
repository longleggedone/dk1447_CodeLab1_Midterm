using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {


	public GameObject gameOverScreen;
	public static bool playerDead = false;
	public bool gameOverState = false;

	public static GameManagerScript instance;
	// Use this for initialization
	void Start () {
		if(instance == null){
			//set instance to this instance of Wk3GameManager
			instance = this;
			//Dont destroy this gameObject when you go to a new scene
			DontDestroyOnLoad(this);
		} else {//otherwise, if we already have a singleton
			//Destroy the new one, since there can only be one
			Destroy(gameObject);
		}

		gameOverScreen.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if (gameOverState != true && playerDead){
			gameOverState = true;
			GameOver();
		}
		if (gameOverState == true){
			if (Input.GetKeyDown(KeyCode.R)){
				gameOverScreen.SetActive(false);
				gameOverState = false;
				playerDead = false;
				SceneManager.LoadScene("MidTerm");

			}
		}
	}

	void GameOver(){
		gameOverScreen.SetActive(true);

	}
}
