using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class DuckIM : InputManager {
    public GameObject playerDuck;
	protected override void MouseDown (Vector2 mouseWorldPos)
	{
        //Debug.Log ("mouse: " + mouseWorldPos);
        playerDuck.GetComponent<PlayerDuck>().MousePressed(mouseWorldPos);
	} 

	protected override void MouseClickedOnObjOfInterest ()
	{
        playerDuck.GetComponent<PlayerDuck>().Quack();
	}

}
