using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalStream : WaterStream {
    public bool isFlippedAroundYAxis = false;

    public void SetDirection()
    {
        Quaternion quat = GetComponent<Transform>().rotation;
        Vector2 rot = Vector2.zero;
        if (isFlippedAroundYAxis)
        {rot = quat * Vector2.right; }
        else if (!isFlippedAroundYAxis)
        {rot = quat * Vector2.left; }

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
