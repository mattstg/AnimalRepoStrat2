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
        introLessons = 3;
        outroLessons = 1;
        lessonType = LessonType.Duck;
        roundTimeToGetFullScore = 90;
		stage = -1;
		nextStep = true;
	}

    protected override void PostGame()
    {
        playerDuck.GetComponent<PlayerDuck>().abilityBar.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        roundTimerActive = false;
        im.enabled = false;
        int ducklingsSaved = (int)GameObject.FindObjectOfType<DuckEndZone>().ducklingsSaved;
        float timeScore = GetTimedRoundScore();
        float scorePerc = ((float)ducklingsSaved/10f) * timeScore;
        string toOut = "";
        if (gameForceEnded && ducklingsSaved == 0)
        {
            toOut = "You have ran out of time, with night approaching, predators will be more abundant and active, it is unlikely you or your young will survive";
        }
        else if (ducklingsSaved == 0)
        {
            toOut = "Although you have made it to the river, none of your ducklings have, you will have to try again in the future and hope for a better outcome";
        }
        else
            toOut = "You have made it in " + scoreText.TimeAsTimerString(roundTime) + " and with " + ducklingsSaved + " ducklings. The baby ducklings can now take care of themselves, searching for food and shelter";

        textPanel.gameObject.SetActive(true);
        textPanel.SetText(toOut);
        textPanel.StartWriting();
        scoreText.gameObject.SetActive(false);
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
