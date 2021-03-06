﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;
using System.IO;

public enum PlantType {Tree, Fern }
public class PlantSeeder : MonoBehaviour {

	public bool isTree = true;
    public bool triggerColliderTrees = false;
	int treeCount = 3;
	int fernCount = 2;
	// Use this for initialization
	void Awake () {
		GameObject treeParentGO = new GameObject ();
		treeParentGO.name = "TreeParent";
		Transform treeParent = treeParentGO.transform;

		foreach (Transform t in transform) {
			string toLoad = "Prefabs/Plants/";
			//bool isFern = (Random.Range (0, 1f) > .5f);
			toLoad += (isTree)?"Tree":"Fern";
			if(isTree)
				toLoad += Random.Range (0, treeCount).ToString();
			else
				toLoad += Random.Range (0, fernCount).ToString();
			GameObject go = Instantiate (Resources.Load (toLoad), t.transform.position,Quaternion.identity) as GameObject;
			go.transform.SetParent (treeParent);
			if (triggerColliderTrees && go.GetComponent<Collider2D>())
                go.GetComponent<Collider2D>().isTrigger = true;
		}
        if (GameObject.FindObjectOfType<SpriteActivator>())
        {
            if (!isTree)
                GameObject.FindObjectOfType<SpriteActivator>().fernParent = treeParent;
            else
                GameObject.FindObjectOfType<SpriteActivator>().treeParent = treeParent;
        }
        Destroy (this.gameObject);
	}

	//d.Next(files.Length)]; 

			
}