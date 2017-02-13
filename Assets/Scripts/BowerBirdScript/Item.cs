using System.Collections;
using UnityEngine; using LoLSDK;

public class Item : MonoBehaviour {
	public BowerGV.Items itemType;
	public BowerGV.Color itemColor;
	public bool overrrideColor = true;
	public bool overrideType = true;

	public Bower isIn;

	//being carried
	public BowerBird carrier;
	public bool isCarried;

	void Start(){
		if(overrideType)
			itemType = (BowerGV.Items) Random.Range ((int)0, 3);
		//Debug.Log (itemType.ToString ());
		switch (itemType){
		case BowerGV.Items.Berry:
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/BowerSprites/Berry");
			break;
		case BowerGV.Items.Feather:
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/BowerSprites/Feather");
			break;
		case BowerGV.Items.Flower:
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/BowerSprites/Flower");
			break;
		case BowerGV.Items.PreciousStone:
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/BowerSprites/mineral");
			break;
		case BowerGV.Items.Shell: //shouldn't be called cuz its out of range
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/BowerSprites/Shell");
			break;
		default:
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/BowerSprites/Berry");
			break;
		}

		if(overrrideColor)
			itemColor = (BowerGV.Color) Random.Range ((int) 0,4);
		
		//Debug.Log (itemColor.ToString ());
		switch (itemColor) {
		case BowerGV.Color.Blue:
                GetComponent<SpriteRenderer>().color = new Color(102f / 255, 102f / 255, 253f / 255, 1); //lighter blue
			break;
		case BowerGV.Color.Red:
			GetComponent<SpriteRenderer> ().color = Color.red;
			break;
		case BowerGV.Color.White:
			GetComponent<SpriteRenderer> ().color = Color.white;
			break;
		case BowerGV.Color.Yellow:
			GetComponent<SpriteRenderer> ().color = Color.yellow;
			return;
		default:
			GetComponent<SpriteRenderer> ().color = Color.black;
			break;
		}
		GetComponent<Transform> ().Rotate (new Vector3 (0, 0, Random.Range (0, 360)));
	}

	void Update(){
		if(isCarried)
			UpdatePos ();
	}
		
	public bool getPickedUp(BowerBird toBeCarrier){
		if (!isCarried) {
			if (isIn != null) {
				if (!isIn.isOwnerNear ()) {
					carrier = toBeCarrier;
					isCarried = true;
					return true;
					//theft has occured
				} else {
					return false;
				}
			} else {
				//not owned by anyone
				carrier = toBeCarrier;
				isCarried = true;
				return true;
			}
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

	public void isOwned(Bower owner){
		isIn = owner;
	}
}
