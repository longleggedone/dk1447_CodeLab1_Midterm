using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using SimpleJSON;

public class Wk6Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Use UtilScript to write to a file in one line
		UtilScript.WriteStringToFile(Application.dataPath, "hello.txt", "hi!");

		//Use UtilScript to clone and mod a Vector3
		transform.position = UtilScript.CloneModVector3(transform.position, 1, 0, 0);

		//Use UtilScript to clone a Vector3
		Vector3 pos = UtilScript.CloneVector3(transform.position);

		//Create a JSONClass object
		JSONClass subClass = new JSONClass();

		//Add a value to subClass
		subClass["test"] = "value";

		//Create another JSONClass object, must include "using SimpleJSON" above
		JSONClass json = new JSONClass();

		//Add floats, strings, bools, even other classes to our json object
		json["x"].AsFloat = 7;
		json["y"].AsFloat = 0;
		json["z"].AsFloat = 2;
		json["name"] = "Matt";
		json["Alt Facts"].AsBool = false;
		json["sub"] = subClass;

		//Write "json" to a file using UtilScript
		UtilScript.WriteStringToFile(Application.dataPath, "file.json", json.ToString());

		//print out the string value of "json"
		Debug.Log(json);

		//Read in a file into a string using UtilScript
		string result = UtilScript.ReadStringFromFile(Application.dataPath, "file.json");

		//Parse string we read in from a file into a JSONNode
		JSONNode readJSON = JSON.Parse(result);

		//print out a value from the JSONNode
		Debug.Log(readJSON["z"].AsFloat);

		//Create a webclient, must include "using System.Net" above
		WebClient client = new WebClient();
		//Get the content from a webpage, in this case, a json value for the sunset time in Hawaii
		string content = client.DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20astronomy.sunset%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22maui%2C%20hi%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");

		//turn string into a JSONNode
		JSONNode hawaii = JSON.Parse(content);

		//Get the sunset time from the JSONNode
		string sunset = hawaii["query"]["results"]["channel"]["astronomy"]["sunset"];

		//print out the sunset time
		print(sunset);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
