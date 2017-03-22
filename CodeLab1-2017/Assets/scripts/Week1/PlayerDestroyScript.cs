using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyScript : MonoBehaviour {

	//public variable for the distance from the goal
	public float radius = 4.5f;

	//private var (only available in this script) for the
	//goal gameObject
	private GameObject goal;

	// Use this for initialization
	void Start () {
		//Set the goal to what every gameObject in the scene
		//has the name "Goal"
		goal = GameObject.Find("Goal");
	}
	
	// Update is called once per frame
	void Update () {

		//if the distance from the goal to the gameObject this script is attached to
		//is greater than the radius
		if(Vector3.Distance(goal.transform.position, transform.position) > radius){
			//Destroy the gameObject his is attached to
			Destroy(gameObject);
			//Print out message about the gameObject dying
			Debug.Log(gameObject.name + " DIED!!! OH NOOO!!!!");
		}
	}
}
