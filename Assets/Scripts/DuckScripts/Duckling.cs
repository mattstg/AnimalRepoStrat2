using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duckling : MonoBehaviour {
    
    public int maxQuackStrength = 5;
    public float quackDecayRate = 1;
    public float quackStrength = 0;
	public bool isDead = false;
    // Update is called once per frame

    void Start()
    {
        GameObject.FindObjectOfType<DucklingManager>().Ducklings.Add(this.gameObject);
    }
    void Update () {
		if(isDead)
        {
            GameObject.FindObjectOfType<DucklingManager>().Ducklings.Remove(this.gameObject);
        }
        if (quackStrength > 0)
        {
            quackStrength -= Time.deltaTime * quackDecayRate;
        }
        else if(quackStrength <= 0)
        {
            quackStrength = 0;
        }
	}
}
