using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class BowerGF : GameFlow {

	public PlayerBowerBird playerBB;
	public FemaleBowerBird femaleBB;
	public GameObject bowerDirection;
	public InputManager im;
    public GameObject bowerBirdsParent;
    public Transform bowerArrow;
	string winningText = "Unfortunately, none of the male bowerbirds in this region have attracted the female, and so <b>she has left in search of other candidates</b>!\n\n" +
        "Perhaps the next female will be less picky!";
	private bool someoneWon = false;
	float scoreFor100 = 21;

	protected override void StartFlow()
	{
        introLessons = 3;
        outroLessons = 1;
        lessonType = LessonType.Bower;
        stage = -1;
		nextStep = true;
		maxRoundTime = 150;
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

	public void GameFinished(BowerBird winner, float _score)
	{
		someoneWon = true;
		score = (int) _score;
		if (winner.isPlayer) {
			//player winner
			winningText = "You have attracted the attention of the female!\n\n" +
                "<b>You win</b> She will use the nest as her home to raise the young";
		} else {
			//enemy bower wins
			winningText = "One of your rivals has attracted the attention of the female!\n\n" +
                "Someone else impressed her with thier nest before you could";
		}
		nextStep = true;
	}

    protected override void PostGame()
    {
        bowerBirdsParent.SetActive(false);
        scoreText.gameObject.SetActive(false);
        playerBB.gameObject.SetActive(false);
        bowerArrow.gameObject.SetActive(false);
        roundTimerActive = false;
        safeGameTime = 0;
        roundTime = 0;

        scoreText.gameObject.SetActive(false);
        im.enabled = false;
		string t0 = winningText;
		if (someoneWon) {
			t0 = winningText;
		}
        textPanel.gameObject.SetActive(true);
        textPanel.SetText(t0);
        textPanel.StartWriting();
        float scorePerc = Mathf.Min(1, score / scoreFor100);
        ProgressTracker.Instance.SetRoundScore(scorePerc, 2);
        ProgressTracker.Instance.SubmitProgress(4);
    }
	
}
