using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Duckling : MonoBehaviour {
    
    public int maxQuackStrength = 5;
    public float quackDecayRate = 1;
    public float quackStrength = 1;
	public bool isDead = false;
    // Update is called once per frame

    DucklingManager ducklingManager;

    void Start()
    {
        ducklingManager = GameObject.FindObjectOfType<DucklingManager>();
        ducklingManager.Ducklings.Add(this);
    }

    void Update () {
		if(isDead)
        {
            ducklingManager.Ducklings.Remove(this);
        }
        if (quackStrength > 1)
        {
            quackStrength -= Time.deltaTime * quackDecayRate;
            
        }
        else if(quackStrength <= 1)
        {
            quackStrength = 1;
        }
        //Debug.Log(quackStrength);
    }
}
