using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLoader : MonoBehaviour {
	public bool reallowSpawn = false;
	public GameObject salmonManager;

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
		Debug.Log ("Here!!");
		GameObject newFish = Instantiate (Resources.Load ("Prefabs/Salmon")) as GameObject;
		newFish.transform.SetParent (salmonManager.transform);
		newFish.transform.position = gameObject.transform.position;
		//newFish.AddComponent<AutoWPAssigner> ();
		newFish.GetComponent<Fish> ().enabled = true;
		newFish.GetComponent<FlockingAI> ().enabled = true;
		newFish.GetComponent<Animator> ().enabled = true;
		reallowSpawn = false;
	}

}
