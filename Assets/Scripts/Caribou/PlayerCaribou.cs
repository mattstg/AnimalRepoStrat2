using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCaribou : MonoBehaviour {

    public GameObject PlayerCalf;
    Vector3 targetPos = new Vector3();
    public float speed = 5f;


    public void MousePressed(Vector3 loc)
    {
        targetPos = loc;
    }

    // Use this for initialization
    void Start () {
        GameObject.FindObjectOfType<FlockManager>().flock.Add(this.gameObject);
        targetPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        float step = speed * Time.deltaTime;


        float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, targetPos);
        if (Vector3.Distance(this.transform.position, targetPos) > .2f)
        {
            transform.eulerAngles = new Vector3(0, 0, angToGoal);
        }


        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }
}
