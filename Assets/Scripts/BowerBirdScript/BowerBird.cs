using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class BowerBird : MonoBehaviour {
	public bool isPlayer = false;
	public Animator anim;
	public bool canGetBored = true;
	public Bower bower;
	public Rigidbody2D body;
	public Vector2 desiredPos;
	public bool isMoving = false;
	public Vector2 startScale; //DONT FUCKIN CHANGE THIS EVER -- wonder what the story behind that is
		
	public float altitude = 0;
	public float idleTime = 0;

	private float rotateSpeed = 90;
	private Vector3 lastDir = Vector3.forward;

	//for holding items
	public bool autoPickup = false;
	public Item holding;
	public bool isHolding = false;
	public GameObject holdingLoc; // position to place object when we are holding it

	public bool returningHome = false;

	// Use this for initialization
	void Start () {
		ParentStart ();
	}

	public void ParentStart(){
		body = GetComponent<Rigidbody2D>();
		startScale = transform.localScale;
		anim = GetComponentInChildren<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		ParentUpdate ();
	}

	/*
	public void PlayerUpdate(){
		ScaleFromAltitude ();
		if (isMoving) {
			if (Vector2.Distance (desiredPos, transform.position) > BowerGV.distanceFromDesiredPos) {
				Vector2 moveDir = desiredPos - (Vector2)transform.position;
				Move (moveDir.normalized);
			} else {
				isMoving = false;
				body.velocity = Vector2.zero;
			}
		} else {
			altitude = Mathf.Clamp (altitude - 2 * BowerGV.altitudePerSecond * Time.deltaTime, 0, 5);
		}
	} */

	public bool isMovingWrongWay(){

		Vector2 desiredDir = desiredPos - (Vector2)transform.position;
		desiredDir = desiredDir.normalized;
		Vector2 currentDir = body.velocity.normalized;

		//float angle = handleAngle (Vector2.Angle (currentDir, desiredDir));

		/*
		if (angle < 45 && angle > -45) {
			return false;
		}
		 */
		if (currentDir == desiredDir) {
			return false;
		}

		return true;
	}



	public Vector2 MRot(float deg, Vector2 inV){
		float nx = inV.x * Mathf.Cos (deg * Mathf.Deg2Rad) - inV.y * Mathf.Sin (deg * Mathf.Deg2Rad);
		float ny = inV.x * Mathf.Sin (deg * Mathf.Deg2Rad) + inV.y * Mathf.Cos (deg * Mathf.Deg2Rad);
		return new Vector2 (nx,ny);
	}

	public float handleAngle(float input){
		if (input >= 180) {
			return input - 360;
		}

		if(input <= -180){
			return input + 360;
		}
		return input;
	}

	public void ParentUpdate(){
		if (body.velocity == Vector2.zero) {
			RotateToDesPoint (lastDir);
		} else {
			lastDir = (Vector3)body.velocity.normalized + transform.position;
			RotateToDesPoint (lastDir);
		}

		ScaleFromAltitude ();
		if (isMoving) {
			idleTime = 0;
			if (Vector2.Distance (desiredPos, transform.position) > BowerGV.distanceFromDesiredPos) {
				Vector2 moveDir = desiredPos - (Vector2)transform.position;
				if (isMovingWrongWay()) {
					/*
					float angle = handleAngle (Vector2.Angle (body.velocity, moveDir));
					if(angle > 0)
						body.velocity = MRot (120 * Time.deltaTime , body.velocity);
					else
						body.velocity = MRot (-120 * Time.deltaTime , body.velocity);
					
					body.velocity = new Vector2 (body.velocity.x * 0.97f, body.velocity.y * 0.97f);
					*/
					float mag = body.velocity.magnitude;
					body.velocity = moveDir.normalized * mag;
				} 
				Move (moveDir.normalized);
			} else {
				if (returningHome) {
					DropItem ();
					returningHome = false;
					isMoving = false;
					body.velocity = Vector2.zero;
				} else {
					isMoving = false;
					body.velocity = Vector2.zero;
				}
			}
		} else {
			altitude = Mathf.Clamp (altitude - BowerGV.altitudePerSecond * Time.deltaTime, 0, 5);
			IdleBoredom (idleTime);
		}
		updateAnimator ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Item")) {
			/*
			Item colItem = other.GetComponent<Item>();
			if (!isHolding && autoPickup && altitude == 0) {
				colItem.getPickedUp (this);
				isHolding = true;
				holding = colItem;
			} */
		} else if (other.CompareTag ("BowerBird")) {
			//BowerBird colBird = other.GetComponent<BowerBird>();
		} else if (other.CompareTag ("Bower")) {
			//Bower colBower = other.GetComponent<Bower> ();
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.CompareTag ("Item")) {
			if (!isHolding && autoPickup && altitude == 0) {
				Item colItem = other.GetComponent<Item>();
				if (colItem.getPickedUp (this)) {
					isHolding = true;
					holding = colItem;
				} else {
					//cannot steal
					if(canGetBored)
						SeekRandomItem();
				}
			}
		} /*else if (other.CompareTag ("BowerBird")) {
			//BowerBird colBird = other.GetComponent<BowerBird>();
		} else if (other.CompareTag ("Bower")) {
			//Bower colBower = other.GetComponent<Bower> ();
		}*/
	}
		
	public void updateAnimator(){
		if (anim != null) {
			if (altitude > 0.02) {
				anim.SetBool ("isFlying", true);
			} else {
				anim.SetBool ("isFlying", false);
			}
		}
	}

	public void DropItem(){
		isHolding = false;
		holding.getDropped (new Vector2(holdingLoc.transform.position.x, holdingLoc.transform.position.y - 0.2f));
		holding = null;
		autoPickup = false;
	}

	public void Move(Vector2 dir){
		if(body.velocity.magnitude < 10.0)
			body.AddForce (dir * BowerGV.BOWERSPEED * Time.deltaTime, ForceMode2D.Impulse);
		altitude = Mathf.Clamp (altitude + BowerGV.altitudePerSecond * Time.deltaTime, 0, 5);
	}

	public void MoveTo(Vector2 position){
		desiredPos = position;
		isMoving = true;
	}
		
	public void ScaleFromAltitude(){
		transform.localScale = startScale * Mathf.Lerp (1, BowerGV.MaxScaleGrowthFromAlt, altitude / 5);
	}

	public void SeekRandomItem(){
		desiredPos = (Vector2)GameObject.FindObjectOfType<ItemManager> ().getRandomItem ().transform.position;
		isMoving = true;
	}

	public void returnHome(){
		returningHome = true;
		desiredPos = (Vector2)bower.transform.position + new Vector2(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f));
		isMoving = true;
	}

	public void IdleBoredom(float timeIdle){
		if (canGetBored) {
			if (!isHolding) {
				if (timeIdle > 5) {
					SeekRandomItem ();
					idleTime = 0;
					autoPickup = true;
				} else {
					idleTime += Time.deltaTime;
				}
			} else {
				returnHome ();
			}
		}
	}

	private void RotateToDesPoint(Vector3 _desiredPos){
		Vector3 dirVector = _desiredPos - transform.position;
		float angle = Mathf.Atan2 (dirVector.y, dirVector.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle + 270,Vector3.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, q, rotateSpeed * Time.deltaTime);
	}
}