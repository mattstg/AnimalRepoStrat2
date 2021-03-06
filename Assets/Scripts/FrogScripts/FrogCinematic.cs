﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class FrogCinematic : MonoBehaviour {

    enum FrogCinematicStage { None ,BecomingWet, BecomingArid }

	int startingFrogs = 40;
	Vector2 spawnBoundry = new Vector2(6,4);
	//float spawnOffsetRange = 2.5f;

	bool updateGroundTransformation = false;
	bool groundIsMoistening = false;
	float transformationCounter = 0;
	float timeToTransformation = 5f;
    FrogCinematicStage currentStage = FrogCinematicStage.None;
    bool evacuateAtEndOfCinematic = false;

    Puddle puddle;
	SpriteRenderer aridBg;
	SpriteRenderer wetBg;

	public void Start()
	{
        puddle = FrogGV.frogWS.puddle;
		aridBg = FrogGV.frogWS.aridBackground;
		wetBg  = FrogGV.frogWS.moistBackground;
	}

	public void StartFirstAridCinematic()
	{
		transformationCounter = 0;
        timeToTransformation = 5;
        currentStage = FrogCinematicStage.BecomingArid;
        foreach (Transform t in FrogGV.frogWS.frogParent)
			Destroy (t.gameObject);
		foreach (Transform t in FrogGV.frogWS.tadpoleParent)
			Destroy (t.gameObject);
        FrogGV.masterList = new List<Frog>();
        FrogGV.ClearTadpoleList();
        puddle.ToggleColliders(false);
    }

    public void StartSecondAridCinematic()
    {
        transformationCounter = 0;
        timeToTransformation = 20;
        currentStage = FrogCinematicStage.BecomingArid;
        evacuateAtEndOfCinematic = true;
        
    }

    public void StartWetlandCinematic() //also sets up level
	{
        transformationCounter = 0;
        timeToTransformation = 10;
        currentStage = FrogCinematicStage.BecomingWet;
        for (int i = 0; i <= startingFrogs; i++) {
			GameObject newFrog = Instantiate(Resources.Load("Prefabs/Frog")) as GameObject;
			newFrog.transform.SetParent(FrogGV.frogWS.frogParent);
			newFrog.transform.position = GetRandomSpawnLoc();
            Frog.FrogInfo fi = new Frog.FrogInfo(0, false, Random.Range(0f, 1f) > .5f);
			newFrog.GetComponent<Frog>().InitializeFrog(fi);
			newFrog.GetComponent<Frog> ().outtaBounds = true;
		}
        SnakeManager.Instance.SetupGame();
        puddle.ToggleColliders(false);
    }

	public void Update()
	{
        switch(currentStage)
        {
            case FrogCinematicStage.None:
                break;
            case FrogCinematicStage.BecomingArid:
                UpdateBecomingArid();
                break;
            case FrogCinematicStage.BecomingWet:
                UpdateBecomingWet();
                break;
        }
	}

    private void UpdateBecomingArid()
    {
        transformationCounter += Time.deltaTime;
        float progress = 1 - (transformationCounter / timeToTransformation);
        Color moistColor = wetBg.color;
        moistColor.a = progress;
        wetBg.color = moistColor;
        //puddle.SetAlpha(progress);
        float delay = 1f / (1f/2f);          //puddle finishes disappearing when progress == 1/2
        float newAlpha = Mathf.Min(Mathf.Max((progress * delay) - delay + 1f, 0f), 1f);
        puddle.SetAlpha(newAlpha);

        //puddle.transform.localScale = Vector2.Lerp(puddle.originalSize, new Vector2(.01f, .01f), (transformationCounter / timeToTransformation));
        //puddle.carryingCapacity = (int)(( 1- (transformationCounter / timeToTransformation)) * puddle.originalCarryingCapacity);

        if (transformationCounter >= timeToTransformation)
        {
            if(evacuateAtEndOfCinematic)
            {
                foreach (Transform t in FrogGV.frogWS.frogParent)
                    t.GetComponent<Frog>().leaveMap = true;
                puddle.ToggleColliders(false);
                foreach (Transform t in FrogGV.frogWS.tadpoleParent)
                    Destroy(t.gameObject);
                FrogGV.ClearTadpoleList();
            }            
            currentStage = FrogCinematicStage.None;
        }
    }

    private void UpdateBecomingWet()
    {
        transformationCounter += Time.deltaTime;
        float progress = transformationCounter / timeToTransformation;
        Color moistColor = wetBg.color;
        moistColor.a = progress;
        wetBg.color = moistColor;
        //puddle.SetAlpha(progress);
        float delay = 1f / (1f - (1f/2f));      //puddle begin appearing when progress == 1/2
        float newAlpha = Mathf.Min(Mathf.Max((progress * delay) - delay + 1f, 0f), 1f);
        puddle.SetAlpha(newAlpha);
        //puddle.transform.localScale = Vector2.Lerp( new Vector2(.01f, .01f), puddle.originalSize, (transformationCounter / timeToTransformation));
        if (transformationCounter >= timeToTransformation)
        {
            puddle.ToggleColliders(true);
            currentStage = FrogCinematicStage.None;
        }
        //puddle.carryingCapacity = (int)((transformationCounter / timeToTransformation) * puddle.originalCarryingCapacity);
    }




    private Vector2 GetRandomSpawnLoc()
	{
		//float spawnOffset = Random.Range (0, spawnOffsetRange);
		float spawnX;// = spawnBoundry.x * ((Random.Range(0f,1f) > .5f)?1:-1);
		float spawnY;// = spawnBoundry.y * ((Random.Range(0f,1f) > .5f)?1:-1);
		if (MathHelper.Fiftyfifty ()) {
			spawnX  = Random.Range(-spawnBoundry.x,spawnBoundry.x);
			spawnY  = spawnBoundry.y * ((MathHelper.Fiftyfifty ())?1:-1);
		} else {
			spawnX = spawnBoundry.x * ((MathHelper.Fiftyfifty ())?1:-1);
			spawnY = Random.Range(-spawnBoundry.y,spawnBoundry.y);
		}
        //Vector2 spawnLoc = new Vector2 (spawnX + Mathf.Sign (spawnX) * spawnOffset, spawnY + Mathf.Sign (spawnY) * spawnOffset);
        Vector2 spawnLoc = new Vector2(spawnX, spawnY);

        return spawnLoc;
	}
}
