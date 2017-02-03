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
}
