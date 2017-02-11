using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public void SetRoundMult(float score, int round)
	{
		roundMult [round] = score;
	}

	public float SubmitProgress()
	{
		float totalScore = 0;
		for (int i = 0; i < 5; i++) 
		{
			totalScore += roundScores [i] *	roundMult [i];
		}
		return totalScore;
	}
}
