using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject newFish = Instantiate (Resources.Load ("Prefabs/Salmon")) as GameObject;
		newFish.transform.SetParent (GetComponentInParent<FlockManager> ().gameObject.transform);
		newFish.AddComponent<AutoWPAssigner> ();
		newFish.GetComponent<Fish> ().enabled = true;
		newFish.GetComponent<FlockingAI> ().enabled = true;
		newFish.GetComponent<Animator> ().enabled = true;
		Destroy (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
