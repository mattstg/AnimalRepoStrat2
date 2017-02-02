using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterfallZone : MonoBehaviour {
	public GameObject jumpToPoint;

	void OnTriggerEnter2D(Collider2D coli){
		Fish currentFish = coli.GetComponent<Fish> ();
		if (currentFish != null) {
			currentFish.jumpTo (jumpToPoint.transform.position);
		}
	}
}
