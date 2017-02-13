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
    public GameObject miniMap;
	public GameObject salmonManager;

    float timeScore100 = 150;

	protected override void StartFlow()
	{
        introLessons = 2;
        outroLessons = 3;
        lessonType = LessonType.Fish;
        maxRoundTime = 210;
		stage = -1;
		nextStep = true;
        roundTimeToGetFullScore = 150;
        if (nextStep)
			playerFish.gameObject.SetActive (false);
	}

	/*public override void Update()
	{
		base.Update ();
        if (nextStep)
        {
            //we are in game 
            nextStep = false;
            stage++;
            switch (stage)
            {
                case 0:
                    IntroText();
                    break;
                case 1:
                    IntroText2();
                    break;
                case 2:
                    ShowTutorial(); //show tutorial
                    break;
                case 3:
                    StartGame(); //start game
                    break;
                case 4:
                    PostGame();
                    break;
                case 5:
                    PostGameQuestions(); //summary questions
                    break;
                case 6:
                    GoToNextScene();
                    break;
                default:
                    break;
            }
        }
       
		
	}*/

	protected override void PostGame()
	{
        ToggleMinimap(false);
        im.gameObject.SetActive(false);
        playerFish.enabled = false;
        roundTimerActive = false;
		string t0 = "Total time: " + scoreText.TimeAsTimerString (roundTime);
		textPanel.gameObject.SetActive (true);
		textPanel.SetText (t0);
		textPanel.StartWriting ();
        float scorePerc = GetTimedRoundScore();
        ProgressTracker.Instance.SetRoundScore(scorePerc, 1);
        ProgressTracker.Instance.SubmitProgress(2);
    }

	/*private void IntroText()
	{
		string t0 = "Dinosaurs \n";
		string t1 = "Are \n";
		string t2 = "Aliens \n";
		textPanel.gameObject.SetActive (true);
		textPanel.SetText (t0 + t1 + t2);
		textPanel.StartWriting ();
	}

	private void IntroText2()
	{
		string t0 = "Opportunist Speicies: ";//They thrive during opportune times. You will find high populations during certain times of the year, but not so much during others. \n";
		/*string t1 = "<B>Short Generation times</B>: To take advantage of the opportunity during the lifetime of the opportunity \n";
		string t2 = "Small Bodies: Need to grow up and mature quickly \n";
		string t3 = "Lots of Eggs: Many other species will flock to this lush grounds during this brief window of oppotunity, plenty will be feeding \n";
		string t4 = "No Parental Care: With lots of kids, parental care requires too much energy \n";
		textPanel.gameObject.SetActive (true);
		textPanel.SetText (t0);// + t1 + t2 + t3 + t4);
		textPanel.StartWriting ();
	}*/

	protected override void PostGameQuestions()
	{
        
        scoreText.gameObject.SetActive(false);
       
		//frogCinematic.StartAridCinematic ();
		graphManager.gameObject.SetActive (true);
		graphManager.titleText.text = "What aspects of reproductive whatever";

		Slot s1 = new Slot ();
		s1.SetQuestion ("Body size","Select what size frogs are relative to most animals in the animal kingdom");
		s1.SetAns (1, "Small", true, "F");
		s1.SetAns (2, "Meduim", false, "What? You think they jump to the sky?");
		s1.SetAns (3, "Large", false, "Ya.. no comment, id say try again but I don't see that would help");

		Slot s2 = new Slot ();
		s2.SetQuestion ("Dinosaurs became birds","True or false, simple shit dont strain your brain");
		s2.SetAns (1, "True", true, "Correct: Dinosaurs hyper evolved when the astroid fused atoms in unstable isotopes");
		s2.SetAns (2, "False", false, "You can't handle the truth");

		graphManager.AddSlot (s1);
		graphManager.AddSlot (s2);
	}

	/*private void ShowTutorial()
	{
        tutorial.SetActive (true);
	}*/

	protected override void StartGame()
	{
        ToggleMinimap(true);
        scoreText.gameObject.SetActive(true);
        playerFish.gameObject.SetActive (true);
		roundTimerActive = true;
		trackScoreAsTime = true;
		im.gameObject.SetActive (true);
	}

    public void ToggleMinimap(bool setActive)
    {
        miniMap.SetActive(setActive);
    }

	public void GameFinished()
	{
        ToggleMinimap(false);
        nextStep = true;
	}

	/*public override void TutorialClosed()
	{
        tutorial.SetActive (false);
		nextStep = true;
	}

	private void StepFive()
	{
		string t0 = "Not  \n";
		string t1 = "<B>Short Generation times</B>: To take advantage of the opportunity during the lifetime of the opportunity \n";
		string t2 = "Small Bodies: Need to grow up and mature quickly \n";
		string t3 = "Lots of Eggs: Many other species will flock to this lush grounds during this brief window of oppotunity, plenty will be feeding \n";
		string t4 = "No Parental Care: With lots of kids, parental care requires too much energy \n";
		textPanel.gameObject.SetActive (true);
		textPanel.SetText (t0 + t1 + t2 + t3 + t4);
		textPanel.StartWriting ();
	}

	/*public override void AnsweredGraphCorrectly ()
	{
		graphManager.gameObject.SetActive (false);
		graphManager.ResetGraphManager ();
		nextStep = true;
	}

	public override void TextButtonNextPressed ()
	{
		textPanel.gameObject.SetActive (false);
		nextStep = true;
	}*/

	public void ReachedCheckpoint(Vector3 checkPt)
	{
		lastCheckpt = checkPt;	
		isFirstCheckpoint = false;
	}

	public void PlayerDied(PlayerFish playerFish)
	{
		destroyAllFish ();
		playerFish.transform.position = lastCheckpt;
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
