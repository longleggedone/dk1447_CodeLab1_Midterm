using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ControllerScript))]//requires attached object to have necessary component, will add if not present
public class PlayerScript : MonoBehaviour {

	public static float distanceTraveled;// how far the player has moved right since the beginning

	public float deathHeight = -50f;
	public bool calledDeath = false;

	public float maxJumpHeight = 4f; //max height of player's jump
	public float minJumpHeight = 1f; //min ""
	public float timeToJumpApex = .4f; //time to reach max jump height

	public float moveSpeed; //player movement speed
	float maxJumpVelocity; //player max jump velocity
	float minJumpVelocity; // min ""
	float gravity; //downward 'force' on player
	Vector3 velocity;

//	float accelerationTimeAirborne = .2f;
//	float accelerationTimeGrounded = .1f;
//	float velocityXSmoothing;

	ControllerScript controller; //reference to controller
	// Use this for initialization
	void Start () {
		controller = GetComponent<ControllerScript>();//assigns reference
		gravity = -(2 * maxJumpHeight)/Mathf.Pow(timeToJumpApex, 2); //MATH to determine gravity based on desired jump height and time
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex; //MORE MATH to determine maximum jump velocity based on gravity and jump time
		minJumpVelocity = Mathf.Sqrt(2*Mathf.Abs(gravity) * minJumpHeight); //YET MORE MATH to determine min jump velocity god damn
		print ("Gravity: " + gravity + " Jump Velocity: " + maxJumpVelocity);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < deathHeight){
			if(!calledDeath){
				GameManagerScript.playerDead = true;
				SpeedScoreManagerScript.instance.PostScores();
			}
			calledDeath = true;
			velocity = Vector3.zero;
		
		}
		if(controller.collisions.above || controller.collisions.below){ //if colliding with something above or below
			velocity.y = 0; //reset y velocity
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")); //sets 'input' as vector2 with horizontal and vertical axes inputs

		if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below){ //if pressing space and colliding with something below
			velocity.y = maxJumpVelocity; //sets player y velocity to jump value
		}
		if (Input.GetKeyUp (KeyCode.Space)){ //if space is released
			if(velocity.y > minJumpVelocity){ //if we are moving up higher than our minimum jump velocity
				velocity.y = minJumpVelocity; //set y velocity equal to min jump velocity
			}
		}

		velocity.x = moveSpeed;
		moveSpeed += 0.01f * Time.deltaTime;
//		float targetVelocityX = input.x * moveSpeed; //sets x velocity to player move speed multiplied by input
//		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
//									(controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne); 
		velocity.y += gravity * Time.deltaTime; //applies gravity to player
		controller.Move(velocity * Time.deltaTime); //calls move function from controller script
		distanceTraveled = transform.localPosition.x;
	}
}
