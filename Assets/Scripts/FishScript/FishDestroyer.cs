using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDestroyer : MonoBehaviour {
	public Transform targetTransfrom;
	public float disntanceToDestroy = 20;
	public float prcntChance = 20; // per seond

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Random.Range (0, 100) < (prcntChance * Time.deltaTime) && targetTransfrom != null) {
			tryToDestroy ();
		}
	}

	public void tryToDestroy(){
		foreach (Fish t in GetComponentsInChildren<Fish>()) {
			if(Vector3.Distance(targetTransfrom.position,t.transform.position) > disntanceToDestroy && !t.isBeingEaten){
				if (Random.Range(0,1) > 0.8f){
					GameObject fishLoader = Instantiate (Resources.Load ("Prefabs/fishLoader")) as GameObject;
					fishLoader.transform.position = t.transform.position;
					fishLoader.GetComponent<FishLoader> ().salmonManager = this.gameObject;
				}
				Destroy (t.gameObject);
			}
		}
	}
}
