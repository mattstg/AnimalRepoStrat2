using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Calf : MonoBehaviour {

    public bool isPlayerCalf = false;
    float baseSpeed;
    float sprintCoolDown = 0f;
    public float maxSprintCooldown = 10;
    public float sprintCoolDownSpeed = 1f;
    public float sprintTriggerDistance = 30f;
    public float maxSpeedBoost = 200f;
    public float currentSpeedBoost = 0f;
    public float speedBoostDecayRate = 8f;

    PredatorManager predatorManager;
    FlockingAI flockAi;

    // Use this for initialization
    void Start () {
        flockAi = gameObject.GetComponent<FlockingAI>();
        baseSpeed = flockAi.speedRange.y;
        predatorManager = GameObject.FindObjectOfType<PredatorManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isPlayerCalf)
        {
            if (sprintCoolDown == 0)
            {
                List<Transform> wolfTrans = predatorManager.predatorTransforms;
                float closestWolfDistance = 1000;
                if (wolfTrans.Count != 0)
                {
                    foreach (Transform _wolfTrans in wolfTrans)
                    {
                        Vector2 _wolfPos = _wolfTrans.position;
                        float distanceFromWolf = Vector2.Distance(_wolfPos, transform.position);
                        if (distanceFromWolf < closestWolfDistance)
                        {
                            closestWolfDistance = distanceFromWolf;

                        }
                    }

                    if (closestWolfDistance <= sprintTriggerDistance)
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
        }
        


        if (currentSpeedBoost > 0)
        {
            currentSpeedBoost -= Time.deltaTime * speedBoostDecayRate;
        }

        else if (currentSpeedBoost <= 0)
            currentSpeedBoost = 0;

        flockAi.speed = baseSpeed + currentSpeedBoost;

        if (flockAi.test)
        {
            Debug.Log(flockAi.speed + "=" + baseSpeed + "+" + currentSpeedBoost);
        }
    }
}
