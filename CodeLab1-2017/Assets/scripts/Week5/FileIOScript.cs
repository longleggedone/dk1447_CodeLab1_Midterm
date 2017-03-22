using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileIOScript : MonoBehaviour {

	public string fileName = "temp.txt"; //public var for the file to read/write

	public List<string> highScoreNames;  //public List of highScore names
	public List<string> highScoreValues; //public List of highScore values

	// Use this for initialization
	void Start () {
		//print out the dataPath
		Debug.Log("Path: " + Application.dataPath);

		//Create a string variable to the path of the file we want to use
		string finalFilePath = Application.dataPath + "/" + fileName;

		/*
		 * Write to a file
		 */

		//Open a streamWriter to the fiel we want to change
		//(basically, allows us to access a file in the 
		//file system for writting). Takes 2 arguments, a path
		//to a file and whether to append to the file (true) or
		//overwrite it completely (false)
		StreamWriter sw = new StreamWriter(finalFilePath, false);

		//loop through all the highScore names
		for(int i = 0; i < highScoreNames.Count; i++){
			//write a line to the file with the highSchore name, a space, and the highScore value
			//for example "Matt 100"
			sw.WriteLine(highScoreNames[i] + " " + highScoreValues[i]);
		}

		//Close the StreamWriter
		sw.Close();

		/*
		 *  Read from file
		 */

		//Create a StreamReader. Basically the same thing as the
		//StreamWriter, but for reading instead of writing.
		StreamReader sr = new StreamReader(finalFilePath);

		//A while loop is like a for loop, but it just loops
		//until whatever condition inside of it turns false.
		//This can be dangerous, even crash Unity, so be careful!
		//Is this case, we loop until we reach the end of the file.
		while(!sr.EndOfStream){

			//read one line of the file into a var "line"
			//for example, in this case something like "Matt 100"
			string line = sr.ReadLine();

			//split line up by the ' ' (space) character
			//this will make an array, with each element of
			//the array is the text that appears between ' '
			//characters
			//example ["Matt", "100"]
			string[] splitLine = line.Split(' ');

			//get the name out first part of the splitLine array ex: "Matt"
			string name = splitLine[0];

			//get the value out second part of the splitLine array ex: "100"
			string value = splitLine[1];

			Debug.Log("name: " + name);
			Debug.Log("value: " + value);

			//Add the name to the name List
			highScoreNames.Add(name);
			//Add the value to the value List
			highScoreValues.Add(value);
		}

		//Close the StreamWriter
		sr.Close();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
