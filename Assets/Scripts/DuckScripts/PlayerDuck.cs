using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuck : MonoBehaviour {
    public float speed = 5f;
    Vector3 TargetPosition;
    public int quackRadius = 12;
    float quackCoolDown = 0f;
    public int maxQuackCooldown = 10;
    public float quackCoolDownSpeed = 1f;

    public void MousePressed(Vector3 loc)
    {
        Vector3 goalVec = loc;
        float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, goalVec);
        transform.eulerAngles = new Vector3(0, 0, angToGoal);
        
    }

    public void Quack()
    {
        if (quackCoolDown == 0)
        {
            foreach (GameObject duckling in GameObject.FindObjectOfType<DucklingManager>().Ducklings)
            {
                if (!duckling.GetComponent<Duckling>().isDead)
                {
                    float dist = Vector3.Distance(transform.position, duckling.transform.position);
                    if (dist <= quackRadius)
                    {
                        duckling.GetComponent<Duckling>().quackStrength = duckling.GetComponent<Duckling>().maxQuackStrength;
                    }
                }
            }
            quackCoolDown = maxQuackCooldown;
            Debug.Log("quack!");
        }
        else
            Debug.Log("'Quack' is on cool-down!");
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

        if(quackCoolDown > 0f)
        {
            quackCoolDown -= Time.deltaTime * quackCoolDownSpeed;
        }
        else if(quackCoolDown <= 0f)
        {
            quackCoolDown = 0f;
        }
    }
}
