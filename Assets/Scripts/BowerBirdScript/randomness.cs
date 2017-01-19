using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomness : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Transform mink = GetComponent<Transform> ();
		mink.Rotate(new Vector3(0,0, Random.Range (0, 360)));
		mink.localScale = mink.localScale + mink.localScale * Random.Range(-0.4f,0.4f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
