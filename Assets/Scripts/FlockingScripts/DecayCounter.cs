using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayCounter : MonoBehaviour {
    public float countdown = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            GameObject.FindObjectOfType<Corpsemanager>().Corpses.Remove(this.gameObject);
            Destroy(gameObject);
        }
	}
}
