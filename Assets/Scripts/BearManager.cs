using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearManager : MonoBehaviour {
	public Transform PlayerFish;
	public float pauseDistance = 15;
	private float timeBetweenDistChecks = 0.5f;
	public float lastTimeSinceCheck = 0;
	List<Bear> Bears = new List<Bear>();
	// Use this for initialization
	void Start () {
		Bears.AddRange(GetComponentsInChildren<Bear> ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > lastTimeSinceCheck + timeBetweenDistChecks) {
			lastTimeSinceCheck = Time.time;
			foreach (Bear b in Bears)
				if (Vector3.Distance (PlayerFish.position, b.transform.position) < pauseDistance) {
					b.paused = false;
				} else {
					b.paused = true;
				}
		}
	}
}
