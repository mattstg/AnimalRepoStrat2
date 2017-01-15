using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleBowerBird : BowerBird {
	public BowerListManager linkToBowers;
	private Bower nextBower;
	private float[] bowerRating;
	private float evaluationTime = 0;
	private Vector2 highestRating = new Vector2(0,0);

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
			Bower currentBower = other.GetComponent<Bower> ();
			bowerRating[linkToBowers.i] = currentBower.returnRating ();
			highestRating.x = (highestRating.x < bowerRating [linkToBowers.i]) ? bowerRating[linkToBowers.i] : highestRating.x;
			highestRating.y = (highestRating.x < bowerRating [linkToBowers.i]) ? linkToBowers.i : highestRating.y;
			if (bowerRating [linkToBowers.i] > BowerGV.pointsToWIN) {
				//WIN GAME
				BowerGF gameMan = GameObject.FindObjectOfType<BowerGF>();
				gameMan.GameFinished (currentBower.owner,highestRating.x);
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
