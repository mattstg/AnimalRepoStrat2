using UnityEngine; using LoLSDK;
using System.Collections;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour
{
	public bool isInFishScene = false;
	public List<Vector2> waypointPositions = new List<Vector2>();
	public List<staticWaypoint> waypoints = new List<staticWaypoint> ();



	// Use this for initialization
	void Awake()
	{
		if (isInFishScene) {
			foreach (waypointScript w in GetComponentsInChildren<waypointScript>()) {
				w.toStatic ();
			}
			foreach (waypointScript w in GetComponentsInChildren<waypointScript>()) {
				w.setWays ();
				waypoints.Add (w.toStatic ());
			}
			foreach (waypointScript w in GetComponentsInChildren<waypointScript>()) {
				w.gameObject.SetActive (false);
			}
		}
			


		foreach (Transform t in GetComponentsInChildren<Transform>()) {
			waypointPositions.Add (t.position);
		}
		//waypointPositions.AddRange(Get

	}

	// Update is called once per frame
	void Update()
	{

	}
}