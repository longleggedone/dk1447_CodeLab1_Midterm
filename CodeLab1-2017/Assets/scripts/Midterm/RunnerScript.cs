using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerScript : MonoBehaviour {

	public static float distanceTraveled;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(5f * Time.deltaTime, 0f, 0f);
		distanceTraveled = transform.localPosition.x;
	}
}
