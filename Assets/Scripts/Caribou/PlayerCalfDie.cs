using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCalfDie : MonoBehaviour {

    float deathTimeTillRestart = 3;	
	// Update is called once per frame
	void Update () {
        deathTimeTillRestart -= Time.deltaTime;
        if(deathTimeTillRestart <= 0)
        {
            GameObject.FindObjectOfType<CaribouGF>().PlayerCalfDied();
            Destroy(this);
        }
	}
}
