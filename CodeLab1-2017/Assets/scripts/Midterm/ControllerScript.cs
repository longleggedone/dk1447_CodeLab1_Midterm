using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))] //requires attached object to have necessary component, will add if not present
public class ControllerScript : MonoBehaviour {

	public LayerMask collisionMask; //layermask for raycast detection

	const float SKIN_WIDTH = .015f; //distance from inside the object the rays will originate
	public int horizontalRayCount = 4; //number of horizontal raycasts
	public int verticalRayCount = 4; // number of vertical raycasts

	float horizontalRaySpacing; //horizontal distance between raycasts
	float verticalRaySpacing; //vertical ""

	BoxCollider collider; //collider reference
	RaycastOrigins raycastOrigins; //instance of struct for raycast origin points
	public CollisionInfo collisions;

	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider>(); //assigns reference to attached component
		CalculateRaySpacing(); //exactly what it says
	}
		

	public void Move(Vector3 velocity){
		UpdateRaycastOrigins(); //exactly what it says
		collisions.Reset(); //resets collision info bools


		if (velocity.x != 0){ //if moving on the x axis
			HorizontalCollisions(ref velocity); //check for horizontal collisions
		}
		if (velocity.y != 0){ //if moving on the y axis
			VerticalCollisions(ref velocity); //check for vertical collisions
		}
		transform.Translate(velocity); //move player by velocity
	}

	void HorizontalCollisions(ref Vector3 velocity){
		float directionX = Mathf.Sign (velocity.x); //gets direction of x velocity
		float rayLength = Mathf.Abs(velocity.x) + SKIN_WIDTH; //makes the ray equal to the x velocity, accounting for skin width
		for (int i = 0; i < horizontalRayCount; i ++){ //for each ray in raycount
			Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight; //sets the ray origin, if the x direction is negative use the bottom left, else use the bottom right
			rayOrigin += Vector2.up * (horizontalRaySpacing * i); //move the ray origin to the right accounting for spacing and iteration
			RaycastHit hit; //hit info for raycast
			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);//debugging visualizer, draws raycasts in scene view

			if(Physics.Raycast(rayOrigin, Vector3.right * directionX, out hit, rayLength, collisionMask)){
				velocity.x = (hit.distance - SKIN_WIDTH) * directionX; //updates x velocity 
				rayLength = hit.distance;//to prevent conflicts between raycasts
				collisions.left = directionX == -1; //if we hit something while going left, set collision info left bool to true
				collisions.right = directionX == 1; //same for right bool
			} //raycasts from origin, in direction, for length, using layermask


		}
	}

	void VerticalCollisions(ref Vector3 velocity){
		float directionY = Mathf.Sign (velocity.y); //gets direction of y velocity
		float rayLength = Mathf.Abs(velocity.y) + SKIN_WIDTH; //makes the ray equal to the y velocity, accounting for skin width
		for (int i = 0; i < verticalRayCount; i ++){ //for each ray in raycount
			Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft; //sets the ray origin, if the y direction is negative use the bottom left, else use the top left
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x); //move the ray origin to the right accounting for spacing, iteration, and x velocity
			RaycastHit hit; //hit info for raycast
			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
			if(Physics.Raycast(rayOrigin, Vector3.up * directionY, out hit, rayLength, collisionMask)){
				velocity.y = (hit.distance - SKIN_WIDTH) * directionY; //updates y velocity 
				rayLength = hit.distance;//to prevent conflicts between raycasts
				collisions.below = directionY == -1; //if we hit something while going up, set collision info below bool to true
				collisions.above = directionY == 1; //same for above bool

			} //raycasts from origin, in direction, for length, using layermask

			//Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);//debugging visualizer, draws raycasts in scene view

//			if(hit != null){ //if the raycast hits
//				velocity.y = (hit.distance - SKIN_WIDTH) * directionY; //updates y velocity 
//				rayLength = hit.distance;//to prevent conflicts between raycasts
//			}
		}
	}

	void UpdateRaycastOrigins(){  //updates the origin points of the raycasts
		Bounds bounds = collider.bounds; //sets a new bounds to the same size as the collider's bounding box
		bounds.Expand(SKIN_WIDTH * -2); //'expands' the bounds by a negative number, effectively shrinking the new bounding box to be smaller than the collider's

		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y); //assigns corners of bounding box as new vectors
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	void CalculateRaySpacing(){ //determines the distance between raycasts
		Bounds bounds = collider.bounds; //*see UpdateRaycastOrigins above
		bounds.Expand(SKIN_WIDTH * -2);

		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue); //clamps the raycount to a minimum of 2
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount -1); //sets the distance between raycasts using the size of the bounding box
		verticalRaySpacing = bounds.size.x /(verticalRayCount -1);
	}

	struct RaycastOrigins{ //struct for determining raycast origin points
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

	public struct CollisionInfo{ //bools for direction of collision
		public bool above, below;
		public bool left, right;

		public void Reset(){ 
			above = below = false;
			left = right = false;
		}
	}

}
