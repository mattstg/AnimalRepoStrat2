using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour {

	public string nextSceneName = "";
    public GameObject tutorial;
	public GraphManager graphManager;
	public TextPanel textPanel;
	public ScoreText scoreText;

	private int _score = 0;
	public int score {set{ChangeScore (value); }get{return _score; }}
	public bool trackScoreAsTime = false;
	protected bool roundTimerActive = false;
	protected float roundTime = 0;

	public void Start()
	{		
		StartFlow ();
	}

	public void ChangeScore(int newScore)
	{
		scoreText.SetScore (newScore);
		_score = newScore;
	}

	public virtual void AnsweredGraphCorrectly()
	{
		
	}

	public virtual void TextButtonNextPressed()
	{

	}

	public virtual void Update() //needs to be called by child
	{
		if (roundTimerActive) {
			roundTime += Time.deltaTime;
		}
		if (trackScoreAsTime) {
			scoreText.SetScoreTime (roundTime);
		}
	}

	public virtual void StartFlow()
	{

	}

	public virtual void TutorialClosed()
	{

	}

	public virtual void OpenTutorial()
	{

	}

	public void GoToNextScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (nextSceneName);
	}
}
