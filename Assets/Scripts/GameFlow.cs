using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour {

	public string nextSceneName = "";
    public GameObject tutorial;
	public GraphManager graphManager;
	public TextPanel textPanel;
	protected ScoreText scoreText;

	private int _score = 0;
	public int score {set{ChangeScore (value); }get{return _score; }}

	public void Start()
	{
		scoreText = FindObjectOfType<ScoreText> ();
		scoreText.gameObject.SetActive (false);
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
