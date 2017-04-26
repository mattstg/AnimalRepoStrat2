using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class DuckGF : GameFlow {

	//public FrogCinematic frogCinematic;
	//public GameObject tutorialScreen;
	public InputManager im;
    public GameObject playerDuck;
    public GameObject ducklingParent;
    int ducklingsSaved;

    protected override void StartFlow()
	{
        introLessons = 3;
        outroLessons = 1;
        lessonType = LessonType.Duck;
        roundTimeToGetFullScore = 110;
		stage = -1;
		nextStep = true;
	}

    protected override void PostGame()
    {
        playerDuck.GetComponent<PlayerDuck>().abilityBar.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        roundTimerActive = false;
        im.enabled = false;
        GameObject.FindObjectOfType<DuckEndZone>().finished = true;
        float timeScore = GetTimedRoundScore();
        float scorePerc = (((float)ducklingsSaved/10f) + timeScore)/2;
        string toOut = "";
        if (gameForceEnded && ducklingsSaved == 0)
        {
            toOut = "You have run out of time.\n\n" +
                "With night approaching, predators will be more abundant and active. It is unlikely that you or your young will survive.";
        }
        else if (ducklingsSaved == 0)
        {
            toOut = "Although you have made it to the lake, none of your ducklings succeeded in making it that far.\n\n" +
                "You will have to try again next year, and hope for a better outcome.";
        }
        else
            toOut = "You successfully made it to the lake in " + scoreText.TimeAsTimerString(roundTime) + " with " + ducklingsSaved + " ducklings!";

        textPanel.gameObject.SetActive(true);
        textPanel.SetText(toOut);
        textPanel.StartWriting();
        scoreText.gameObject.SetActive(false);
        ProgressTracker.Instance.SetRoundScore(scorePerc, 3);
        ProgressTracker.Instance.SubmitProgress(7);
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

    public void GameFinished(int babyDucksSaved)
    {
        nextStep = true;
        ducklingsSaved = babyDucksSaved;
    }
    
}
