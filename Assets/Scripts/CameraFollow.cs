using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class CameraFollow : MonoBehaviour {

    public float dampTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    public Transform toFollow;
    public Vector3 offset;

	// Update is called once per frame
	void Update () {
		if (toFollow) {
            //transform.position = new Vector3(toFollow.position.x + offset.x, toFollow.position.y + offset.y, -10);

            Vector3 target = new Vector3(toFollow.position.x + offset.x, toFollow.position.y + offset.y, -10);
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target);
            Vector3 delta = target - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
	}

	public void SetZoom(float z)
	{
		GetComponent<Camera> ().orthographicSize = z;
	}
}
