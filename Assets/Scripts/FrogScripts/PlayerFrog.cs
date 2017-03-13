using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class PlayerFrog : Frog {

    public override void CreateFrog(FrogInfo _frogInfo, bool pioneerFrog = false)
    {
        base.CreateFrog(_frogInfo, pioneerFrog);
    }

    public void MousePressed(Vector3 loc)
	{
		if (!outtaBounds && ( currentFrogState == FrogState.idle || currentFrogState == FrogState.calling)) {
			JumpTowardsGoal (loc);
		}
	}

    protected override void PlayRibbitNoise()
    {
        LOLAudio.Instance.PlayAudio("FrogCall.wav", false);
    }


    public void ClickedOn()
	{
		if (!outtaBounds && currentFrogState == FrogState.idle)
			EnterCallingState ();
	}

    public override void FrogEaten()
    {
        toScale.transform.localScale = new Vector3(originalScale.x, originalScale.y, transform.localScale.z);
        Frog frogFound = null;
        foreach(Transform t in FrogGV.frogWS.frogParent)
        {
            Frog f = t.GetComponent<Frog>();
            if(f.isPlayerDescendant && f.isMale)
            {
                frogFound = f;
                break;
            }
        }

        if(frogFound)
        {
            EnterIdleState();
            transform.position = frogFound.transform.position;
            frogFound.FrogEaten();
        }
        else
        {
            bool safetyCatch = true;
			foreach(Transform t in FrogGV.frogWS.frogParent)
			{
				Frog f = t.GetComponent<Frog>();
				if(f.isMale && !f.outtaBounds)
				{
					EnterIdleState();
					transform.position = f.transform.position;
                    //Destroy(frogFound.gameObject);  
                    safetyCatch = false;
					break;
				}
			}
            if(safetyCatch)
            {
                EnterIdleState();
                transform.position = new Vector2(0, 0);
            }

        }
    }
}
