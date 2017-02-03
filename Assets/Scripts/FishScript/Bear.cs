using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour {

	enum BearState{Swipe,Rest,WalkingToFood,WalkingBackFromFood,DigestingFood}

	BearState curState = BearState.Rest;
	List<Transform> bearSwipes;
	List<FlockingAI> fishInRange = new List<FlockingAI> ();
	public Transform swipeParent;
	float maxTimeToAct = 1.25f;
	float curTimeToAct = 0;
	int curSwipeIndex = 0;
	[HideInInspector]
	public Transform eatingFish;
	public Transform bearMouth;
	// Use this for initialization
	void Awake() {
		bearSwipes = new List<Transform> ();
		foreach (Transform t in swipeParent) {
			bearSwipes.Add (t);
			t.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		curTimeToAct += Time.deltaTime;
		if (curState == BearState.Swipe) {
			SetSwipeAlpha (bearSwipes [curSwipeIndex].GetComponent<SpriteRenderer> (), curTimeToAct / maxTimeToAct);
			if (curTimeToAct >= maxTimeToAct)
				Swipes ();
		} 
		else if (curState == BearState.Rest)
		{
			if (eatingFish)
				eatingFish.transform.position = bearMouth.position;
			if (curTimeToAct >= maxTimeToAct) 
			{
				if (eatingFish)
				{
					Destroy (eatingFish.gameObject);
					eatingFish = null;
				}
				BeginSwipe ();
			}
		}
	}

	private void TurnOffAllTrigger()
	{
		foreach (Transform t in bearSwipes)
			t.gameObject.SetActive (false);
	}

	private void BeginSwipe()
	{
		curTimeToAct = 0;
		curState = BearState.Swipe;
		curSwipeIndex++;
		curSwipeIndex %= bearSwipes.Count;
		bearSwipes [curSwipeIndex].gameObject.SetActive (true);
		SetSwipeAlpha (bearSwipes [curSwipeIndex].GetComponent<SpriteRenderer> (), 0);
	}

	private void Swipes()
	{
		curState = BearState.Rest;
		curTimeToAct = 0;
		TurnOffAllTrigger ();
		if (fishInRange.Count > 0) 
		{
			eatingFish = fishInRange [0].transform;

			if (fishInRange [0].gameObject.GetComponent<PlayerFish> ()) {
				//player dies, restart at checkpoint

			}
			else
			{
				fishInRange [0].Dies ();
				eatingFish.GetComponent<Fish> ().enabled = false;
				//fishInRange[0].dies
			}

		}
	}


	private void ConsumeFish()
	{
/*		if (fishInRange [0].gameObject.GetComponent<PlayerFish> ()) 
		{
			GameObject.FindObjectOfType<FishGF>().PlayerDied();
		}*/
	}

	/*private void MoveTowardsGoal()
	{
		if (eatingFish)
			goalPos = eatingFish.position;
		float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, goalPos);
		float distanceToGoal = Vector2.Distance (goalPos, transform.position);
		transform.eulerAngles = new Vector3 (0, 0, angToGoal - 90);
		Vector2 goalDir = goalPos - MathHelper.V3toV2(transform.position);
		float speed = foxCurrentSpeed;
		if (distanceToGoal < foxCurrentSpeed * Time.deltaTime)
			speed = distanceToGoal;
		//GetComponent<Rigidbody2D> ().AddRelativeForce (goalDir.normalized * foxCurrentSpeed * Time.deltaTime,ForceMode2D.Impulse);
		GetComponent<Rigidbody2D> ().velocity = goalDir.normalized * speed;
		//transform.position = Vector2.MoveTowards(transform.position, goalPos, foxCurrentSpeed * Time.deltaTime);
	}*/


	private void SetSwipeAlpha(SpriteRenderer sr, float setAlpha)
	{
		Color curColor = sr.color;
		curColor.a = setAlpha;
		sr.color = curColor;
	}

	public void OnTriggerEnter2D(Collider2D coli)
	{
		FlockingAI fish = coli.GetComponent<FlockingAI> ();
		if (fish && !fishInRange.Contains(fish)) {
			fishInRange.Add (fish);
		}
	}

	public void OnTriggerExit2D(Collider2D coli)
	{
		FlockingAI fish = coli.GetComponent<FlockingAI> ();
		if (fish) {
			fishInRange.Remove (fish);
		}
	}
}
