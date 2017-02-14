using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowerDirection : MonoBehaviour {
	public PlayerBowerBird player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rotate ();
	}

	private void rotate(){
		Vector3 dirVector = player.bower.transform.position - player.transform.position;
		float angle = Mathf.Atan2 (dirVector.y, dirVector.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle + 270,Vector3.forward);
		transform.rotation = q;
	}

}
