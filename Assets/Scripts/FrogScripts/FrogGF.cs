﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class FrogGF : GameFlow {

    public static readonly int maxFrogCount = 75;
    

    public FrogCinematic frogCinematic;
	//int stage = 0;
	//bool nextStep = false;
	public Frog playerFrog;
	public InputManager im;
    float gameTimer = 150; //3 mins
    bool secondCinematicStarted = false;
    int matureDescendants = 0;

    int frogsForMaxScore = 25;
    

	protected override void StartFlow()
	{
        introLessons = 3;
        outroLessons = 1;
        lessonType = LessonType.Frog;
		nextSceneName = "FishScene";
		stage = -1;
		nextStep = true;

        //Pre game visual
        foreach (Transform t in FrogGV.frogWS.tadpoleParent)
            t.GetComponent<Tadpole>().enabled = true;
        SnakeManager.Instance.SetupGame();
    }

	public override void Update()
	{
        float dt = Time.deltaTime;
        if (stage == 2)
        {
            gameTimer -= dt;
            if (gameTimer <= 0)
                GameFinished();
            if (!secondCinematicStarted && gameTimer <= 20)
            {
                secondCinematicStarted = true;
                frogCinematic.StartSecondAridCinematic();
            }
        }

        for(int i = FrogGV.masterList.Count - 1; i > 0; i--)
            FrogGV.masterList[i].UpdateFrog(dt);
        for (int i = FrogGV.masterTadpoleList.Count - 1; i > 0; i--)
            FrogGV.masterTadpoleList[i].UpdateTadpole(dt);

        SnakeManager.Instance.UpdateSnakeManager(dt);
        base.Update();
	}

    protected override void PostGame()
    {
        playerFrog.gameObject.SetActive(false);
        FrogGV.RemoveFrogFromMasterList(playerFrog, true);
        im.enabled = false;
        float scorePerc = Mathf.Min((float)matureDescendants / (float)frogsForMaxScore, 1);
        string t0 = "The puddle has evaporated, and the land has returned to its usual arid state.\n\n" +
            "You are now the proud ancestor of " + matureDescendants + " descendants!";
        textPanel.gameObject.SetActive(true);
        textPanel.SetText(t0);// + t1 + t2 + t3 + t4);
        textPanel.StartWriting();
		scoreText.gameObject.SetActive (false);
        //float scorePerc = Mathf.Min((float)matureDescendants / (float)frogsForMaxScore, 1);
        ProgressTracker.Instance.SetRoundScore(scorePerc, 0);
        ProgressTracker.Instance.SubmitProgress(1);
    }

    protected override void OpenTutorial()
    {
		frogCinematic.StartFirstAridCinematic ();
        base.OpenTutorial();
	}

    protected override void StartGame()
	{
		scoreText.gameObject.SetActive (true);
		playerFrog.gameObject.SetActive (true);
		frogCinematic.StartWetlandCinematic ();
		Camera.main.gameObject.GetComponent<CameraFollow> ().toFollow = playerFrog.transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);
		Camera.main.gameObject.GetComponent<CameraFollow> ().SetZoom (2);
		im.gameObject.SetActive (true);
		playerFrog.InitializeFrog (new Frog.FrogInfo(0,true, true),true);        
    }

	public void GameFinished()
	{
		nextStep = true;
        matureDescendants = 0;
        foreach (Transform t in FrogGV.frogWS.frogParent)
        {
            if (t.GetComponent<Frog>().isPlayerDescendant)
                matureDescendants++;
        }
        Camera.main.GetComponent<CameraFollow>().toFollow = null;
        Camera.main.transform.position = new Vector3(-.06f, 0, -10);
        Camera.main.orthographicSize = 2.9f;
        FrogGV.masterList.Remove(playerFrog);
    }

	
}
