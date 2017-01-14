using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowerIM : InputManager {
	public PlayerBowerBird playerBB;

	protected override void MouseDown (Vector2 mouseWorldPos)
	{
		playerBB.MoveTo (mouseWorldPos);
		playerBB.autoPickup = true;
		//Debug.Log ("mouse: " + mouseWorldPos);
	} 

	protected override void MouseClickedOnObjOfInterest ()
	{
		if (playerBB.isHolding) {
			playerBB.DropItem ();
		}
	}

}
