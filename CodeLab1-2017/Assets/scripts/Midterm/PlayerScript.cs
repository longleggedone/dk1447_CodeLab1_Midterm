using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ControllerScript))]//requires attached object to have necessary component, will add if not present
public class PlayerScript : MonoBehaviour {

	float moveSpeed = 6f; //palyer movement speed
	float gravity = -20f; //downward 'force' on player
	Vector3 velocity;

	ControllerScript controller; //reference to controller
	// Use this for initialization
	void Start () {
		controller = GetComponent<ControllerScript>();//assigns reference
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")); //sets input as vector2 with horizontal and vertical axes

		velocity.x = input.x * moveSpeed; //sets x velocity to player move speed multiplied by input
		velocity.y += gravity * Time.deltaTime; //applies gravity to player
		controller.Move(velocity * Time.deltaTime);
	}
}
