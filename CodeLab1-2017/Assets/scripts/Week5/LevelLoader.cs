using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	//public vars to offset the starting position
	//of the loaded level, so tha the upper 
	//left corner can appear somewhere other than
	//(0, 0, 0)
	public float offsetX = 0;
	public float offsetY = 0;

	//Array of files from which to load levels
	public string[] fileNames;
	//index of which file to load from "fileNames"
	//notice this var is static, so it will remain
	//even when we load a new level
	public static int levelNum = 0;

	// Use this for initialization
	void Start () {

		//select a fileName to load
		string fileName = fileNames[levelNum];

		//create a path to that fileName
		string filePath = Application.dataPath + "/" + fileName;

		//Create a StreamReader from that filePath
		StreamReader sr = new StreamReader(filePath);

		//Create an empty GameObject to hold all the level gameObjects
		GameObject levelHolder = new GameObject("Level Holder");

		//int to track the Y position in the file 
		int yPos = 0;

		//Create a gameObject for the player by loading it and instantiating it
		//from the prefab in the Resources folder
		GameObject player = Instantiate(Resources.Load("Prefabs/Player") as GameObject);

		//While we haven't reach the end of the file
		while(!sr.EndOfStream){
			//load a line of text into "line"
			string line = sr.ReadLine();

			//loop through each character in "line"
			//each character is a new X position
			for(int xPos = 0; xPos < line.Length; xPos++){

				//if the currect character is an 'x'
				if(line[xPos] == 'x'){
					//Create a new cube gameObject from a Primitive
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

					//Make the parent transform levelHolder's transform, so it is
					//easier to manage in the scene hiearchy
					cube.transform.parent = levelHolder.transform;

					//Move the cube to the appropriate position on the screen according
					//to where it appeared in the file and the offsets
					cube.transform.position = new Vector3(
						xPos + offsetX, 
						yPos + offsetY, 
						0);
				} if(line[xPos] == 'P'){ // we see a 'P'
					//Move the player to that location
					player.transform.position = new Vector3(
						xPos + offsetX, 
						yPos + offsetY, 
						0);
				}
			}

			//decrease the Y Position for each new line
			yPos--;
		}

		//Close the StreamReader
		sr.Close();
	}
	
	// Update is called once per frame
	void Update () {
		//If someone presses the "P"
		if(Input.GetKeyDown(KeyCode.P)){
			//Increase the level by 1
			levelNum++;
			//Reload the scene "Week5", but a new level will appear
			//because we increased the level number
			SceneManager.LoadScene("Week5");
		}
	}
}
