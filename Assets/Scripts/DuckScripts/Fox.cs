﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour {

	enum FoxState { Idle, FocusedOnGoal} //idle for wander, focused on goal for returning or chasing
	public GameObject waypointParent;
	List<Vector2> waypoints;
	float patrolBoxSize = 4;
	Vector2 patrolOrigin;
	FoxState currentState = FoxState.Idle;
	public Vector2 goalPos;
	Transform chaseTarget;
	float foxWalkSpeed = 2;
	float foxRunSpeed = 5;
	float foxCurrentSpeed = 2;
	Transform caughtYoung;
	float idleWaitTime = 3;
	int currentWaypointIndex = 0;
	float timeWandering; //used to stop from getting stuck

	// Use this for initialization
	void Awake()
	{
		goalPos = patrolOrigin = transform.position;
		waypoints = new List<Vector2> ();
		foreach (Transform t in waypointParent.transform)
			{
				waypoints.Add (t.position);
				//Destroy (t.gameObject);
			}
		
	}

	
	// Update is called once per frame
	void Update () {
		timeWandering += Time.deltaTime;

		if (!chaseTarget) {
			if (Vector2.Distance (goalPos, transform.position) < .1f) {
				timeWandering = 0;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
				if (!chaseTarget) {	
					idleWaitTime -= Time.deltaTime;
					if (idleWaitTime <= 0) {
						idleWaitTime = 3;
						GetNewWanderPoint ();
					}
				}
			} else {
				MoveTowardsGoal ();
			}
		} else {
			MoveTowardsGoal ();
		}

		if (timeWandering >= 7 && !chaseTarget) {
			GetNewWanderPoint ();
			//timeWandering
		}
	}

	private void GetNewWanderPoint()
	{
		currentWaypointIndex++;
		currentWaypointIndex %= waypoints.Count;
		goalPos = waypoints [currentWaypointIndex];
	}

	public void OnCollisionEnter2D(Collision2D coli)
	{
		if (coli.gameObject.GetComponent<Duckling> ()) {
			Debug.Log ("duck collision");
		}
	}

	public void OnTriggerEnter2D(Collider2D coli)
	{
		if (coli.GetComponent<Duckling> () && !chaseTarget) {
			foxCurrentSpeed = foxRunSpeed;
			chaseTarget = coli.transform;
		}

	}

	public void OnTriggerExit2D(Collider2D coli)
	{
		if (coli.GetComponent<Duckling> () && coli.GetComponent<Duckling> () == chaseTarget) {
			chaseTarget = null;
			foxCurrentSpeed = foxWalkSpeed;
			GetNewWanderPoint ();
		}

	}

	private void MoveTowardsGoal()
	{
		if (chaseTarget)
			goalPos = chaseTarget.position;
		float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, goalPos);
		float distanceToGoal = Vector2.Distance (goalPos, transform.position);
		transform.eulerAngles = new Vector3 (0, 0, angToGoal - 90);
		Vector2 goalDir = goalPos - MathHelper.V3toV2(transform.position);
		float speed = foxCurrentSpeed;
		if (distanceToGoal < foxCurrentSpeed * Time.deltaTime)
			speed = distanceToGoal;
		//GetComponent<Rigidbody2D> ().AddRelativeForce (goalDir.normalized * foxCurrentSpeed * Time.deltaTime,ForceMode2D.Impulse);
		GetComponent<Rigidbody2D> ().velocity = goalDir.normalized * speed;
		//transform.position = Vector2.MoveTowards(transform.position, goalPos, foxCurrentSpeed * Time.deltaTime);
	}
}
