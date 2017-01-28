using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
	public Vector3 desiredPos;
	public bool isMoving;

	//FISH GV
	public float rotateSpeed = 360; //45 degrees per Sec
	private float rotateSpeedRadians {get{return rotateSpeed * Mathf.PI / 180;} set{rotateSpeedRadians = value;}}
	public float moveSpeed = 3;


	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		FishUpdate ();
	}

	public void FishUpdate(){
		
		if (isMoving) {
			Vector3 temp = desiredPos - transform.position;
			if (Mathf.Abs (temp.magnitude) > 0.4f) {
				RotateToDesPoint ();
				MoveForward (desiredPos);
			} else {
				transform.position = desiredPos;
				isMoving = false;
			}
		} else {
			//MoveTo (transform.position + 2 * transform.forward);
		}
	}

	private void RotateToDesPoint(){
		transform.eulerAngles = clampXY(Vector3.RotateTowards (transform.eulerAngles, desiredPos, rotateSpeedRadians * Time.deltaTime, rotateSpeedRadians));

	}

	private void MoveForward(Vector3 des){
		transform.position = clampZ(Vector3.MoveTowards (transform.position,des, moveSpeed * Time.deltaTime));
	}

	public void MoveTo(Vector3 newDes){
		desiredPos = newDes;
		isMoving = true;
	}

	public bool isClose(float desired, float current){
		return false;
	}
		
	private Vector3 clampZ(Vector3 toChange){
		return new Vector3 (toChange.x, toChange.y, 0);
	}
		
	private Vector3 clampXY(Vector3 toChange){
		return new Vector3 (0, 0, toChange.z);
	}
}
