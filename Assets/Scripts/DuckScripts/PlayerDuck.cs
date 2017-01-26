using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuck : MonoBehaviour {
    public float speed = 5;
    Vector3 TargetPosition;
    

    public void MousePressed(Vector3 loc)
    {
        Vector3 goalVec = loc;
        float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, goalVec);
        transform.eulerAngles = new Vector3(0, 0, angToGoal);
        
    }

    public Vector3 GetTargetPos()
    {
        Vector2 _clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3( _clickedPos.x, _clickedPos.y, 1 );
        return (targetPos);
    }

	// Use this for initialization
	void Start () {
        //GameObject.FindObjectOfType<FlockManager>().flock.Add(this.gameObject);
        TargetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        TargetPosition = GetTargetPos();
        float step = speed * Time.deltaTime;

        float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, TargetPosition);
        transform.eulerAngles = new Vector3(0, 0, angToGoal);

        transform.position = Vector3.MoveTowards(transform.position, TargetPosition , step);
    }
}
