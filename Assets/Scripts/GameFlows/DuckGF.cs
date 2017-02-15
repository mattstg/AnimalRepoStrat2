using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class DuckGF : GameFlow {

	//public FrogCinematic frogCinematic;
	//public GameObject tutorialScreen;
	public InputManager im;
    public GameObject playerDuck;
    public GameObject ducklingParent;

    protected override void StartFlow()
	{
        introLessons = 2;
        outroLessons = 3;
        lessonType = LessonType.Duck;
        roundTimeToGetFullScore = 90;
		stage = -1;
		nextStep = true;
	}

	/*public override void Update()
	{
        base.Update();
        if (nextStep)
        {
            stage++;
            nextStep = false;
            switch (stage)
            {
                case 0:
                    IntroText();
                    break;
                case 1:
                    IntroText2();
                    break;
                case 2:
                    ShowTutorial(); //show tutorial
                    break;
                case 3:
                    StartGame(); //start game
                    break;
                case 4:
                    PostGame();
                    break;
                case 5:
                    PostGameQuestions(); //summary questions
                    break;
                case 6:
                    GoToNextScene();
                    break;
                default:
                    break;
            }
        }
		
	}*/

    protected override void PostGame()
    {
        playerDuck.GetComponent<PlayerDuck>().abilityBar.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        roundTimerActive = false;
        nextStep = true;
        im.enabled = false;
        int ducklingsLeft = ducklingParent.transform.childCount;
        float timeScore = GetTimedRoundScore();
        float scorePerc = ducklingsLeft * timeScore;
        ProgressTracker.Instance.SetRoundScore(scorePerc, 3);
        ProgressTracker.Instance.SubmitProgress(6);
    }

	protected override void StartGame()
	{
        playerDuck.SetActive(true);
        playerDuck.GetComponent<PlayerDuck>().abilityBar.gameObject.SetActive(true);
        ducklingParent.SetActive(true);
        im.enabled = true;
        scoreText.gameObject.SetActive(true);
        roundTimerActive = true;
    }

	public void GameFinished()
	{
		nextStep = true;
	}

    public void GameFinished(float babyDucksSaved)
    {
        Debug.Log("game finished: " + babyDucksSaved);
        nextStep = true;
    }
    
}
