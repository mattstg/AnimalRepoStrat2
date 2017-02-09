using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class CaribouIM : InputManager {
    public GameObject playerCaribou;
    protected override void MouseDown (Vector2 mouseWorldPos)
	{
        playerCaribou.GetComponent<PlayerCaribou>().MousePressed(mouseWorldPos);
        //Debug.Log ("mouse: " + mouseWorldPos);
    } 

	protected override void MouseClickedOnObjOfInterest ()
	{
        playerCaribou.GetComponent<PlayerCaribou>().Snort();
	}

}
