using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrog : Frog {

	public void MousePressed(Vector3 loc)
	{
		if (!outtaBounds && ( currentFrogState == FrogState.idle || currentFrogState == FrogState.calling)) {
			JumpTowardsGoal (loc);
		}
	}

	public void ClickedOn()
	{
		if (!outtaBounds && currentFrogState == FrogState.idle)
			EnterCallingState ();
	}
}
