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
    Dictionary<FlockingAI, CaribouSave> caribouSaves;
    Vector2 lastCheckpoint;
    int checkpointsPassed;

    protected override void StartFlow()
	{
        introLessons = 3;
        outroLessons = 2;
        lessonType = LessonType.Caribou;
        stage = -1;
		nextStep = true;
        roundTimeToGetFullScore = 165;
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


        im.enabled = false;
        roundTimerActive = false;
        playerTransform.gameObject.SetActive(false);
        foreach (Transform t in allParentTransforms)
            t.gameObject.SetActive(false);

        playerTransform.GetComponent<PlayerCaribou>().abilityBar.gameObject.SetActive(false);
		//nextStep = true;
        scoreText.gameObject.SetActive(false);
        float _score = GetTimedRoundScore();//checkpointsPassed
        _score = Mathf.Max(_score, (checkpointsPassed - 1) * .15f);
        _score = Mathf.Clamp(_score, 0, 1);

        ProgressTracker.Instance.SetRoundScore(_score, 4);
        ProgressTracker.Instance.SubmitProgress(8);

        string toOut = "";
        if (gameForceEnded)
            toOut = "Unfortunately, you have run out of time.\n\n" +
                "You were unable to complete the migration run.";
        else
            toOut = "You and your calf successfully escaped from the wolves in " + scoreText.TimeAsTimerString(roundTime) + "!";
        textPanel.gameObject.SetActive(true);
        textPanel.SetText(toOut);
        textPanel.StartWriting();

    }

	protected override void StartGame()
	{
        playerTransform.GetComponent<PlayerCaribou>().abilityBar.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        im.enabled = true;
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
        caribouSaves = new Dictionary<FlockingAI, CaribouSave>();
        foreach (Transform parentTransform in allParentTransforms)
            if (parentTransform.name != "WolfManager")
                foreach (Transform t in parentTransform)
                {
                    FlockingAI fa = t.GetComponent<FlockingAI>();
                    CaribouSave cs = new CaribouSave(t.position, fa.activeWaypoint);
                    caribouSaves.Add(fa, cs);
                }
        checkpointsPassed++;
    }

    public void LoadCheckpoint()
    {
        foreach (KeyValuePair<FlockingAI, CaribouSave> kv in caribouSaves)
        {
            if (!kv.Key.gameObject.activeInHierarchy)
                kv.Key.Undies();
            kv.Key.transform.position = kv.Value.pos;
            kv.Key.activeWaypoint = kv.Value.curWaypointIndex;
            kv.Key.transform.eulerAngles = new Vector3(0,0,90);
            //kv.Key.gameObject.AddComponent<AutoWPAssigner>().upwardsOnly = true; ;
        }
        foreach (KeyValuePair<Transform, Vector2> kv in wolfStartingPositions)
            kv.Key.position = kv.Value;
        playerTransform.position = lastCheckpoint;
        playerCalf.position = new Vector2(playerTransform.position.x, playerTransform.position.y - 8);
        playerCalf.GetComponent<FlockingAI>().Undies();
    }
}

class CaribouSave
{
    public Vector2 pos;
    public int curWaypointIndex;
    public CaribouSave(Vector2 _pos, int _curWaypointIndex)
    {
        pos = _pos;
        curWaypointIndex = _curWaypointIndex;
    }
}

