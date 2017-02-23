using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowerDirection : MonoBehaviour {
	public PlayerBowerBird player;
    public Camera cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rotate ();
	}

	private void rotate()
    {
        Vector3 bowerPos = cam.GetComponent<Camera>().WorldToViewportPoint(player.bower.transform.position);
        Vector3 arrowPos = cam.GetComponent<Camera>().ScreenToViewportPoint(transform.position);
        Vector3 dirVector = bowerPos - arrowPos;
//      Debug.Log(player.bower.transform.position + ", " + transform.position + ", " + bowerPos + ", " + arrowPos + ", " + dirVector);

//      Vector3 dirVector = player.bower.transform.position - player.transform.position;

		float angle = Mathf.Atan2 (dirVector.y, dirVector.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle + 270,Vector3.forward);
		transform.rotation = q;
	}

}
