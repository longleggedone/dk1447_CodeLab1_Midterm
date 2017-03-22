using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToScoreScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//if the mouseButton is clicked
		if(Input.GetMouseButtonDown(0)){
			//use the singleton to increate the score by 10
			ScoreManagerScript.instance.Score += 10;
		}
	}
}
