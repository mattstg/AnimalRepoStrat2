﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum PlantType {Tree, Fern }
public class PlantSeeder : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GameObject treeParentGO = new GameObject ();
		Transform treeParent = treeParentGO.transform;

		foreach (Transform t in transform) {
			string toLoad = "Prefabs/Plants/" +  GetRandPlant();
			Debug.Log ("toLoad: " + toLoad);
			GameObject go = Instantiate (Resources.Load (toLoad), treeParent) as GameObject;
			go.transform.position = new Vector3 ();
		}
		Destroy (this.gameObject);

	}


	public string GetRandPlant()  //count of files that contains tree or plant individ 
	{
		// Add file sizes.
		bool valid = true;
		string fileRetrieved = "";
		//string[] files = Directory.GetFiles("Asset/Resources/Prefabs/Plants","*.prefab");
		string[] files = Directory.GetFiles("");
		string filepath = files [Random.Range (0, (files.Length))];
		Debug.Log ("filepath: " + filepath);
		return Path.GetFileNameWithoutExtension (filepath);
	}

	d.Next(files.Length)]; 

			
}
