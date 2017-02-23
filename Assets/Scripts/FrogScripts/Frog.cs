using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;


//If you are reading this and from LoL. I applogize for the most hardcoded software I have ever created

public class Frog : MonoBehaviour {

    public enum FrogState { jumping,landedJump, idle, calling }
    public FrogInfo frogInfo;

    public bool isMale { get { return frogInfo.isMale; } }
    public FrogState currentFrogState;

    public bool inPuddle = false;
    //Frog jump stuff
    Vector2 goalPos;
    float frogSpeed = .7f;
    public bool outtaBounds = false;
	public bool playerCntrl = false;
	public bool isPlayerDescendant { get { return frogInfo.playerDescendant; }  }
    public bool leaveMap = false;

    //Frog idle state
    float currentIdleWaitTime;
    Vector2 idleWaitRange = new Vector2(1, 6);

    //calling state
	public GameObject ribbitRing;
    OpacityFade opactiyFade;
    float maxRange = 4.4f;
	float rangeRateIncrease = 3f;
    float chanceToRepeatCall = .4f;

    //mating
    Vector2 rangeOfKids = new Vector2(4, 8);
    float mateCooldown = 12;
    float matMaxCooldown = 12;
    bool firstCall = true;

    public float publicGenNum;
    public bool publicIsPlayer;

	public virtual void CreateFrog(FrogInfo _frogInfo, bool pioneerFrog = false)
    {
        frogInfo = new FrogInfo(_frogInfo);
		if(!playerCntrl)
			EnterIdleState();
		if (!isMale) {
			transform.localScale = transform.localScale * 1.5f;
            SpriteRenderer renderer;
            renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            renderer.color = new Color(0f, .63f, 0f, 1f); 
            frogSpeed *= .8f;
            
		}
        FrogGV.AddFrogToMasterList(this,isMale);
        if (pioneerFrog)
            mateCooldown = 0;
		if (frogInfo.playerDescendant) {
			FrogGV.frogWS.frogGF.score++;
		}
        opactiyFade = ribbitRing.GetComponent<OpacityFade>();
        opactiyFade.SetPresentOpacity(0);
    }
	// Update is called once per frame
	void Update () {

        mateCooldown -= Time.deltaTime;

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
        if(leaveMap)
        {
            float angToExit = MathHelper.AngleBetweenPoints(new Vector2(), transform.position);
            JumpTowardsAngle(angToExit);
            //Debug.Log(string.Format("at pos: {0}, goalPos {1} from offset {2}, angle is {3}", transform.position, goalPos, goalOffset, angToCenter));
            return;
        }

        if ((transform.position.x >= FrogGV.mapBounds.x || transform.position.x <= -1f * FrogGV.mapBounds.x) || (transform.position.y >= FrogGV.mapBounds.y || transform.position.y <= -1f * FrogGV.mapBounds.y))
            outtaBounds = true;
        else
            outtaBounds = false;

        if (outtaBounds)
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

        if(!isMale && mateCooldown <= 0) //female rdy to mate has landed
        {
            bool inWater = InPuddle();
            if (inWater)
            {
                Frog father = FrogGV.FemaleCanMate(transform.position);
                if (father)
                {
                    MakeBaby(father);
                    EnterIdleState();
                    currentIdleWaitTime = 5; //force wait after having a kid
                }
            }
        }
        else
        {//Landed on a frog, hop away to new location
           // if(lastTouchedFrog.playerCntrl)
              //  Debug.Log(string.Format("inpuddle {0}, ismale {1}, isTouchingMaleFrog{2}, mateCooldown{3}", inPuddle, isMale, lastTouchedFrog.isMale, mateCooldown));
            EnterRandomJump();
        }

        if(isMale && Random.Range(0,1f) >= .5f)
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

    protected void EnterIdleState()
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
        PlayRibbitNoise();
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

    protected virtual void PlayRibbitNoise()
    { //just so player can overite it

    }

	private void CallingState()
	{
		if (ribbitRing.transform.localScale.x >= maxRange)
		{
			ribbitRing.transform.localScale = new Vector3 (1, 1, 1);
            ribbitRing.SetActive (false);
            opactiyFade.SetPresentOpacity(1);
            firstCall = true;
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
            if(firstCall)
            {
                firstCall = false;
                FrogGV.RibbitAtLoc(transform.position);
            }
            ribbitRing.SetActive (true);
			ribbitRing.transform.localScale = ribbitRing.transform.localScale + new Vector3 (1, 1) * rangeRateIncrease * Time.deltaTime;
            opactiyFade.SetPresentOpacity(1 - opactiyFade.GetIntegral(Mathf.Max(0, 3 * ribbitRing.transform.localScale.x / maxRange - 2)));
        }
    }

    private void DisableRibbiting()
    {
        ribbitRing.transform.localScale = new Vector3(1, 1, 1);
        ribbitRing.SetActive(false);
        firstCall = true;
    }

    private void JumpTowardsGoal()
    {
		float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, goalPos);
		transform.eulerAngles = new Vector3 (0, 0,  angToGoal - 90);
        transform.position = Vector2.MoveTowards(transform.position, goalPos, frogSpeed * Time.deltaTime);
        if(new Vector2(transform.position.x, transform.position.y) == goalPos)
            currentFrogState = FrogState.landedJump;
    }

    private bool InPuddle()
    {
       RaycastHit2D rch = Physics2D.Raycast(transform.position, new Vector2(1f, 0), .1f, FrogGV.pond_layer_mask);
        if (rch)
            return true;
        return false;
    }

    /*public void OnCollisionEnter2D(Collision2D coli)
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
        if(otherObj.GetComponent<Puddle>())
        {
            inPuddle = entering;
        }
        else if(otherObj.name == "Boundry")
        {
            outtaBounds = !entering;
        }
    }*/

    public void HeardARibbit(Vector2 loc)
    {
		if (mateCooldown <= 0 && !isMale)
            JumpTowardsGoal(loc);
    }


    private void SetNewRandomGoalPos()
    {
        float randAngle = Random.Range(0, 360);
        Vector2 goalOffset = MathHelper.DegreeToVector2(randAngle);
        goalPos = MathHelper.V3toV2(transform.position) + goalOffset * frogSpeed;
    }

	private void MakeBaby(Frog father)
    {
        mateCooldown = matMaxCooldown;
        if (FrogGV.frogWS.frogParent.childCount < FrogGF.maxFrogCount)
        {
            int numOfKids = (int)Random.Range(rangeOfKids.x, rangeOfKids.y);
            for (int i = 0; i < numOfKids; i++)
            {
                GameObject newFrog = Instantiate(Resources.Load("Prefabs/Tadpole")) as GameObject;
                newFrog.transform.SetParent(FrogGV.frogWS.tadpoleParent);
                newFrog.transform.position = transform.position;
                Frog.FrogInfo tempFi = new FrogInfo(frogInfo);
                tempFi.genNumber = Mathf.Max(tempFi.genNumber, father.frogInfo.genNumber) + 1;
                tempFi.isMale = Random.Range(0f, 1f) > .45f;// MathHelper.Fiftyfifty();
                tempFi.playerDescendant = tempFi.playerDescendant || father.frogInfo.playerDescendant;
                newFrog.GetComponent<Tadpole>().BirthTadpole(tempFi);//playerDescendant = _playerDescendant || isPlayerDescendant;
                newFrog.GetComponent<Tadpole>().enabled = true;
            }
        }
    }

    public virtual void FrogEaten()
    {
		if (frogInfo.playerDescendant)
			FindObjectOfType<FrogGF> ().score--;
        FrogGV.RemoveFrogFromMasterList(this,isMale);
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
