using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMoveScript : MonoBehaviour {

	public float moveMod;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Move the GameObject this script is attached to
		//using random values
//		transform.Translate(new Vector3(
//			Random.Range(-moveMod, moveMod), 
//			Random.Range(-moveMod, moveMod),
//			0));

		//Move the GameObject this is attackhed to using Perlin Noise
		//https://docs.unity3d.com/ScriptReference/Mathf.PerlinNoise.html
		transform.Translate(new Vector3(
			moveMod * (Mathf.PerlinNoise(Time.timeSinceLevelLoad, 0) - 0.5f) * 2,
			moveMod * (Mathf.PerlinNoise(0, Time.timeSinceLevelLoad) - 0.5f) * 2,
			0));
	}
}
