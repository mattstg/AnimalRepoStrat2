using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class FrogGF : GameFlow {

	public FrogCinematic frogCinematic;
	int stage = 0;
	//bool nextStep = false;
	public Frog playerFrog;
	public InputManager im;
    float gameTimer = 180; //3 mins
    bool secondCinematicStarted = false;
    int matureDescendants = 0;

    int frogsForMaxScore = 90;

	public override void StartFlow()
	{
		nextSceneName = "FishScene";
		stage = -1;
		nextStep = true;
	}

	public override void Update()
	{
        if (stage == 3)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer <= 0)
                GameFinished();
            if (!secondCinematicStarted && gameTimer <= 20)
            {
                secondCinematicStarted = true;
                frogCinematic.StartSecondAridCinematic();
            }
        }


		if (!nextStep)
			return;

        nextStep = false;
        stage++;
		switch (stage) {
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
			StartGame(); //start game
			break;
        case 4:
            GameResults();
            break;
		case 5:
			PostGameQuestions (); //summary questions
			break;
		case 6:
			GoToNextScene ();
			break;
		default:
			break;
		}
		
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

    private void GameResults()
    {
        string t0 = "Total Score: " + matureDescendants;
        textPanel.gameObject.SetActive(true);
        textPanel.SetText(t0);// + t1 + t2 + t3 + t4);
        textPanel.StartWriting();
		scoreText.gameObject.SetActive (false);
        float scorePerc = Mathf.Min((float)matureDescendants / (float)frogsForMaxScore, 1);
        ProgressTracker.Instance.SetRoundScore(scorePerc, 0);
        ProgressTracker.Instance.SubmitProgress(0);        
    }

    private void PostGameQuestions()
	{
        //LOLSDK.Instance.SubmitProgress(0, 0, 10);  SCORE, CURRENTPROGRESS, MAXPROGRESS
        im.gameObject.SetActive (false);
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
		frogCinematic.StartFirstAridCinematic ();
		tutorial.SetActive (true);
	}

	private void StartGame()
	{
		scoreText.gameObject.SetActive (true);
		playerFrog.gameObject.SetActive (true);
		frogCinematic.StartWetlandCinematic ();
		Camera.main.gameObject.GetComponent<CameraFollow> ().toFollow = playerFrog.transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
		Camera.main.gameObject.GetComponent<CameraFollow> ().SetZoom (3);
		im.gameObject.SetActive (true);
		playerFrog.CreateFrog (new Frog.FrogInfo(0,true, true),true);
	}

	public void GameFinished()
	{
		nextStep = true;
        matureDescendants = 0;
        foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().frogParent)
        {
            if (t.GetComponent<Frog>().isPlayerDescendant)
                matureDescendants++;
        }
        Camera.main.GetComponent<CameraFollow>().toFollow = null;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.orthographicSize = 5;
    }

	public override void TutorialClosed()
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
}
