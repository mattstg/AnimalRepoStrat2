using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class GameFlow : MonoBehaviour {

	public bool nextStep = false;
	public bool canTimeOut = true;
	private float safeGameTime = 0;

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
    protected float roundTimeToGetFullScore = 0;

	public float maxRoundTime = 180;

	public void Start()
	{		
        if(!MainMenu.Sound_Active)
        {
            AudioSource[] adsrc = GameObject.FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in adsrc)
                a.gameObject.SetActive(false);
            AudioLooper al = GameObject.FindObjectOfType<AudioLooper>();
            if(al)
                al.gameObject.SetActive(false);

        }
		StartFlow ();
	}

	public void ChangeScore(int newScore)
	{
		scoreText.SetScore (newScore);
		_score = newScore;
	}

    protected float GetTimedRoundScore()
    {
        return 1 - ((roundTime - roundTimeToGetFullScore) / (maxRoundTime - roundTimeToGetFullScore));
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
			safeGameTime = roundTime;
		}
		if (trackScoreAsTime) {
			scoreText.SetScoreTime (roundTime);
		}
		if (safeGameTime > maxRoundTime) {
			nextStep = true;
			safeGameTime = 0;
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

    public void ForceNextStep() //only to be used by button to skip game
    {
        nextStep = true; 
    }

	public void GoToNextScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (nextSceneName);
	}
}
