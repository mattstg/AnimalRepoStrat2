using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogGF : GameFlow {

	public FrogCinematic frogCinematic;
	public GameObject tutorialScreen;
	int stage = 0;
	bool nextStep = false;
	public Frog playerFrog;
	public InputManager im;
    float gameTimer = 180; //3 mins

	public override void StartFlow()
	{
		stage = -1;
		nextStep = true;
	}

	public void Update()
	{
        if (stage == 3)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer <= 0)
                GameFinished();
        }


		if (!nextStep)
			return;


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
			PostGameQuestions (); //summary questions
			break;
		default:
			break;
		}
		nextStep = false;
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
		im.gameObject.SetActive (false);
		frogCinematic.StartFirstAridCinematic ();
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
		tutorialScreen.SetActive (true);
	}

	private void StartGame()
	{
		playerFrog.gameObject.SetActive (true);
		frogCinematic.StartWetlandCinematic ();
		Camera.main.gameObject.GetComponent<CameraFollow> ().toFollow = playerFrog.transform;
		Camera.main.gameObject.GetComponent<CameraFollow> ().SetZoom (3);
		im.gameObject.SetActive (true);
		playerFrog.CreateFrog (new Frog.FrogInfo(0,true, true),true);
	}

	public void GameFinished()
	{
		nextStep = true;
        int matureDescendants = 0;
        foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().frogParent)
        {
            if (t.GetComponent<Frog>().isPlayerDescendant)
                matureDescendants++;
        }
        Debug.Log("you have a total of: " + matureDescendants);
    }

	public void TutorialFinished()
	{
		tutorialScreen.SetActive (false);
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
