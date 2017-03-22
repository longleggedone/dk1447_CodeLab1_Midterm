using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerScript : MonoBehaviour {


	public Rigidbody rb;

	public static float distanceTraveled;// how far the player has moved right since the beginning

	public float acceleration;

	public Vector3 jumpVelocity;

	public bool touchingPlatform; //whether the player is colliding with a platform
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(touchingPlatform && Input.GetKeyDown(KeyCode.Space)){
			rb.AddForce(jumpVelocity, ForceMode.VelocityChange);
		}
		//transform.Translate(5f * Time.deltaTime, 0f, 0f);
		distanceTraveled = transform.localPosition.x; //updates travel distance as x position
	}

	void FixedUpdate(){
		if(touchingPlatform){ //when touching a platform
			rb.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration); //add force equal to acceleration in the x axis
		}
	}

	void OnCollisionEnter(){
		touchingPlatform = true;
	}

	void OnCollisionExit(){
		touchingPlatform = false;
	}

}
