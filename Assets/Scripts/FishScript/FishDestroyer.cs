using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDestroyer : MonoBehaviour {
	public Transform targetTransfrom;
	public float disntanceToDestroy = 25;
	public float prcntChance = 2.5f; // per seond

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
			if(Vector3.Distance(targetTransfrom.position,t.transform.position) > disntanceToDestroy){
				Destroy (t.gameObject);
			}
		}
	}
}
