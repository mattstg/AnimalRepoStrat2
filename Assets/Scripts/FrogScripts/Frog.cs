using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//If you are reading this and from LoL. I applogize for the hardcodedness of it all

public class Frog : MonoBehaviour {

    public enum FrogState { jumping,landedJump, idle, calling }

    public bool isMale;
    public FrogState currentFrogState;

    bool inPuddle = false;
    bool isTouchingFrog { get { return touchingFrogs.Count > 0; } } //not perfect, does not cover case such as 3 frog pileup
    List<Frog> touchingFrogs = new List<Frog>();
    //Frog jump stuff
    Vector2 goalPos;
    float frogSpeed = 1;
    public bool outtaBounds = false;
	public bool playerCntrl = false;

    //Frog idle state
    float currentIdleWaitTime;
    Vector2 idleWaitRange = new Vector2(1, 6);

    //calling state
    int maxRange = 3;
    int currentRange = 0;
    float currentCallingTime = 0;
    float timeBetweenRangeIncrease = 1;
    float chanceToRepeatCall = .4f;

    //mating
    Vector2 rangeOfKids = new Vector2(5, 20);
    float mateCooldown = 12;
    float matMaxCooldown = 12;

    public void CreateFrog(bool _isMale)
    {
        isMale = _isMale;
		if(!playerCntrl)
			EnterIdleState();
		if (!isMale) {
			transform.localScale = transform.localScale * 1.5f;
			frogSpeed /= 2;
		}
    }
	// Update is called once per frame
	void Update () {
        mateCooldown -= Time.deltaTime;
		switch(currentFrogState)
        {
            case FrogState.jumping:
                DestroyAllRibitKids();
                JumpTowardsGoal();
                break;
            case FrogState.landedJump:			
                LandedJump();
                break;
            case FrogState.idle:
                IdleState();
                break;
            case FrogState.calling:
                CallingState();
                break;
        }
	}

    private void JumpTowardsAngle(float ang)
    {
        Vector2 goalOffset = MathHelper.DegreeToVector2(ang);
        goalPos = MathHelper.V3toV2(transform.position) + goalOffset * frogSpeed;
        currentFrogState = FrogState.jumping;
    }

    protected void JumpTowardsGoal(Vector2 goalLoc)
    {
        float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, goalLoc);
		transform.eulerAngles = new Vector3 (0, 0, angToGoal - 90);
        Vector2 goalOffset = MathHelper.DegreeToVector2(angToGoal);
        Vector2 diff = goalLoc - MathHelper.V3toV2(this.transform.position);
        float magnitude = diff.magnitude;
        magnitude = (magnitude > frogSpeed) ? frogSpeed:magnitude;

