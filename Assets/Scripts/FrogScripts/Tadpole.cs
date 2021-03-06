﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Tadpole : MonoBehaviour {

    Frog.FrogInfo frogInfo;
    float timeRemainingToGrow;
    Vector2 tadpoleHatchRange = new Vector2(15,30);
    Puddle puddle = null;
    bool tadpoleInitialized = false;

    // Use this for initialization
    public void Start()
    {
        if (!tadpoleInitialized) //cinematic tadpoles dont get birthed
            BirthTadpole(new Frog.FrogInfo(0, false, MathHelper.Fiftyfifty()));
    }


    public void BirthTadpole(Frog.FrogInfo _frogInfo)
    {
        tadpoleInitialized = true;
        frogInfo = new Frog.FrogInfo(_frogInfo);
        timeRemainingToGrow = Random.Range(tadpoleHatchRange.x, tadpoleHatchRange.y);
        float randAngle = Random.Range(0, 360);
        Vector2 randStartForce = MathHelper.DegreeToVector2(randAngle) * 5;
        GetComponent<Rigidbody2D>().AddForce(randStartForce);
        FrogGV.masterTadpoleList.Add(this);
        puddle = FrogGV.frogWS.puddle;
    }
	
	// Update is called once per frame
	public void UpdateTadpole (float dt)
    {
        timeRemainingToGrow -= dt;

        if (timeRemainingToGrow <= 0)
        {
            if (FrogGV.frogWS.frogParent.childCount < FrogGF.maxFrogCount)
            {
                GameObject newFrog = Instantiate(Resources.Load("Prefabs/Frog")) as GameObject;
                newFrog.transform.SetParent(FrogGV.frogWS.frogParent);
                newFrog.transform.position = transform.position;
                newFrog.GetComponent<Frog>().InitializeFrog(frogInfo);
            }
            KillTadpole();
        }

	}

    public void KillTadpole()
    {
        FrogGV.ModTadpole(this, false);
        Destroy(this.gameObject);        
    }
}
