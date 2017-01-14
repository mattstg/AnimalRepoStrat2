using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogIM : InputManager {
	public PlayerFrog playerFrog;


	protected override void MouseDown (Vector2 mouseWorldPos)
	{
		playerFrog.MousePressed (mouseWorldPos);
		//Debug.Log ("mouse: " + mouseWorldPos);
	} 

	protected override void MouseClickedOnObjOfInterest ()
	{
		playerFrog.ClickedOn ();
	}
}
