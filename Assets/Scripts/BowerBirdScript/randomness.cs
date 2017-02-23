using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class randomness : MonoBehaviour {

	public Vector2 randomScale = new Vector2 (-.4f, .4f);
    private float scaleFactor;
    // Use this for initialization
	void Start () {
		Transform mink = GetComponent<Transform> ();
		mink.Rotate(new Vector3(0,0, Random.Range (0, 360)));
        scaleFactor = 1 + Random.Range(randomScale.x, randomScale.y);
        mink.localScale = new Vector3(mink.localScale.x * scaleFactor, mink.localScale.y * scaleFactor, 1);
        //mink.localScale = mink.localScale + mink.localScale * Random.Range(randomScale.x,randomScale.y);
        Destroy(this); //no longer need script
	}

}
