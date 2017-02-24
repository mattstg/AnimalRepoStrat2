using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Bear : MonoBehaviour {

	enum BearState{Hunting,Eating,Retrieving,Returning}

	BearState curState = BearState.Eating;
	List<SpriteRenderer> bearSwipes;
	List<Fish> fishInRange = new List<Fish> ();
	public Transform swipeParent;
	float maxTimeToAct = 1.25f;
	float curTimeToAct = 0;
	int curSwipeIndex = 0;
	[HideInInspector]
	public Transform eatingFish;
	public Transform bearMouth;
	public Vector2 originalPos;
	public Vector3 originalRot;
	public float rotDampTime = 0.5f;
	private Vector3 rotVelocity = Vector2.zero;
	float bearSpeed = 1;

	// Use this for initialization
	void Awake() {
        originalPos = transform.position;
        originalRot = transform.eulerAngles;
		bearSwipes = new List<SpriteRenderer> ();
		foreach (Transform t in swipeParent) {
			bearSwipes.Add (t.GetComponent< SpriteRenderer>());
			t.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		curTimeToAct += Time.deltaTime;

        if (curState == BearState.Hunting)
        {
            SetSwipeAlpha(bearSwipes[curSwipeIndex], curTimeToAct / maxTimeToAct);
            if (curTimeToAct >= maxTimeToAct)
                Swipes();
        }
        else if (curState == BearState.Retrieving)
        {
            if (eatingFish)
            {
                if (IsAtGoal(eatingFish.transform.position,true))
                {
                    curState = BearState.Returning;
                    eatingFish.transform.position = bearMouth.position;
                    curTimeToAct = 0;
                }
                else
                {
                    MoveTowardsGoal(eatingFish.transform.position);
                }
            }
            else
            {
                curState = BearState.Returning;
            }
        }
        else if (curState == BearState.Returning)
        {
            if (IsAtGoal(originalPos))
            {
                transform.position = originalPos;
                //transform.eulerAngles = originalRot;
		        float targetRot = transform.eulerAngles.z + GetMinimalRotation(transform.eulerAngles.z, originalRot.z);
                transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, new Vector3(0, 0, targetRot), ref rotVelocity, rotDampTime);
				if (eatingFish != null)
                {
					eatingFish.position = bearMouth.position;
					eatingFish.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);
				}
                if (transform.eulerAngles.z <= targetRot + 2 && transform.eulerAngles.z >= targetRot -2)
                {
                    curState = BearState.Eating;
                    curTimeToAct = 0;
                }
            }
            else
            {
                MoveTowardsGoal(originalPos);
				if (eatingFish && eatingFish != null)
                {
                    eatingFish.position = bearMouth.position;
                    eatingFish.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
                }                
            }
        }
        else if(curState == BearState.Eating)
        {
            if (curTimeToAct >= maxTimeToAct/4)
            {
                if (eatingFish)
                {
                    if(eatingFish.gameObject.GetComponent<PlayerFish>())
                    {
                        GameObject.FindObjectOfType<FishGF>().PlayerDied(eatingFish.gameObject.GetComponent<PlayerFish>());
                        eatingFish = null;
                    }
                    else
                    {
                        Destroy(eatingFish.gameObject);
                        eatingFish = null;
                    }                    
                }
                BeginSwipe();
            }
        }
			
		
	}

	private void TurnOffAllTrigger()
	{
		foreach (SpriteRenderer sr in bearSwipes)
			sr.gameObject.SetActive (false);
	}

	private void BeginSwipe()
	{
		curTimeToAct = 0;
		curState = BearState.Hunting;
		curSwipeIndex++;
		curSwipeIndex %= bearSwipes.Count;
		bearSwipes [curSwipeIndex].gameObject.SetActive (true);
		SetSwipeAlpha (bearSwipes [curSwipeIndex], 0);
	}

	private void Swipes()
	{
        curTimeToAct = 0;
		TurnOffAllTrigger ();
		if (fishInRange.Count > 0 && fishInRange[0] != null) 
		{
            curState = BearState.Retrieving;
			eatingFish = fishInRange [0].transform;
			fishInRange [0].isBeingEaten = true;
			
			if (fishInRange [0].gameObject.GetComponent<PlayerFish> ())
            {
                fishInRange[0].gameObject.GetComponent<PlayerFish>().SetPlayerEnabled(false);
                //player dies, restart at checkpoint
            }
			else
			{
				fishInRange [0].Dies ();
			}
        }
        else
        {
            curState = BearState.Eating;
        }
        fishInRange.Clear();
    }


	private void MoveTowardsGoal(Vector2 goal)
	{
		float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, goal);
		float distanceToGoal = Vector2.Distance (goal, transform.position);
		//transform.eulerAngles = new Vector3 (0, 0, angToGoal + 180);
		float targetRot = transform.eulerAngles.z + GetMinimalRotation(transform.eulerAngles.z, angToGoal + 180);
                transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, new Vector3(0, 0, targetRot), ref rotVelocity, rotDampTime);
		Vector2 goalDir = goal - MathHelper.V3toV2(transform.position);
		transform.position = Vector2.MoveTowards(transform.position, goal, bearSpeed * Time.deltaTime);
    }

    private bool IsAtGoal(Vector2 goal, bool mouthAcceptable = false)
    {
        if (mouthAcceptable && Vector2.Distance(bearMouth.position, goal) < .2f)
            return true;
        return (Vector2.Distance(transform.position, goal) < .2f);
    }

	private void SetSwipeAlpha(SpriteRenderer sr, float setAlpha)
	{
		Color curColor = sr.color;
		curColor.a = setAlpha;
		sr.color = curColor;
	}

	public void OnTriggerEnter2D(Collider2D coli)
	{
		Fish fish = coli.GetComponent<Fish> ();
		if (fish && !fishInRange.Contains(fish) && !fish.isBeingEaten) {
			fishInRange.Add (fish);
		}
	}

	public void OnTriggerExit2D(Collider2D coli)
	{
        Fish fish = coli.GetComponent<Fish> ();
		if (fish) {
			fishInRange.Remove (fish);
		}
	}

	public float GetMinimalRotation(float source, float target)
	{
		float delta = target - source;
		while (delta > 180)
		{
			delta -= 360;
		}
		while (delta < -180)
		{
			delta += 360;
		}
		return delta;
	}
}
