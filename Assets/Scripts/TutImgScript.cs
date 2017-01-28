using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutImgScript : MonoBehaviour {
	

	public void TutFinished()
    {
		GameObject.FindObjectOfType<GameFlow> ().TutorialClosed ();
    }
}
