﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class BowerGF : GameFlow {

	public PlayerBowerBird playerBB;
	public FemaleBowerBird femaleBB;
	public GameObject bowerDirection;
	public InputManager im;
    public GameObject bowerBirdsParent;
    public Transform bowerArrow;
	public string winningText = "Unfortunately none of the bower birds in this region has attracted the female and she has left to find greener pastures! Perhaps the next female will be less picky!";
	private bool someoneWon = false;
	float scoreFor100 = 24;

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
			winningText = "You have attracted the attention of the female! You win with a Bower score of ";
		} else {
			//enemy bower wins
			winningText = "One of your rivals has attracted the attention of the female with a Bower score of ";
		}
		nextStep = true;
	}

    protected override void PostGame()
    {
        bowerArrow.gameObject.SetActive(false);
        roundTimerActive = false;
        scoreText.gameObject.SetActive(false);
        im.enabled = false;
		string t0 = winningText;
		if (someoneWon) {
			t0 = winningText + score + "!";
		}
        textPanel.gameObject.SetActive(true);
        textPanel.SetText(t0);
        textPanel.StartWriting();
        float scorePerc = Mathf.Min(1, score / scoreFor100);
        ProgressTracker.Instance.SetRoundScore(scorePerc, 2);
        ProgressTracker.Instance.SubmitProgress(4);
    }
	
}
