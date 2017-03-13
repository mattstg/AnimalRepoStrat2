using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class FishGF : GameFlow {
	//public FrogCinematic frogCinematic;
	//public GameObject tutorialScreen;

	public GameObject _fishSpawnPointONE;
	public GameObject _fishSpawnPointTWO;

	public InputManager im;
	public Vector3 lastCheckpt;
	public bool isFirstCheckpoint = true;
	public PlayerFish playerFish;
	public GameObject salmonManager;

    int checkpointsPassed = 0;

	protected override void StartFlow()
	{
        introLessons = 3;
        outroLessons = 2;
        lessonType = LessonType.Fish;
        maxRoundTime = 165;
		stage = -1;
		nextStep = true;
        roundTimeToGetFullScore = 130;
        if (nextStep)
			playerFish.gameObject.SetActive (false);
	}

	protected override void PostGame()
	{
        //ToggleMinimap(false);
        im.enabled = false;
        playerFish.enabled = false;
        roundTimerActive = false;
        string toOut = "";
        if(gameForceEnded)
        {
            toOut = "You have run out of time. You did not make it to your spawning grounds in time for mating.\n\n" +
                "Because your body has undergone a transformation to survive in fresh water, you will not survive back in the salty sea.\n\n" +
                "Your lineage ends here.";
        }
        else
        {
            toOut = "You successfully made it to the spawning grounds in " + scoreText.TimeAsTimerString(roundTime) + "!";
        }
		textPanel.gameObject.SetActive (true);
		textPanel.SetText (toOut);
		textPanel.StartWriting ();
        float scorePerc = Mathf.Max(GetTimedRoundScore(), (checkpointsPassed * .33f));
        scorePerc = Mathf.Clamp(scorePerc, 0, 1);
        scoreText.gameObject.SetActive(false);
        ProgressTracker.Instance.SetRoundScore(scorePerc, 1);
        ProgressTracker.Instance.SubmitProgress(3);
    }


	protected override void StartGame()
	{
        scoreText.gameObject.SetActive(true);
        playerFish.gameObject.SetActive (true);
		roundTimerActive = true;
		trackScoreAsTime = true;
		im.gameObject.SetActive (true);
	}

	public void GameFinished()
	{
        nextStep = true;
	}


	public void ReachedCheckpoint(Vector3 checkPt)
	{
		lastCheckpt = checkPt;	
		isFirstCheckpoint = false;
        checkpointsPassed++;
    }

	public void PlayerDied(PlayerFish playerFish)
	{
		destroyAllFish ();
		playerFish.transform.position = lastCheckpt;
        playerFish.isBeingEaten = false;
        playerFish.SetPlayerEnabled(true);
		//need to spawn a bunch of fish at base of waterfall
		if (!isFirstCheckpoint) {
			for (int c = 0; c < Random.Range ((int)10, 25); c++) {
				GameObject newFish = Instantiate (Resources.Load ("Prefabs/Salmon")) as GameObject;
				newFish.transform.SetParent (salmonManager.transform);
				newFish.transform.position = lastCheckpt + new Vector3 (Random.Range (-3, 3), -11 + Random.Range (-4, 4), 0);
				newFish.AddComponent<AutoWPAssigner> ();
				newFish.GetComponent<Fish> ().enabled = true;
				newFish.GetComponent<FlockingAI> ().enabled = true;
				newFish.GetComponent<Animator> ().enabled = true;
			} // move them then add script to stuff
		} else {
			for (int c = 0; c < Random.Range ((int)10, 25); c++) {
				GameObject newFish = Instantiate (Resources.Load ("Prefabs/Salmon")) as GameObject;
				newFish.transform.SetParent (salmonManager.transform);
				newFish.transform.position = (Random.Range ((int)0, 2) > 0) ? _fishSpawnPointONE.transform.position : _fishSpawnPointTWO.transform.position;
				newFish.AddComponent<AutoWPAssigner> ();
				newFish.GetComponent<Fish> ().enabled = true;
				newFish.GetComponent<FlockingAI> ().enabled = true;
				newFish.GetComponent<Animator> ().enabled = true;
			} // move them then add script to stuff
		}
		allowNewSpawns ();
		//Message 
	}

	public void allowNewSpawns(){
		foreach (FishLoader f in GetComponentsInChildren<FishLoader>()) {
			f.reAllowSpawn();
		}
	}

	public void destroyAllFish(){
		foreach (Fish f in salmonManager.GetComponentsInChildren<Fish>()) {
			Destroy (f.gameObject);
		}
	}
}
