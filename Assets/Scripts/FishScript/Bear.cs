using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour {

	enum BearState{Swipe,Rest}

	BearState curState = BearState.Rest;
	List<Transform> bearSwipes;
	List<FlockingAI> fishInRange = new List<FlockingAI> ();
	public Transform swipeParent;
	float maxTimeToAct = 2.5f;
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
			Debug.Log ("alpha: " + curTimeToAct / maxTimeToAct);
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
		if (fishInRange.Count > 0) {
			eatingFish = fishInRange [0].transform;
			//fishInRange[0].dies
		}
	}

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
