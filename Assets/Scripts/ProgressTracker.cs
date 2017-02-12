using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class ProgressTracker {

	private static ProgressTracker instance;

	public static ProgressTracker Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new ProgressTracker();
			}
			return instance;
		}
	}

	float[] roundScores = new float[5];
	float[] roundMult = new float[5];
    float maxScorePerRound = 100;
    float roundMultMax = .5f; //50% increase at 100% bonus
    float lossPerQuizAttempt = .05f;


	private ProgressTracker()
	{
		for (int i = 0; i < 5; i++) 
		{
			roundScores [i] = 0;
			roundMult [i] = 0;
		}
	}

	public void SetRoundScore(float score, int round)
	{
		roundScores [round] = score;
	}

	public void SetRoundMult(float score, int round, int tries)
	{
        score = Mathf.Max(score - (tries * lossPerQuizAttempt), 0);
		roundMult [round] = score;
	}

	public void SubmitProgress(int progressNumber)
	{
		float totalScore = 0;
		for (int i = 0; i < 5; i++) 
		{
            totalScore += roundScores [i] * (1 + roundMult [i]* roundMultMax) * maxScorePerRound;
            Debug.Log(string.Format("{0} : {1}", roundScores[i], roundMult[i]));
            Debug.Log(string.Format("total score in {0} iteration: {1}", i, totalScore));
        }
        LOLSDK.Instance.SubmitProgress((int)totalScore, progressNumber, 10);// SCORE, CURRENTPROGRESS, MAXPROGRESS
    }
}
