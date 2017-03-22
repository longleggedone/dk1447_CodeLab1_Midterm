using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavePlayerPosition : MonoBehaviour {

	//bool to control whether to load the player
	//position from a file
	public bool loadPlayer = false;

	//file name to save/load player position
	public string fileName;

	//var to save path to save file
	string filePath;

	//const for '|' character we will use to seperate values in the file
	const char DELIMITER = '|';

	// Use this for initialization
	void Start () {

		//set filePath to dataPath plus fileName
		filePath = Application.dataPath + "/" + fileName;

		//if the file exists and loadPlayer is true
		if(File.Exists(filePath) && loadPlayer)
		{
			//create a StreamReader to filePath
			StreamReader sr = new StreamReader(filePath);

			//read the first line of the file
			string line = sr.ReadLine();

			//close the stream reader
			sr.Close();

			//split the string we read by '|' to seperate values
			//x|y|z for example 0|1.5|4 will become ["0", "1.5", "4"]
			string[] splitLine = line.Split(DELIMITER);

			//set the transform position to the x, y, z saved in the file
			//convert strings into floats, for example "1.5" becomes 1.5f
			transform.position = new Vector3(
				float.Parse(splitLine[0]),
				float.Parse(splitLine[1]),
				float.Parse(splitLine[2]));
		}
	}
	
	// Update is called once per frame
	void Update () {
		//if the space key is pressed
		if(Input.GetKeyDown(KeyCode.Space)){
			//Get the position of the gameObject this script is attached to
			Vector3 playerPos = transform.position;

			//Open a StreamWriter to file path, set to overwrite the existing file
			StreamWriter sw = new StreamWriter(filePath, false);

			//Write a string to the file, with the x, y, z values seperated by '|'
			sw.WriteLine("" + 
				playerPos.x + DELIMITER + 
				playerPos.y + DELIMITER +
				playerPos.z);


			//Close the StreamWriter
			sw.Close();
		}
	}
}
