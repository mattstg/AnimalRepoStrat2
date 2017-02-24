using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class WaterfallZone : MonoBehaviour {
	public GameObject jumpToPoint;
    public bool savesCheckpoint = true;
    public bool circuitConnectingWaterfall = false;

	void OnTriggerEnter2D(Collider2D coli){
		Fish currentFish = coli.GetComponent<Fish> ();
		if (currentFish != null) {
			currentFish.jumpTo (jumpToPoint.transform.position);
            if(savesCheckpoint && coli.GetComponent<PlayerFish>())
			    GameObject.FindObjectOfType<FishGF> ().ReachedCheckpoint (jumpToPoint.transform.position);

        }
	}


}
