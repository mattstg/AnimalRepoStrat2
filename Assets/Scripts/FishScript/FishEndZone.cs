using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEndZone : MonoBehaviour {

	public void OnTrigger2DEnter(Collider2D coli)
    {
        Debug.Log("coli: " + coli.gameObject.name);
       
            //coli.gameObject.GetComponent<PlayerFish>().
    }

    public void OnCollision2DEnter(Collision2D coli)
    {
        Debug.Log("coli: " + coli.gameObject.name);
        if (coli.gameObject.GetComponent<PlayerFish>())
            GameObject.FindObjectOfType<FishGF>().nextStep = true;
        //coli.gameObject.GetComponent<PlayerFish>().
    }
}
