﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum PlantType {Tree, Fern }
public class PlantSeeder : MonoBehaviour {

	int treeCount = 5;
	int fernCount = 0;
	// Use this for initialization
	void Start () {
		GameObject treeParentGO = new GameObject ();
		treeParentGO.name = "TreeParent";
		Transform treeParent = treeParentGO.transform;

		foreach (Transform t in transform) {
			string toLoad = "Prefabs/Plants/";
			//bool isFern = (Random.Range (0, 1f) > .5f);
			bool isFern = false;
			toLoad += (!isFern)?"Tree":"Fern";
			if(isFern)
				toLoad += Random.Range (0, treeCount).ToString();
			else
				toLoad += Random.Range (0, treeCount).ToString();
			Debug.Log ("To load: " + toLoad);
			GameObject go = Instantiate (Resources.Load (toLoad), t.transform.position,Quaternion.identity) as GameObject;
			go.transform.SetParent (treeParent);
		}
		Destroy (this.gameObject);
	}

	//d.Next(files.Length)]; 

			
}
