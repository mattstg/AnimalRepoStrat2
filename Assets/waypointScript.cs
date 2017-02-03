using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointScript : MonoBehaviour {

	//possible waypoints it leads to
	public GameObject wayOne;
	public GameObject wayTwo;
	public GameObject wayThree;
	private int numberOfWaypoints = -1;
	private bool hasNextWaypoint = false;

	// Use this for initialization
	void Start () {
		if (wayOne != null)
			numberOfWaypoints++;
		
		if(wayTwo != null)
			numberOfWaypoints++;
		
		if(wayThree != null)
			numberOfWaypoints++;
		
		if (wayOne != null)
			hasNextWaypoint = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool hasNext(){
		return hasNextWaypoint;
	}

	public GameObject getNextWaypoint(){
		int rand = Random.Range ((int)0, numberOfWaypoints + 1); //range 0 - 2
		switch (rand) {
		case 0:
			return wayOne;
		case 1:
			return wayTwo;
		case 2:
			return wayThree;
		default:
			return wayOne;
		}
	}

}
