using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIM : InputManager {
	public PlayerFish pF;

	protected override void MouseDown (Vector2 mouseWorldPos)
	{
	//	Debug.Log ("mouse down");
		if (pF != null) {
			pF.isMoving = true;
			pF.PlayerMoveTo (mouseWorldPos);
		}
	} 

	protected override void MouseClickedOnObjOfInterest ()
	{
        Debug.Log("mouse clicked on fish");
	} 

}
