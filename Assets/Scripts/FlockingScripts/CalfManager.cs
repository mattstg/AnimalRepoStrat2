using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfManager : MonoBehaviour {


	
	public List<Transform> calfTransforms = new List<Transform> ();
	

	// Use this for initialization
	void Start () {
	foreach (Transform t in this.transform) 
		{
			calfTransforms.Add (t);
		}
		 
		
	}
	
	// Update is called once per frame
	void Update () {
		

		}
	}

