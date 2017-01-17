using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogFish : MonoBehaviour {

    public float maxcooldown = 3;
    public float wanderCooldown;
    bool onCooldown = false;

    public float chanceOfFullFromFrog = .33f;
    public float chanceOfFullFromTadpole = .20f;

    List<Transform> targets = new List<Transform>();
    float timeHuntingTarget = 0;

    public Transform currentTarget;
    bool targetIsFrog;
    float fishspeed = 3;
	// Update is called once per frame
	void Update () {

        if (onCooldown)
        {
            wanderCooldown -= Time.deltaTime;
            if (wanderCooldown <= 0)
            {
                onCooldown = false;
                wanderCooldown = maxcooldown;
            }
        }
        else
        {
            if(currentTarget)
            {
                if(currentTarget.GetComponent<Frog>() && currentTarget.GetComponent<Frog>().inPuddle)
                {
                    LostCurrentTarget();
                }
                else
                {
                    //transform.position = transform.position + 
                }
            }
                
        }	
	}


    protected void MoveTowardsGoal(Vector2 goalLoc)
    {
        float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, goalLoc);
        transform.eulerAngles = new Vector3(0, 0, angToGoal - 90);
        Vector2 goalOffset = MathHelper.DegreeToVector2(angToGoal);
        Vector2 diff = goalLoc - MathHelper.V3toV2(this.transform.position);
        float magnitude = diff.magnitude;
        magnitude = (magnitude > fishspeed) ? fishspeed : magnitude;
        Vector2 goalPos;
        transform.position = MathHelper.V3toV2(transform.position) + goalOffset * magnitude * Time.deltaTime;
    }

    private void LostCurrentTarget()
    {
        currentTarget = null;
        if(targets.Count > 0)
        {
            currentTarget = targets[Random.Range(0, targets.Count - 1)];
        }
    }

    public void OnTriggerEnter2D(Collider2D coli)
    {
        if(coli.GetComponent<Frog>() || coli.GetComponent<Tadpole>())
            targets.Add(coli.transform);
    }

    public void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.GetComponent<Frog>() || coli.GetComponent<Tadpole>())
            targets.Remove(coli.transform);
        if (coli.transform == currentTarget)
            LostCurrentTarget();
    }
}
