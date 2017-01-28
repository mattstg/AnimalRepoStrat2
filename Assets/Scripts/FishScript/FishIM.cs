using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIM : InputManager {
	public PlayerFish pF;

	protected override void MouseDown (Vector2 mouseWorldPos)
	{
	//	Debug.Log ("mouse down");
		pF.isMoving = true;
		pF.MoveTo (mouseWorldPos);
	} 

	protected override void MouseClickedOnObjOfInterest ()
	{
        Debug.Log("mouse clicked on fish");
	} 

}
