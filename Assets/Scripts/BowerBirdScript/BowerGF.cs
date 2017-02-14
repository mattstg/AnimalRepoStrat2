using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class BowerGF : GameFlow {

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

	protected override void StartGame()
	{
        bowerBirdsParent.SetActive(true);
        scoreText.gameObject.SetActive(true);
		playerBB.gameObject.SetActive (true);
		roundTimerActive = true;
		im.enabled = true;
		bowerDirection.SetActive (true);
	}

	public void CheatFinishGame()
	{
		nextStep = true;
	}

	public void GameFinished(BowerBird winner, float score)
	{
		nextStep = true;
	}

    protected override void PostGame()
    {
		roundTimerActive = false;
        scoreText.gameObject.SetActive(false);
        im.enabled = false;
        string t0 = "Score: " + score;
        textPanel.gameObject.SetActive(true);
        textPanel.SetText(t0);
        textPanel.StartWriting();
        float scorePerc = Mathf.Min(1, score / scoreFor100);
        ProgressTracker.Instance.SetRoundScore(scorePerc, 2);
        ProgressTracker.Instance.SubmitProgress(4);
    }
	
}
