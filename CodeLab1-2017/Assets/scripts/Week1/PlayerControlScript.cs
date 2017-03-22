using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour {

	//Variable for controlling speed
	//it is public, so it can been seen outside
	//of this class, including in the inspector
	public float speed = 1;

	//public keyboard keys for controlling movement
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Call the move function with a direction and a key
		Move(Vector3.up, upKey);
		Move(Vector3.down, downKey);
		Move(Vector3.left, leftKey);
		Move(Vector3.right, rightKey);
	}

	//function for moving the player
	void Move(Vector3 dir, KeyCode key){
		//if the key passed to this function was pressed
		if(Input.GetKey(key)){
			//than translate the player in the direction passed to this function
			//multiplied by the speed and the deltaTime
			transform.Translate(dir * speed * Time.deltaTime);
		}
	}
}
