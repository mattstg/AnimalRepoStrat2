using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class waypointScript : MonoBehaviour {

	//possible waypoints it leads to
	public GameObject wayOne;
	public GameObject wayTwo;
	public GameObject wayThree;
	private int numberOfWaypoints = -1;
	private bool hasNextWaypoint = false;
	public staticWaypoint self;
	public staticWaypoint[] ways = new staticWaypoint[3];

	// Use this for initialization
	void Start () {	}

	public void Initial(){
		if (wayOne != null) {
			numberOfWaypoints++;
		}

		if (wayTwo != null) {
			numberOfWaypoints++;
		}

		if (wayThree != null) {
			numberOfWaypoints++;
		}

		if (wayOne != null)
			hasNextWaypoint = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public staticWaypoint toStatic(){
		if(self == null){
			Initial ();
			self = new staticWaypoint (ways, this.transform.position.x, this.transform.position.y,hasNextWaypoint,numberOfWaypoints);
			return self;
		}else{
			return self;
		}
	}

	public void setWays(){
		if (wayOne != null) {
			ways [0] = wayOne.GetComponent<waypointScript> ().toStatic();
		}

		if (wayTwo != null) {
			ways [1] = wayTwo.GetComponent<waypointScript> ().toStatic();
		}

		if (wayThree != null) {
			ways [2] = wayThree.GetComponent<waypointScript> ().toStatic();
		}
		self = toStatic ();
	}

}


