using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManagerScript : MonoBehaviour {

	public Transform prefab;
	public Vector3 minSize, maxSize;

	public int numberOfObjects;
	public float recycleOffset;

	public Vector3 startPos;
	private Vector3 nextPos;

	private Queue<Transform> objectQueue;
	// Use this for initialization
	void Start () {
		objectQueue = new Queue<Transform>(numberOfObjects);
		for(int i = 0; i < numberOfObjects; i ++){
			objectQueue.Enqueue((Transform)Instantiate(prefab));
		}
		nextPos = startPos;
		for(int i = 0; i < numberOfObjects; i ++){
			Recycle();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (objectQueue.Peek().localPosition.x + recycleOffset < RunnerScript.distanceTraveled){
			Recycle();
		}
	}

	private void Recycle () {
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));

		Vector3 position = nextPos;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;

		Transform spawn = objectQueue.Dequeue();
		spawn.localScale = scale;
		spawn.localPosition = position;
		nextPos.x += scale.x;
		objectQueue.Enqueue(spawn);
	}
}
