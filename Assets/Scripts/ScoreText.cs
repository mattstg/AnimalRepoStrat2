using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

	Text textScore;
	// Use this for initialization
	void Awake () {
		textScore = GetComponent<Text> ();	
	}

	public void SetScore(int newScore)
	{
		textScore.text = newScore.ToString ();
	}

	public void SetScoreTime(float totalTime)
	{
		textScore.text = TimeAsTimerString (totalTime); 
	}

	public string TimeAsTimerString(float totalTime)
	{
		string sec = (((int)totalTime % 60).ToString()).PadLeft(2,'0');
		string min = (((int)totalTime / 60).ToString()).PadLeft(2,'0');
		string ms = (((int)((totalTime - (int)totalTime) * 100)).ToString()).PadLeft(2,'0');

		return  min +  ":" + sec + ":" + ms; 
	}

}
