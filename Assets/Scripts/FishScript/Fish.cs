using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
	public Vector3 desiredPos;
	public bool isMoving;
	public Rigidbody2D body;
	public bool isRotateLeft = false;

	//FISH GV
	public float rotateSpeed = 0.5f;
	public float moveSpeed = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		FishUpdate ();
	}

	public void FishUpdate(){
		if (isMoving) {

			float angToGoal = MathHelper.AngleBetweenPoints((Vector2) this.transform.position, (Vector2) desiredPos) - 90;
			angToGoal = (angToGoal < -180) ? angToGoal + 360 : angToGoal;
			Debug.Log ("angToGoal " + angToGoal);
			float currentz = transform.eulerAngles.z;
			currentz = (currentz > 180) ? currentz - 360 : currentz;
			Debug.Log ("currentz " + currentz);
			//isClose ();

			if (currentz + 10 > || angToGoal < currentz - 10) {
				if (angToGoal > 0)
				transform.eulerAngles = new Vector3 (0, 0, currentz + rotateSpeed);
				else
					transform.eulerAngles = new Vector3 (0, 0, currentz - rotateSpeed);
			} else {
				transform.eulerAngles = new Vector3 (0, 0, angToGoal);
			}




			MoveForward (moveSpeed);
			
		
		} else {
			MoveForward (moveSpeed/5);
		}
	}

	private void MoveForward(float _speed){
		//body.AddForce (transform.up * _speed * Time.deltaTime);
	}

	public void MoveTo(Vector2 position){
		desiredPos = new Vector3 (position.x, position.y, 0);
		isMoving = true;
	}

	public bool isClose(float desired, float current){

	}
}
