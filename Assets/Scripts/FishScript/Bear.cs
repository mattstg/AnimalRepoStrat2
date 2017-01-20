using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour {

	public List<Transform> bearSwipes;
	public Transform swipeParent;
	// Use this for initialization
	void Awake() {
		bearSwipes = new List<Transform> ();
		foreach (Transform t in swipeParent) {
			bearSwipes.Add (t);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
