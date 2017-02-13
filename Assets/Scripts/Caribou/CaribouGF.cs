using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class CaribouGF : GameFlow {

	//public FrogCinematic frogCinematic;
	//public GameObject tutorialScreen;
	int stage = 0;
	public InputManager im;
    public GameObject tutorialImage;

    public Transform playerTransform;
    public Transform playerCalf;
    public List<Transform> allParentTransforms; //bull, wolves, reindeers calves, leaders

    Dictionary<Transform, Vector2> wolfStartingPositions;
    Dictionary<Transform, Vector2> savedPosition;
    Vector2 lastCheckpoint;

    public override void StartFlow()
	{
		stage = -1;
		nextStep = true;
        roundTimeToGetFullScore = 210;
        playerTransform.gameObject.SetActive(false);
        SaveCheckpoint(playerTransform.position);
        //
        foreach (Transform t in allParentTransforms)
        {
            if(t.name == "WolfManager")
            {
                wolfStartingPositions = new Dictionary<Transform, Vector2>();
                foreach (Transform wolf in t)
                    wolfStartingPositions.Add(wolf, wolf.position);
            }
            t.gameObject.SetActive(false);
            
        }
    }

	public override void Update()
	{
        base.Update();
        if (!nextStep)
            return;
        //we are in game 
        nextStep = false;
        stage++;
		switch (stage)
        {
		    case 0:
		    	IntroText ();
		    	break;
		    case 1:
		    	IntroText2 ();
		    	break;	
		    case 2:
		    	ShowTutorial (); //show tutorial
		    	break;
		    case 3:
		    	StartGame (); //start game
		    	break;
			case 4:
				PostGame ();
				break;
		    case 5:
		    	PostGameQuestions (); //summary questions
		    	break;
               case 6:
                   GoToNextScene();
                   break;
		    default:
		    	break;
		}		
		
	}

	private void PostGame(){
        playerTransform.GetComponent<PlayerCaribou>().abilityBar.gameObject.SetActive(false);
        roundTimerActive = false;
		//nextStep = true;
        scoreText.gameObject.SetActive(true);
        im.enabled = false;
        ProgressTracker.Instance.SetRoundScore(GetTimedRoundScore(), 4);
        ProgressTracker.Instance.SubmitProgress(8);
        string t0 = "You completed the run in " + scoreText.TimeAsTimerString(roundTime);
        textPanel.gameObject.SetActive(true);
        textPanel.SetText(t0);
        textPanel.StartWriting();

    }

	private void IntroText()
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
		string t4 = "No Parental Care: With lots of kids, parental care requires too much energy \n";*/
		textPanel.gameObject.SetActive (true);
		textPanel.SetText (t0);// + t1 + t2 + t3 + t4);
		textPanel.StartWriting ();
	}

	private void PostGameQuestions()
	{
		
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

	private void ShowTutorial()
	{
        tutorialImage.SetActive (true);
	}

	private void StartGame()
	{
        playerTransform.GetComponent<PlayerCaribou>().abilityBar.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        im.gameObject.SetActive (true);
		roundTimerActive = true;
        playerTransform.gameObject.SetActive(true);
        foreach(Transform t in allParentTransforms)
            t.gameObject.SetActive(true);
    }

	public void GameFinished(BowerBird winner, float score)
	{
		nextStep = true;
	}

    public override void TutorialClosed()
    {
        tutorialImage.SetActive (false);
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

	public override void AnsweredGraphCorrectly ()
	{
		graphManager.gameObject.SetActive (false);
		graphManager.ResetGraphManager ();
		nextStep = true;
	}

	public override void TextButtonNextPressed ()
	{
		textPanel.gameObject.SetActive (false);
		nextStep = true;
	}

    public void PlayerCalfDied()
    {
        LoadCheckpoint();
    }


    public void SaveCheckpoint(Vector2 checkpointPos)
    {
        lastCheckpoint = checkpointPos;
        savedPosition = new Dictionary<Transform, Vector2>();
        foreach(Transform parentTransform in allParentTransforms)
            if(parentTransform.name != "WolfManager")
                foreach(Transform t in parentTransform)
                    savedPosition.Add(t, t.position);
    }

    public void LoadCheckpoint()
    {
        foreach (KeyValuePair<Transform, Vector2> kv in savedPosition)
        {
            if (!kv.Key.gameObject.activeInHierarchy)
                kv.Key.gameObject.GetComponent<FlockingAI>().Undies();
            kv.Key.position = kv.Value;
            kv.Key.eulerAngles = new Vector3(0,0,90);
        }
        foreach (KeyValuePair<Transform, Vector2> kv in wolfStartingPositions)
            kv.Key.position = kv.Value;
        playerTransform.position = lastCheckpoint;
        playerCalf.position = new Vector2(playerTransform.position.x, playerTransform.position.y - 8);
        playerCalf.GetComponent<FlockingAI>().Undies();
    }
}
