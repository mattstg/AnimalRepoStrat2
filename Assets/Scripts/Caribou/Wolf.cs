using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Wolf : MonoBehaviour {

    float baseSpeed;
    float sprintCoolDown = 0f;
    public float maxSprintCooldown = 10;
    public float sprintCoolDownSpeed = 1f;
    public float sprintTriggerDistance = 30f;
    public float maxSpeedBoost = 200f;
    float currentSpeedBoost = 0f;
    public float speedBoostDecayRate = 8f;

    CalfManager calfManager;
    FlockingAI flockAi;

    // Use this for initialization
    void Start () {
        flockAi = gameObject.GetComponent<FlockingAI>();
        baseSpeed = flockAi.speedRange.y;
        calfManager = GameObject.FindObjectOfType<CalfManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	if(sprintCoolDown == 0)
        {
            List<Transform> calfTrans = calfManager.calfTransforms;
            float closestCalfDistance = 1000;
            if (calfTrans.Count != 0)
            {
                foreach (Transform _calfTrans in calfTrans)
                {
                    Vector2 _calfPos = _calfTrans.position;
                    float distanceFromCalf = Vector2.Distance(_calfPos, transform.position);
                    if (distanceFromCalf < closestCalfDistance)
                    {
                        closestCalfDistance = distanceFromCalf;
                        
                    }
                }

                if (closestCalfDistance <= sprintTriggerDistance)
                {
                    currentSpeedBoost = maxSpeedBoost;
                    sprintCoolDown = maxSprintCooldown;
                }
            }
        }
        if (sprintCoolDown > 0f)
        {
            sprintCoolDown -= Time.deltaTime * sprintCoolDownSpeed;
        }
        else if (sprintCoolDown <= 0f)
        {
            sprintCoolDown = 0f;
        }

        if (currentSpeedBoost > 0)
        {
            currentSpeedBoost -= Time.deltaTime * speedBoostDecayRate;
        }

        else if (currentSpeedBoost <= 0)
            currentSpeedBoost = 0;

        flockAi.speed = baseSpeed + currentSpeedBoost;

        /*if(flockAi.test)
        {
            //Debug.Log(gameObject.GetComponent<FlockingAI>().speed + "=" + baseSpeed + "+" + currentSpeedBoost);
            //Debug.Log("Update Time: " + Time.deltaTime);
        }*/
    }
}
