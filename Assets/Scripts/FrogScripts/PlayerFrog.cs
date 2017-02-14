using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class PlayerFrog : Frog {

    private AudioSource audioSrc;

    public override void CreateFrog(FrogInfo _frogInfo, bool pioneerFrog = false)
    {
        audioSrc = GetComponent<AudioSource>();
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
        Frog frogFound = null;
        foreach(Transform t in GameObject.FindObjectOfType<FrogWS>().frogParent)
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
            Destroy(frogFound.gameObject);            
        }
        else
        {
			foreach(Transform t in GameObject.FindObjectOfType<FrogWS>().frogParent)
			{
				Frog f = t.GetComponent<Frog>();
				if(f.isMale)
				{
					EnterIdleState();
					transform.position = f.transform.position;
					//Destroy(frogFound.gameObject);  
					break;
				}
			}
        }
    }
}
