using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIM : InputManager {

	protected override void MouseDown (Vector2 mouseWorldPos)
	{
		Debug.Log ("mouse down");
	} 

	protected override void MouseClickedOnObjOfInterest ()
	{
        Debug.Log("mouse clicked on fish");
	}

}