        if (diff.magnitude < frogSpeed)
        {
            goalPos = MathHelper.V3toV2(transform.position) + goalOffset * magnitude;
        }
        else
        {
            goalPos = MathHelper.V3toV2(transform.position) + goalOffset * magnitude;
        }
        currentFrogState = FrogState.jumping;
    }

    private void LandedJump()
    {
        if(outtaBounds)
        {
            float angToCenter = MathHelper.AngleBetweenPoints(transform.position, new Vector2());
            JumpTowardsAngle(angToCenter);
            //Debug.Log(string.Format("at pos: {0}, goalPos {1} from offset {2}, angle is {3}", transform.position, goalPos, goalOffset, angToCenter));
            return;
        }

		if (playerCntrl) {
			EnterIdleState ();
			return;
		}

        if(isTouchingFrog)
        {
            if(inPuddle && !isMale && isTouchingMaleFrog() && mateCooldown <= 0)
            {//as a female, landed on a frog in a puddle, baby time
                MakeBaby();
                EnterIdleState();
                currentIdleWaitTime = 5; //force wait after having a kid
            }
            else
            {//Landed on a frog, hop away to new location
                EnterRandomJump();
            }
        }
        else if(inPuddle && isMale)
        {
            //landed in puddle, try ribbiting to attract mate
            EnterCallingState();
        }
        else
        {
            EnterIdleState();
        }
    }

    private void IdleState()
    {
		if (playerCntrl)
			return;
        currentIdleWaitTime -= Time.deltaTime;
        if(currentIdleWaitTime <= 0)
            EnterRandomJump();
    }

    private void EnterIdleState()
    {
        currentFrogState = FrogState.idle;
        currentIdleWaitTime = Random.Range(idleWaitRange.x, idleWaitRange.y);
    }

    private void EnterRandomJump()
    {
        SetNewRandomGoalPos();
        currentFrogState = FrogState.jumping;
    }

    protected void EnterCallingState()
    {
        currentCallingTime = 0;
        currentRange = 0;
        currentFrogState = FrogState.calling;
    }

    private void CallingState()
    {
        currentCallingTime += Time.deltaTime;
        int frogCallRange = (int)(currentCallingTime / timeBetweenRangeIncrease);
        if (frogCallRange > maxRange)
        {
            DestroyAllRibitKids();
			if (!playerCntrl) {
				if (Random.Range (0f, 1f) > chanceToRepeatCall) {
					EnterCallingState (); //re-cycle the state
				} else {
					EnterRandomJump ();
				}
			} else {
				EnterIdleState ();
			}
        }
        else if(currentRange != frogCallRange)
        {
            currentRange = frogCallRange;
            GameObject ribbitObj = Instantiate(Resources.Load("Prefabs/RibbitRing")) as GameObject;
            ribbitObj.transform.SetParent(transform);
            ribbitObj.transform.localPosition = new Vector3();
            ribbitObj.transform.localScale = new Vector3(1, 1, 1) * (currentRange + 1) * 1.75f;
            ribbitObj.name = "ribbit";
        }
    }

    private void DestroyAllRibitKids()
    {
        foreach(Transform t in transform)
            if (t.name == "ribbit")
                Destroy(t.gameObject);
    }

    private void JumpTowardsGoal()
    {
		float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, goalPos);
		transform.eulerAngles = new Vector3 (0, 0,  angToGoal - 90);
        transform.position = Vector2.MoveTowards(transform.position, goalPos, frogSpeed * Time.deltaTime);
        if(new Vector2(transform.position.x, transform.position.y) == goalPos)
            currentFrogState = FrogState.landedJump;
    }

    public void OnCollisionEnter2D(Collision2D coli)
    {
        ResolveCollision(coli.collider,true);
    }
    public void OnTriggerEnter2D(Collider2D coli)
    {
        ResolveCollision(coli,true);
    }
    public void OnCollisionExit2D(Collision2D coli)
    {
        ResolveCollision(coli.collider, false);
    }
    public void OnTriggerExit2D(Collider2D coli)
    {
        ResolveCollision(coli, false);
    }



    public void ResolveCollision(Collider2D coli, bool entering)
    {
        GameObject otherObj = coli.gameObject;
        if(otherObj.GetComponent<Frog>())
        {
            Frog otherFrog = otherObj.GetComponent<Frog>();
            if (entering)
            {
                if (!touchingFrogs.Contains(otherFrog))
                    touchingFrogs.Add(otherFrog);
            }
            else
            {
                touchingFrogs.Remove(otherFrog);
            }
        }
        else if(otherObj.GetComponent<Puddle>())
        {
            inPuddle = entering;
        }
        else if(otherObj.CompareTag("RibbitRing"))
        {
            HeardARibbit(otherObj.transform);
        }
        else if(otherObj.name == "Boundry")
        {
            outtaBounds = !entering;
        }
    }

    private void HeardARibbit(Transform ribbitLoc)
    {
        if (ribbitLoc.parent != this && mateCooldown <= 0 && !isMale)
        {
//            float angToCenter = MathHelper.AngleBetweenPoints(transform.position, ribbitLoc.position);
            JumpTowardsGoal(ribbitLoc.position);
        }
    }
    
    private bool isTouchingMaleFrog()
    {
        if (!isTouchingFrog)
            return false;
        foreach (Frog f in touchingFrogs)
            if (f.isMale)
                return true;
        return false;
    }

    private void SetNewRandomGoalPos()
    {
        float randAngle = Random.Range(0, 360);
        Vector2 goalOffset = MathHelper.DegreeToVector2(randAngle);
        goalPos = MathHelper.V3toV2(transform.position) + goalOffset * frogSpeed;
    }

    private void MakeBaby()
    {
        mateCooldown = matMaxCooldown;
        int numOfKids = (int)Random.Range(rangeOfKids.x, rangeOfKids.y);
        for (int i = 0; i < numOfKids; i++)
        {
            GameObject newFrog = Instantiate(Resources.Load("Prefabs/Tadpole")) as GameObject;
            newFrog.transform.SetParent(GameObject.FindObjectOfType<FrogWS>().tadpoleParent);
            newFrog.transform.position = transform.position;
        }
    }

    public void FrogEaten()
    {
        Destroy(this.gameObject);
    }
}
