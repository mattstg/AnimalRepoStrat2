﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum PlantType {Tree, Fern }
public class PlantSeeder : MonoBehaviour {

    public bool removeTreeColliders = false;
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
            if(removeTreeColliders)
            {
                Collider2D[] colis = go.GetComponentsInChildren<Collider2D>();
                for (int i = colis.Length - 1; i >= 0; i--)
                    Destroy(colis[i]);
            }
		}
		Destroy (this.gameObject);
	}

	//d.Next(files.Length)]; 

			
}
