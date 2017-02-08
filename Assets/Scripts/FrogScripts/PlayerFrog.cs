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

    protected override void PlayRibbitNoise()
    {
        if(MainMenu.Sound_Active)
        {
            GetComponent<AudioSource>().Play();
        }
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
