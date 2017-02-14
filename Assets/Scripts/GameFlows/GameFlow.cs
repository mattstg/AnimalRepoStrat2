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

    protected AudioLooper audioLooper;

	private int _score = 0;
	public int score {set{ChangeScore (value); }get{return _score; }}
	public bool trackScoreAsTime = false;
	protected bool roundTimerActive = false;
	protected float roundTime = 0;
    protected float roundTimeToGetFullScore = 0;
    protected int stage = 0;

    protected LessonType lessonType;
    protected int introLessons;
    protected int outroLessons;
    protected int currentLesson;

    public float maxRoundTime = 180;

	public void Start()
	{
        audioLooper = new AudioLooper();
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
        graphManager.gameObject.SetActive(false);
        graphManager.ResetGraphManager();
        nextStep = true;
    }

    public virtual void TextButtonNextPressed()
	{
        if (stage != 3) //post game has a text button too
        {
            currentLesson++;
            if (currentLesson == introLessons || currentLesson >= introLessons + outroLessons) //means you just reached end of intro or outro lessons
            {
                textPanel.gameObject.SetActive(false);
                nextStep = true;
            }
            else
            {
                DisplayLesson();
            }
        }
        else
        {
            textPanel.gameObject.SetActive(false);
            nextStep = true;
        }
	}

	public virtual void Update() //needs to be called by child
	{
        audioLooper.Update();
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

        if (nextStep)
        {
            nextStep = false;
            stage++;
            switch (stage)
            {
                case 0:
                    StartMusic();
                    DisplayLesson();
                    break;
                case 1:
                    OpenTutorial(); //show tutorial
                    break;
                case 2:
                    StartGame(); //start game
                    break;
                case 3:
                    PostGame();
                    break;
                case 4:
                    DisplayLesson();
                    break;
                case 5:
                    PostGameQuestions(); //summary questions
                    break;
                case 6:
                    CloseMusic();
                    GoToNextScene();
                    break;
                default:
                    break;
            }
        }

    }

    protected void StartMusic()
    {
        LOLAudio.Instance.PlayAudio(lessonType + "Music.mp3", true);
        audioLooper.StartAudioLooper(lessonType);
    }

    protected void CloseMusic()
    {
        LOLAudio.Instance.StopAudio(lessonType + "Music.mp3");
        audioLooper.CloseAudioLooper();
    }

    protected virtual void StartGame()
    {

    }

    protected virtual void PostGame()
    {

    }

    protected virtual void DisplayLesson()
    {
        string lessonText = LessonRetriever.Instance.RetrieveLesson(lessonType, currentLesson);
        textPanel.gameObject.SetActive(true);
        textPanel.SetText(lessonText);
        textPanel.StartWriting();
    }

    protected virtual void PostGameQuestions()
    {
        QuestionRetriever.Instance.SetupQuestions(graphManager, lessonType);
    }

    protected virtual void StartFlow()
	{

	}

	public virtual void TutorialClosed()
	{
        tutorial.SetActive(false);
        nextStep = true;
    }

    protected virtual void OpenTutorial()
	{
        tutorial.SetActive(true);
    }

    public void ForceNextStep() //only to be used by button to skip game
    {
        nextStep = true; 
    }

	protected virtual void GoToNextScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (nextSceneName);
	}
}
