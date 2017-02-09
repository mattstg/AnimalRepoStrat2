using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Fish : MonoBehaviour {
	private float speedBoostTime = 1;

	//AI manager
	private Vector3 vectorAI;
	public bool followAi = true;

	public Vector3 desiredPos;
	public bool isMoving;

	private bool isJumping = false;
	private Vector3 jumpPoint;
	private float jumpSpeed = 10;

	//FISH GV
	private float rotateSpeed = 10; //45 degrees per Sec
	private float speedBoost = 1f; // moveSpeed * speedBoost is max total speed during boost
	private float timeSinceLastClick = 0f;

	//Move Speed and Boost Upon Move
	private float realMov = 1;
	protected float moveSpeed {
		set { realMov = value; } 
		get {
			if (timeSinceLastClick < speedBoostTime) {
				float percentCompletion = 1 - timeSinceLastClick / speedBoostTime;
				return realMov + realMov * speedBoost * percentCompletion;
			}
			else
				return realMov;
		}
	}


	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		AIUpdate ();
		FishUpdate ();
    }

	public void AIUpdate(){
		MoveTo (transform.position + vectorAI);
	}

	public void VectorAISet(Vector3 input){
		vectorAI = input;
		vectorAI = vectorAI.normalized;
		timeSinceLastClick = 0;
	}

	public void FishUpdate(){
		if (!isJumping) {
			timeSinceLastClick += Time.deltaTime;
			if (isMoving) {
				Vector3 temp;
				if(followAi)
					temp = transform.position + vectorAI;
					else
					temp = desiredPos - transform.position;
				if (Mathf.Abs (temp.magnitude) > 0.4f) {
					RotateToDesPoint (desiredPos);
					MoveForward (desiredPos);
				} else {
					//transform.position = desiredPos;
					isMoving = false;
				}
			} else {
				//RotateToDesPoint (transform.position + transform.up);
				MoveTo (transform.position + transform.up);
			}
		} else {
			transform.position = clampZ(Vector3.MoveTowards (transform.position,jumpPoint, jumpSpeed * Time.deltaTime));
			Vector3 temp = jumpPoint - transform.position;
			if (Mathf.Abs (temp.magnitude) < 0.4f) {
				isJumping = false;
				GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
				desiredPos = transform.position + Vector3.up;
			}
		}
	}

	private void RotateToDesPoint(Vector3 _desiredPos){
		Vector3 dirVector = _desiredPos - transform.position;
		float angle = Mathf.Atan2 (dirVector.y, dirVector.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle + 270,Vector3.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, q, rotateSpeed * Time.deltaTime);
	}

	private void MoveForward(Vector3 des){
		transform.position = clampZ(Vector3.MoveTowards (transform.position,des, moveSpeed * Time.deltaTime));
	}

	public void MoveTo(Vector3 newDes){
		desiredPos = newDes;
		isMoving = true;
    }

	public void PlayerMoveTo(Vector3 newDes){
		timeSinceLastClick = 0;
		MoveTo (newDes);
	}
		
	private Vector3 clampZ(Vector3 toChange){
		return new Vector3 (toChange.x, toChange.y, 0);
	}
		
	private Vector3 clampXY(Vector3 toChange){
		return new Vector3 (0, 0, toChange.z);
	}

	public void jumpTo(Vector3 landPoint){
		isJumping = true;
		Vector3 temp = new Vector3 (transform.position.x, landPoint.y);
		jumpPoint = temp;
		GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;
	}

    public void Dies()
    {
        GetComponent<FlockingAI>().Dies();
        enabled = false;
    }
}
