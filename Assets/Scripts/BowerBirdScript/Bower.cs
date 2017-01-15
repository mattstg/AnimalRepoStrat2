using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bower : MonoBehaviour {
	public BowerBird owner;
	float collectionRating = 0f;
	List<Item> collection;
	Rigidbody2D bowerInfluence;

	// Use this for initialization
	void Start () {
		bowerInfluence = GetComponent<Rigidbody2D>();
		collection = new List<Item> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log ("something" + other.name);
		if (other.CompareTag ("Item")) {
			//Debug.Log ("item added");
			Item colItem = other.GetComponent<Item>();
			addItemToCollection (colItem);
		} else if (other.CompareTag ("BowerBird")) {
			//Debug.Log ("heh");
			BowerBird colBird = other.GetComponent<BowerBird>();
			if (colBird == owner) {
				//welsome home
				} else {
				//enemy in your nest!
			}
		} else if (other.CompareTag ("Bower")) {
			//Bower colBower = other.GetComponent<Bower> ();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("Item")) {
			Item colItem = other.GetComponent<Item>();
			removeItemFromCollection (colItem);
		} else if (other.CompareTag ("BowerBird")) {
			//BowerBird colBird = other.GetComponent<BowerBird>();
		} else if (other.CompareTag ("Bower")) {
			//Bower colBower = other.GetComponent<Bower> ();
		}
	}
		
	public void rateCollection(){
		float value = 0;
		int[] colors = new int[4];
		foreach (Item i in collection){ // depending on item it is you get between 1 and 5 points
			colors [(int)i.itemColor]++;
			value += (int)i.itemType + 1;
		}

		int totalItems = collection.Count;
		float bonusSetScore = 1;
		if (totalItems != 0) {
			foreach (int c in colors) { //for every additional additional object of same color you get 2 points
				if (c / totalItems > .51) {
					bonusSetScore += c / totalItems;
				}
			}
			//Debug.Log ("Base Score " + value);
			//Debug.Log ("bonus set score = " + bonusSetScore + "%");
			//Debug.Log ("Total score " + value * bonusSetScore);
			collectionRating = value * bonusSetScore;
		} else {
			collectionRating = 0;
		}
	}

	public void addItemToCollection(Item toAdd){
		//should place an object around the bower in the bowerInflunceZone, maybe where dropped
		collection.Add(toAdd);
	}

	public Item removeItemFromCollection(Item toRemove){
		collection.Remove (toRemove);
		return toRemove;
	}

	public float returnRating(){
		rateCollection ();
		return collectionRating;
	}

	public bool isOwnerNear(){
		return Vector2.Distance (owner.transform.position, transform.position) < BowerGV.BOWERHOMEDISTANCE;
	}
}
