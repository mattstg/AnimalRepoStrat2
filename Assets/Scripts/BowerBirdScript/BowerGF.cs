using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class BowerGF : GameFlow {

	//public FrogCinematic frogCinematic;
	//public GameObject tutorialScreen;
	public PlayerBowerBird playerBB;
	public FemaleBowerBird femaleBB;
	public GameObject bowerDirection;
	public InputManager im;
    public GameObject bowerBirdsParent;

	float scoreFor100 = 35;

	protected override void StartFlow()
	{
        introLessons = 2;
        outroLessons = 3;
        lessonType = LessonType.Bower;
        stage = -1;
		nextStep = true;
	}

	/*public override void Update()
	{
        base.Update();
		if (nextStep)
        { 
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
				StartGame (); //start game
				break;
            case 4:
                PostGame();
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
		string t4 = "No Parental Care: With lots of kids, parental care requires too much energy \n";
		textPanel.gameObject.SetActive (true);
		textPanel.SetText (t0);// + t1 + t2 + t3 + t4);
		textPanel.StartWriting ();
	}*/

	protected override void PostGameQuestions()
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

	/*private void ShowTutorial()
	{
        //frogCinematic.StartAridCinematic ();
        tutorial.SetActive (true);
	}*/

	protected override void StartGame()
	{
        bowerBirdsParent.SetActive(true);
        scoreText.gameObject.SetActive(true);
		playerBB.gameObject.SetActive (true);
		//frogCinematic.StartWetlandCinematic ();
		roundTimerActive = true;
		im.enabled = true;
		bowerDirection.SetActive (true);
		//playerFrog.CreateFrog (true,true);
	}

	public void CheatFinishGame()
	{
		nextStep = true;
	}

	public void GameFinished(BowerBird winner, float score)
	{
		//play cinamatic
		nextStep = true;
	}

    protected override void PostGame()
    {
		roundTimerActive = false;
        scoreText.gameObject.SetActive(false);
        im.enabled = false;
        //nextStep = true;
        string t0 = "Score: " + score;
        textPanel.gameObject.SetActive(true);
        textPanel.SetText(t0);
        textPanel.StartWriting();
        float scorePerc = Mathf.Min(1, score / scoreFor100);
        ProgressTracker.Instance.SetRoundScore(scorePerc, 2);
        ProgressTracker.Instance.SubmitProgress(4);

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
	}*/
}
