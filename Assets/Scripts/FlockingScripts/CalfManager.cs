using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalfManager : MonoBehaviour {


	public List<Vector2> calfPositions = new List<Vector2> ();
	public List<Transform> calfTransforms = new List<Transform> ();
	int numOfCalves;

	// Use this for initialization
	void Start () {
	foreach (Transform t in this.transform) 
		{

			calfPositions.Add (t.position);
			calfTransforms.Add (t);
		}
		numOfCalves = calfPositions.Count;
		
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < numOfCalves; i++) {
			calfPositions [i] = calfTransforms [i].position;

		}
	}
}
