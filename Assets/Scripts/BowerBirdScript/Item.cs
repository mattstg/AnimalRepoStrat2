using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour {
	public BowerGV.Items itemType;
	public BowerGV.Color itemColor;

	//being carried
	public BowerBird carrier;
	public bool isCarried;

	void Start(){

	}

	void Update(){
		if(isCarried)
			UpdatePos ();
	}
		
	public bool getPickedUp(BowerBird toBeCarrier){
		if (!isCarried) {
			carrier = toBeCarrier;
			isCarried = true;
			return true;
		} else {
			return false;
		}
	}

	public void getDropped(Vector2 dropLoc){
		isCarried = false;
		carrier = null;
		gameObject.transform.position = dropLoc;
	}

	public void UpdatePos(){
		gameObject.transform.position = carrier.holdingLoc.transform.position;
	}
}
