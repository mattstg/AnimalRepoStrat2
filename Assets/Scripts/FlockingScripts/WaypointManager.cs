using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
	public List<Vector2> waypointPositions = new List<Vector2>();


	// Use this for initialization
	void Start()
	{
		Vector2 position;
		foreach(Transform t in GetComponentsInChildren<Transform>())
			{
			position = t.position;
			waypointPositions.Add (position);
			//Debug.Log (position.x + " " + position.y);
			}
		//waypointPositions.AddRange(Get

	}

	// Update is called once per frame
	void Update()
	{

	}
}