﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class BowerIM : InputManager {
	public PlayerBowerBird playerBB;

	protected override void MouseDown (Vector2 mouseWorldPos)
	{
		playerBB.MoveTo (mouseWorldPos);
		//playerBB.autoPickup = true;
		//Debug.Log ("mouse: " + mouseWorldPos);
	} 

	protected override void MouseClickedOnObjOfInterest ()
	{
		if (playerBB.altitude > 0) {
			playerBB.isMoving = false;
			playerBB.body.velocity = Vector2.zero;
		} else {
			if (playerBB.isHolding) {
				playerBB.DropItem ();
			}
		}
	}

}
