using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class DuckEndZone : MonoBehaviour {

    float timeDuckInEndZone = 0;
    float timeDuckInEndZoneToEndGame = 12;
    public bool duckInEndZone = false;
    public bool finished = false;
	// Update is called once per frame
	void Update () {
		if(!finished && duckInEndZone)
        {
            timeDuckInEndZone += Time.deltaTime;
            if(timeDuckInEndZone >= timeDuckInEndZoneToEndGame)
            {
                int ducklingsSaved = 0;
                List<Duckling> ducklings = GameObject.FindObjectOfType<DucklingManager>().Ducklings;
                foreach(Duckling d in ducklings)
                {
                    if (d != null && d.transform.position.y < -36)
                        ducklingsSaved++;
                }
                GameObject.FindObjectOfType<DuckGF>().GameFinished(ducklingsSaved);
                finished = true;
            }
        }
	}

    public void OnTriggerEnter2D(Collider2D coli)
    {
        if(coli.gameObject.GetComponent<PlayerDuck>())
            duckInEndZone = true;
    }

    public void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.gameObject.GetComponent<PlayerDuck>())
            duckInEndZone = false;
    }
}
