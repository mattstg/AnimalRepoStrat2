using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckIM : InputManager {
    public PlayerDuck playerDuck;
	protected override void MouseDown (Vector2 mouseWorldPos)
	{
        //Debug.Log ("mouse: " + mouseWorldPos);
        playerDuck.MousePressed(mouseWorldPos);
	} 

	protected override void MouseClickedOnObjOfInterest ()
	{
	
	}

}
