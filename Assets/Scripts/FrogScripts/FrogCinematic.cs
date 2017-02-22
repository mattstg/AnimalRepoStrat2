using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class FrogCinematic : MonoBehaviour {

    enum FrogCinematicStage { None ,BecomingWet, BecomingArid }

	int startingFrogs = 40;
	Vector2 spawnBoundry = new Vector2(5,4);
	//float spawnOffsetRange = 2.5f;

	bool updateGroundTransformation = false;
	bool groundIsMoistening = false;
	float transformationCounter = 0;
	float timeToTransformation = 5f;
    FrogCinematicStage currentStage = FrogCinematicStage.None;
    bool evacuateAtEndOfCinematic = false;

	List<Puddle> puddles = new List<Puddle>();
	Transform puddleParent;
	SpriteRenderer aridBg;
	SpriteRenderer wetBg;

	public void Start()
	{
		puddleParent = GameObject.FindObjectOfType<FrogWS> ().puddleParent;
		aridBg = GameObject.FindObjectOfType<FrogWS> ().aridBackground;
		wetBg = GameObject.FindObjectOfType<FrogWS> ().moistBackground;
		foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().puddleParent) {
			puddles.Add (t.GetComponent<Puddle> ());
		}
	}

	public void StartFirstAridCinematic()
	{
		transformationCounter = 0;
        timeToTransformation = 5;
        currentStage = FrogCinematicStage.BecomingArid;
        foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().frogParent)
			Destroy (t.gameObject);
		foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().tadpoleParent)
			Destroy (t.gameObject);
        foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().puddleParent)
            t.GetComponent<Puddle>().activeTadpoles = new List<Tadpole>();
    }

    public void StartSecondAridCinematic()
    {
        transformationCounter = 0;
        timeToTransformation = 20;
        currentStage = FrogCinematicStage.BecomingArid;
        evacuateAtEndOfCinematic = true;        
    }

    public void StartWetlandCinematic() //also sets up level
	{
        transformationCounter = 0;
        timeToTransformation = 10;
        currentStage = FrogCinematicStage.BecomingWet;
        for (int i = 0; i <= startingFrogs; i++) {
			GameObject newFrog = Instantiate(Resources.Load("Prefabs/Frog")) as GameObject;
			newFrog.transform.SetParent(GameObject.FindObjectOfType<FrogWS>().frogParent);
			newFrog.transform.position = GetRandomSpawnLoc();
            Frog.FrogInfo fi = new Frog.FrogInfo(0, false, Random.Range(0f, 1f) > .5f);
			newFrog.GetComponent<Frog>().CreateFrog(fi);
			newFrog.GetComponent<Frog> ().outtaBounds = true;
		}
        GameObject.FindObjectOfType<SnakeManager>().SetupGame();
	}

	public void Update()
	{
        switch(currentStage)
        {
            case FrogCinematicStage.None:
                break;
            case FrogCinematicStage.BecomingArid:
                UpdateBecomingArid();
                break;
            case FrogCinematicStage.BecomingWet:
                UpdateBecomingWet();
                break;
        }
	}

    private void UpdateBecomingArid()
    {
        transformationCounter += Time.deltaTime;
        Color moistColor = wetBg.color;
        moistColor.a = 1 - (transformationCounter / timeToTransformation);
        wetBg.color = moistColor;
        foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().puddleParent)
        {
            t.localScale = Vector2.Lerp(t.GetComponent<Puddle>().originalSize, new Vector2(.01f, .01f), (transformationCounter / timeToTransformation));
            t.GetComponent<Puddle>().carryingCapacity = (int)(( 1- (transformationCounter / timeToTransformation)) * t.GetComponent<Puddle>().originalCarryingCapacity);
        }

        if (evacuateAtEndOfCinematic && transformationCounter >= timeToTransformation)
            foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().frogParent)
                t.GetComponent<Frog>().leaveMap = true;
    }

    private void UpdateBecomingWet()
    {
        transformationCounter += Time.deltaTime;
        Color moistColor = wetBg.color;
        moistColor.a = (transformationCounter / timeToTransformation);
        wetBg.color = moistColor;
        foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().puddleParent)
        {
            t.localScale = Vector2.Lerp( new Vector2(.01f, .01f), t.GetComponent<Puddle>().originalSize, (transformationCounter / timeToTransformation));
            t.GetComponent<Puddle>().carryingCapacity = (int)((transformationCounter / timeToTransformation) * t.GetComponent<Puddle>().originalCarryingCapacity);
        }
    }




	private Vector2 GetRandomSpawnLoc()
	{
		//float spawnOffset = Random.Range (0, spawnOffsetRange);
		float spawnX;// = spawnBoundry.x * ((Random.Range(0f,1f) > .5f)?1:-1);
		float spawnY;// = spawnBoundry.y * ((Random.Range(0f,1f) > .5f)?1:-1);
		if (MathHelper.Fiftyfifty ()) {
			spawnX  = Random.Range(-spawnBoundry.x,spawnBoundry.x);
			spawnY  = spawnBoundry.y * ((MathHelper.Fiftyfifty ())?1:-1);
		} else {
			spawnX = spawnBoundry.x * ((MathHelper.Fiftyfifty ())?1:-1);
			spawnY = Random.Range(-spawnBoundry.y,spawnBoundry.y);
		}
        //Vector2 spawnLoc = new Vector2 (spawnX + Mathf.Sign (spawnX) * spawnOffset, spawnY + Mathf.Sign (spawnY) * spawnOffset);
        Vector2 spawnLoc = new Vector2(spawnX, spawnY);

        return spawnLoc;
	}
}
