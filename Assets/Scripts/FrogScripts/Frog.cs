﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//If you are reading this and from LoL. I applogize for the hardcodedness of it all

public class Frog : MonoBehaviour {

    public enum FrogState { jumping,landedJump, idle, calling }
    FrogInfo frogInfo;

    public bool isMale { get { return frogInfo.isMale; } }
    public FrogState currentFrogState;

    bool inPuddle = false;
    Frog lastTouchedFrog;
    //Frog jump stuff
    Vector2 goalPos;
    float frogSpeed = 1;
    public bool outtaBounds = false;
	public bool playerCntrl = false;
	bool isPlayerDescendant = false;

    //Frog idle state
    float currentIdleWaitTime;
    Vector2 idleWaitRange = new Vector2(1, 6);

    //calling state
	public GameObject ribbitRing;
    float maxRange = 8;
	float rangeRateIncrease = 4f;
    float chanceToRepeatCall = .4f;

    //mating
    Vector2 rangeOfKids = new Vector2(5, 20);
    float mateCooldown = 12;
    float matMaxCooldown = 12;
	Transform lastHeardRibbitRing;
	float lastHeardRibbitCooldown = 3;
	float lastHeardRibbitCooldownMax = 3;

	public void CreateFrog(FrogInfo _frogInfo)
    {
        frogInfo = new FrogInfo(_frogInfo);
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
		lastHeardRibbitCooldown -= Time.deltaTime;
		if (lastHeardRibbitCooldown <= 0)
			lastHeardRibbitRing = null;

		switch(currentFrogState)
        {
            case FrogState.jumping:
                DisableRibbiting();
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

        if(lastTouchedFrog != null)
        {
            if(inPuddle && !isMale && lastTouchedFrog.isMale && mateCooldown <= 0)
            {//as a female, landed on a frog in a puddle, baby time
                MakeBaby(false);
                EnterIdleState();
                currentIdleWaitTime = 5; //force wait after having a kid
            }
            else
            {//Landed on a frog, hop away to new location
                if(lastTouchedFrog.playerCntrl)
                    Debug.Log(string.Format("inpuddle {0}, ismale {1}, isTouchingMaleFrog{2}, mateCooldown{3}", inPuddle, isMale, lastTouchedFrog.isMale, mateCooldown));
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
       // currentCallingTime = 0;
        currentFrogState = FrogState.calling;
    }

	/*
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
    }*/

	private void CallingState()
	{
		if (ribbitRing.transform.localScale.x >= maxRange)
		{
			ribbitRing.transform.localScale = new Vector3 (1, 1, 1);
			ribbitRing.SetActive (false);
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
		else
		{
			ribbitRing.SetActive (true);
			ribbitRing.transform.localScale = ribbitRing.transform.localScale + new Vector3 (1, 1) * rangeRateIncrease * Time.deltaTime;
		}
	}

    private void DisableRibbiting()
    {
        ribbitRing.transform.localScale = new Vector3(1, 1, 1);
        ribbitRing.SetActive(false);
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
                lastTouchedFrog = otherFrog;
            }
            else if(lastTouchedFrog == otherFrog)
            {
                lastTouchedFrog = null;
            }
        }
        else if(otherObj.GetComponent<Puddle>())
        {
            inPuddle = entering;
        }
        else if(otherObj.CompareTag("RibbitRing"))
        {
			if (entering) {
				//Debug.Log ("enetering ");
				HeardARibbit (otherObj.transform);
			}
			else
            {
                if (otherObj.transform == lastHeardRibbitRing)
                {
                    lastHeardRibbitRing = null;
                }
				//Debug.Log ("eaving");
			}
        }
        else if(otherObj.name == "Boundry")
        {
            outtaBounds = !entering;
        }
    }

    private void HeardARibbit(Transform ribbitLoc)
    {
		if (ribbitLoc != transform && mateCooldown <= 0 && !isMale && lastHeardRibbitRing != ribbitLoc)
        {
			lastHeardRibbitRing = ribbitLoc;
			lastHeardRibbitCooldown = lastHeardRibbitCooldownMax;
//            float angToCenter = MathHelper.AngleBetweenPoints(transform.position, ribbitLoc.position);
            JumpTowardsGoal(ribbitLoc.position);
        }
    }


    private void SetNewRandomGoalPos()
    {
        float randAngle = Random.Range(0, 360);
        Vector2 goalOffset = MathHelper.DegreeToVector2(randAngle);
        goalPos = MathHelper.V3toV2(transform.position) + goalOffset * frogSpeed;
    }

	private void MakeBaby(bool _playerDescendant)
    {
        mateCooldown = matMaxCooldown;
        int numOfKids = (int)Random.Range(rangeOfKids.x, rangeOfKids.y);
        for (int i = 0; i < numOfKids; i++)
        {
            GameObject newFrog = Instantiate(Resources.Load("Prefabs/Tadpole")) as GameObject;
            newFrog.transform.SetParent(GameObject.FindObjectOfType<FrogWS>().tadpoleParent);
            newFrog.transform.position = transform.position;
            Frog.FrogInfo tempFi = new FrogInfo(frogInfo);
            tempFi.genNumber++;
            tempFi.isMale = MathHelper.Fiftyfifty();
            newFrog.GetComponent<Tadpole>().BirthTadpole(tempFi);//playerDescendant = _playerDescendant || isPlayerDescendant;
        }
    }

    public void FrogEaten()
    {
        Destroy(this.gameObject);
    }

    public class FrogInfo
    {
        public int genNumber = 0;
        public bool playerDescendant = false;
        public bool isMale;

        public FrogInfo(int _genNumber, bool _playerDescendant, bool _isMale)
        {
            genNumber = _genNumber;
            playerDescendant = _playerDescendant;
            isMale = _isMale;
        }

        public FrogInfo(FrogInfo toClone)
        {
            genNumber = toClone.genNumber;
            playerDescendant = toClone.playerDescendant;
            isMale = toClone.isMale;
        }

    }
}