using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManagerScript : MonoBehaviour {

	public Transform prefab; //the prefab to be spawned
	public Vector3 minSize, maxSize;//vectors for min and max scale of spawned objects
	public Vector3 minGap, maxGap; //vectors for min and max possible gap distance between platforms
	public float minY, maxY; //min and max y elevations of platforms

	public int numberOfObjects; //number of objects that will be spawned
	public float recycleOffset; //offset determines how far left of player the object should recycle

	public Vector3 startPos; //point to begin spawning objects
	private Vector3 nextPos; //point to spawn the next object

	private Queue<Transform> objectQueue; 
	// Use this for initialization
	void Start () {
		objectQueue = new Queue<Transform>(numberOfObjects); 
		for(int i = 0; i < numberOfObjects; i ++){ //for the each of the given number of objects to be spawned
			objectQueue.Enqueue((Transform)Instantiate(prefab)); //spawn the object and place it in queue
		}
		nextPos = startPos; //sets next spawn position to be the start position
		for(int i = 0; i < numberOfObjects; i ++){ //calls recycle function to randomize objects at start
			Recycle();
		}
	}

	// Update is called once per frame
	void Update () {
		if (objectQueue.Peek().localPosition.x + recycleOffset < PlayerScript.distanceTraveled){ //if the object at the start of the queue is far enough left of the player
			Recycle(); //calls recycle function
		}
	}

	private void Recycle () {
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));//sets new vector3 'scale' with random values between min and max sizes

		Vector3 position = nextPos; //sets new vector3 'position' to equal that of the next spawn position
		position.x += scale.x * 0.5f; // takes the x of position and adds half of x scale
		position.y += scale.y * 0.5f; //does the same for y

		Transform spawn = objectQueue.Dequeue();
		spawn.localScale = scale; //sets the individual spawned object scale to the random values of 'scale'
		spawn.localPosition = position; //sets the position of the spawned object to equal the next spawn position
		//nextPos.x += scale.x; //adds the x scale to the x of the next spawn position 
		objectQueue.Enqueue(spawn); //places the object in the queue

		nextPos += new Vector3(
			Random.Range(minGap.x, maxGap.x) + scale.x,
			Random.Range(minGap.y, maxGap.y),
			Random.Range(minGap.z, maxGap.z)); //adds randomized gap values to the next spawn position

		if(nextPos.y < minY){ //if the next spawn position is too low
			nextPos.y = minY + maxGap.y; //raise it 
		}
		else if(nextPos.y > maxY){ //if the next spawn position is too high
			nextPos.y = maxY - maxGap.y; //lower it
		}
	}
}
