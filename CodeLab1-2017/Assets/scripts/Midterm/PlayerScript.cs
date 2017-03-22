using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ControllerScript))]//requires attached object to have necessary component, will add if not present
public class PlayerScript : MonoBehaviour {

	float gravity = -20; //downward 'force' on player
	Vector3 velocity;

	ControllerScript controller; //reference to controller
	// Use this for initialization
	void Start () {
		controller = GetComponent<ControllerScript>();//assigns reference
	}
	
	// Update is called once per frame
	void Update () {
		velocity.y += gravity * Time.deltaTime; //applies gravity to player
		controller.Move(velocity * Time.deltaTime);
	}
}
