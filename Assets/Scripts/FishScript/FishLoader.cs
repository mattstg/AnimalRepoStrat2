using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLoader : MonoBehaviour {
	private bool reallowSpawn = true;
	public GameObject salmonManager;
	public CircleCollider2D triggerCol;

	// Use this for initialization
	void Start () {
		spawnFish ();
	}
	
	// Update is called once per frame
	void Update () {
		if (reallowSpawn) {
			spawnFish ();
		}
	}

	private void spawnFish(){
		GameObject newFish = Instantiate (Resources.Load ("Prefabs/Salmon")) as GameObject;
		newFish.transform.SetParent (salmonManager.transform);
		newFish.transform.position = gameObject.transform.position;
		//newFish.AddComponent<AutoWPAssigner> ();
		newFish.GetComponent<Fish> ().enabled = true;
		newFish.GetComponent<FlockingAI> ().enabled = true;
		newFish.GetComponent<Animator> ().enabled = true;
		reallowSpawn = false;
		triggerCol.enabled = false;
	}

	public void reAllowSpawn(){
		reallowSpawn = true;
		triggerCol.enabled = true;
	}

}
