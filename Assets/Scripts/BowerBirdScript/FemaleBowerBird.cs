using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleBowerBird : BowerBird {
	public BowerListManager linkToBowers;
	private Bower nextBower;
	private float[] bowerRating;
	private float evaluationTime = 0;

	void Start(){
		ParentStart ();
		canGetBored = false;
		bowerRating = new float[linkToBowers.count];
		VisistFirstBower ();
	}

	void Update(){
		ParentUpdate ();
		if (idleTime > BowerGV.bowerInstepctionTime) {
			VisitNextBower ();
		} else if (!isMoving) {
			idleTime += Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Item")) {
			/*
			Item colItem = other.GetComponent<Item>();
			if (!isHolding && autoPickup && altitude == 0) {
				colItem.getPickedUp (this);
				isHolding = true;
				holding = colItem;
			} */
		} else if (other.CompareTag ("BowerBird")) {
			//BowerBird colBird = other.GetComponent<BowerBird>();
		} else if (other.CompareTag ("Bower")) {
			//Bower colBower = other.GetComponent<Bower> ();
		//	isMoving = false;
		//	body.velocity = Vector2.zero;
			bowerRating[linkToBowers.i] = other.GetComponent<Bower> ().returnRating ();
			if (bowerRating [linkToBowers.i] > BowerGV.pointsToWIN) {
				//WIN GAME
				Debug.Log("Winner!!");
			}
			//Debug.Log ("bower rated: " + bowerRating [linkToBowers.i]);
		}
	}

	public void VisitNextBower(){
		idleTime = 0;
		nextBower = linkToBowers.getNextBower ();
		MoveTo (nextBower.transform.position);
		isMoving = true;
	}

	public void VisistFirstBower(){
		idleTime = 0;
		nextBower = linkToBowers.getBower (linkToBowers.i);
		MoveTo (nextBower.transform.position);
		isMoving = true;
	}


}
