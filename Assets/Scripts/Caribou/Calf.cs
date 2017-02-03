using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calf : MonoBehaviour {

    public bool isPlayerCalf = false;
    float baseSpeed;
    float sprintCoolDown = 0f;
    public float maxSprintCooldown = 10;
    public float sprintCoolDownSpeed = 1f;
    public float sprintTriggerDistance = 30f;
    public float maxSpeedBoost = 200f;
    float currentSpeedBoost = 0f;
    public float speedBoostDecayRate = 8f;

    // Use this for initialization
    void Start () {
        baseSpeed = gameObject.GetComponent<FlockingAI>().speedRange.y;
    }
	
	// Update is called once per frame
	void Update () {

        if (sprintCoolDown == 0)
        {
            if (!isPlayerCalf)
            {
                List<Transform> wolfTrans = GameObject.FindObjectOfType<PredatorManager>().predatorTransforms;
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

        gameObject.GetComponent<FlockingAI>().speed = baseSpeed + currentSpeedBoost;

        if (gameObject.GetComponent<FlockingAI>().test)
        {
            Debug.Log(gameObject.GetComponent<FlockingAI>().speed + "=" + baseSpeed + "+" + currentSpeedBoost);
        }
    }
}
