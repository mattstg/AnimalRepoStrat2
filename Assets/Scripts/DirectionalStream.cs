using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalStream : WaterStream {
    public void SetDirection()
    {
        Quaternion quat = GetComponent<Transform>().rotation;
        Vector2 rot = quat * Vector2.right;
        
        GetComponent<WaterStream>().dirOfStream = rot;
    }

	// Use this for initialization
	void Start () {
        SetDirection();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
