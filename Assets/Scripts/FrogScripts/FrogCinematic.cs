using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCinematic : MonoBehaviour {

	int startingFrogs = 90;
	Vector2 spawnBoundry = new Vector2(8,5);
	float spawnOffsetRange = 2.5f;

	bool updateGroundTransformation = false;
	bool groundIsMoistening = false;
	float transformationCounter = 0;
	float timeToTransformation = 5f;


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

	public void StartAridCinematic()
	{
		updateGroundTransformation = true;
		groundIsMoistening = false;
		transformationCounter = 0;
		foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().frogParent)
			Destroy (t.gameObject);
		foreach (Transform t in GameObject.FindObjectOfType<FrogWS>().tadpoleParent)
			Destroy (t.gameObject);
	}

	public void StartWetlandCinematic() //also sets up level
	{
		updateGroundTransformation = true;
		groundIsMoistening = true;
		transformationCounter = 0;
		for (int i = 0; i <= startingFrogs; i++) {
			GameObject newFrog = Instantiate(Resources.Load("Prefabs/Frog")) as GameObject;
			newFrog.transform.SetParent(GameObject.FindObjectOfType<FrogWS>().frogParent);
			newFrog.transform.position = GetRandomSpawnLoc();
			newFrog.GetComponent<Frog>().CreateFrog(Random.Range(0f,1f) > .5f,false);
			newFrog.GetComponent<Frog> ().outtaBounds = true;
		}
	}

	public void Update()
	{
		if (!updateGroundTransformation)
			return;
		transformationCounter += Time.deltaTime;
		if (groundIsMoistening) {
			Color moistColor = wetBg.color;
			moistColor.a = (transformationCounter / timeToTransformation);
			wetBg.color = moistColor;
		} else {
			Color moistColor = wetBg.color;
			moistColor.a = 1 - (transformationCounter / timeToTransformation);
			wetBg.color = moistColor;
		}
	}



	private Vector2 GetRandomSpawnLoc()
	{
		float spawnOffset = Random.Range (0, spawnOffsetRange);
		float spawnX;// = spawnBoundry.x * ((Random.Range(0f,1f) > .5f)?1:-1);
		float spawnY;// = spawnBoundry.y * ((Random.Range(0f,1f) > .5f)?1:-1);
		if (MathHelper.Fiftyfifty ()) {
			spawnX  = Random.Range(-spawnBoundry.x,spawnBoundry.x);
			spawnY  = spawnBoundry.y * ((MathHelper.Fiftyfifty ())?1:-1);
		} else {
			spawnX = spawnBoundry.x * ((MathHelper.Fiftyfifty ())?1:-1);
			spawnY = Random.Range(-spawnBoundry.y,spawnBoundry.y);
		}
		Vector2 spawnLoc = new Vector2 (spawnX + Mathf.Sign (spawnX) * spawnOffset, spawnY + Mathf.Sign (spawnY) * spawnOffset);
		return spawnLoc;
	}
}
