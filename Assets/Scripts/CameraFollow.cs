using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform toFollow;
    public Vector3 offset;

	// Update is called once per frame
	void Update () {
		if (toFollow) {
			transform.position = toFollow.position + new Vector3(0,0,-10) + offset;
		}
	}

	public void SetZoom(float z)
	{
		GetComponent<Camera> ().orthographicSize = z;
	}
}
