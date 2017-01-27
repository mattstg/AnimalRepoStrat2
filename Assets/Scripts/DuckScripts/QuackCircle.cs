using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuackCircle : MonoBehaviour {
    public GameObject player;
    float qRadius;
   

    // Use this for initialization
    void Start () {
       qRadius = player.GetComponent<PlayerDuck>().quackRadius;
        float growthFactor = ((qRadius * 2) - 10) / 10;

       transform.localScale += new Vector3(growthFactor,growthFactor,0);
    }
	
	// Update is called once per frame
	void Update () {
      
        

    }
}
