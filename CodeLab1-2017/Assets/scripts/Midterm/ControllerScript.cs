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

	// Use this for initialization
	void Start () {
		collider = GetComponent<BoxCollider>(); //assigns reference to attached component
		CalculateRaySpacing(); //exactly what it says
	}
		

	public void Move(Vector3 velocity){
		UpdateRaycastOrigins(); //exactly what it says

		VerticalCollisions(ref velocity);

		transform.Translate(velocity);
	}

	void VerticalCollisions(ref Vector3 velocity){
		float directionY = Mathf.Sign (velocity.y); //gets direction of y velocity
		float rayLength = Mathf.Abs(velocity.y) + SKIN_WIDTH; //makes the ray equal to the y velocity, accounting for skin width
		for (int i = 0; i < verticalRayCount; i ++){ //for each ray in raycount
			Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft; //sets the ray origin, if the y direction is negative use the bottom left, else use the top left
			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x); //move the ray origin to the right accounting for spacing, iteration, and x velocity
			RaycastHit hit; //hit info for raycast
			if(Physics.Raycast(rayOrigin, Vector3.up * directionY, out hit, rayLength, collisionMask)){
				velocity.y = (hit.distance - SKIN_WIDTH) * directionY; //updates y velocity 
				rayLength = hit.distance;//to prevent conflicts between raycasts
			} //raycasts from origin, in direction, for length, using layermask

			Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);//debugging visualizer, draws raycasts in scene view

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
	

}
