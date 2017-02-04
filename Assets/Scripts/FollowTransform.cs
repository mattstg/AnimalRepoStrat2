using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour {

    public Transform toFollow;

	public void Update()
    {
        if (toFollow)
            transform.position = new Vector3(toFollow.position.x, toFollow.position.y, 0);
    }
}
