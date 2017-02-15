using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class CaribouGF : GameFlow {

	//public FrogCinematic frogCinematic;
	//public GameObject tutorialScreen;
	public InputManager im;
    public GameObject tutorialImage;

    public Transform playerTransform;
    public Transform playerCalf;
    public List<Transform> allParentTransforms; //bull, wolves, reindeers calves, leaders

    Dictionary<Transform, Vector2> wolfStartingPositions;
    Dictionary<Transform, Vector2> savedPosition;
    Vector2 lastCheckpoint;

    protected override void StartFlow()
	{
        introLessons = 2;
        outroLessons = 3;
        lessonType = LessonType.Caribou;
        stage = -1;
		nextStep = true;
        roundTimeToGetFullScore = 210;
        playerTransform.gameObject.SetActive(false);
        SaveCheckpoint(playerTransform.position);
        //
        foreach (Transform t in allParentTransforms)
        {
            if(t.name == "WolfManager")
            {
                wolfStartingPositions = new Dictionary<Transform, Vector2>();
                foreach (Transform wolf in t)
                    wolfStartingPositions.Add(wolf, wolf.position);
            }
            t.gameObject.SetActive(false);
            
        }
    }


	protected override void PostGame(){
        playerTransform.GetComponent<PlayerCaribou>().abilityBar.gameObject.SetActive(false);
        roundTimerActive = false;
		//nextStep = true;
        scoreText.gameObject.SetActive(true);
        im.enabled = false;
        ProgressTracker.Instance.SetRoundScore(GetTimedRoundScore(), 4);
        ProgressTracker.Instance.SubmitProgress(8);

        string toOut = "";
        if (gameForceEnded)
            toOut = "Unfortunatly, you have ran out of time, and were unable to complete the migration run";
        else
            toOut = "You completed the migration run in " + scoreText.TimeAsTimerString(roundTime);
        textPanel.gameObject.SetActive(true);
        textPanel.SetText(toOut);
        textPanel.StartWriting();

    }

	protected override void StartGame()
	{
        playerTransform.GetComponent<PlayerCaribou>().abilityBar.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        im.gameObject.SetActive (true);
		roundTimerActive = true;
        playerTransform.gameObject.SetActive(true);
        foreach(Transform t in allParentTransforms)
            t.gameObject.SetActive(true);
    }

    public void PlayerCalfDied()
    {
        LoadCheckpoint();
    }


    public void SaveCheckpoint(Vector2 checkpointPos)
    {
        lastCheckpoint = checkpointPos;
        savedPosition = new Dictionary<Transform, Vector2>();
        foreach (Transform parentTransform in allParentTransforms)
            if (parentTransform.name != "WolfManager")
                foreach (Transform t in parentTransform)
                {
                    savedPosition.Add(t, t.position);
                }
    }

    public void LoadCheckpoint()
    {
        foreach (KeyValuePair<Transform, Vector2> kv in savedPosition)
        {
            if (!kv.Key.gameObject.activeInHierarchy)
                kv.Key.gameObject.GetComponent<FlockingAI>().Undies();
            kv.Key.position = kv.Value;
            kv.Key.eulerAngles = new Vector3(0,0,90);
            kv.Key.gameObject.AddComponent<AutoWPAssigner>().upwardsOnly = true; ;
        }
        foreach (KeyValuePair<Transform, Vector2> kv in wolfStartingPositions)
            kv.Key.position = kv.Value;
        playerTransform.position = lastCheckpoint;
        playerCalf.position = new Vector2(playerTransform.position.x, playerTransform.position.y - 8);
        playerCalf.GetComponent<FlockingAI>().Undies();
    }
}
