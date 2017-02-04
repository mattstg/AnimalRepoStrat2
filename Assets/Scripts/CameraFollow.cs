using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform toFollow;
    public Vector3 offset;

	// Update is called once per frame
	void Update () {
		if (toFollow) {
			transform.position = new Vector3(toFollow.position.x + offset.x, toFollow.position.y + offset.y, -10);
		}
	}

	public void SetZoom(float z)
	{
		GetComponent<Camera> ().orthographicSize = z;
	}
}
