﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckEndZone : MonoBehaviour {

    float timeDuckInEndZone = 0;
    float timeDuckInEndZoneToEndGame = 6;
    float ducklingsSaved = 0;
    bool duckInEndZone = false;
    bool finished = false;
	// Update is called once per frame
	void Update () {
		if(!finished && duckInEndZone)
        {
            timeDuckInEndZone += Time.deltaTime;
            if(timeDuckInEndZone >= timeDuckInEndZoneToEndGame)
            {
                GameObject.FindObjectOfType<DuckGF>().GameFinished(ducklingsSaved);
                finished = true;
            }
        }
	}

    public void OnTriggerEnter2D(Collider2D coli)
    {
        if(coli.gameObject.GetComponent<PlayerDuck>())
        {
            duckInEndZone = true;
        }
        else if(coli.gameObject.GetComponent<Duckling>())
        {
            ducklingsSaved++;
        }
    }

    public void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.gameObject.GetComponent<PlayerDuck>())
        {
            duckInEndZone = false;
        }
        else if (coli.gameObject.GetComponent<Duckling>())
        {

        }
    }
}