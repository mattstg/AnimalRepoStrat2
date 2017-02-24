using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEndZone : MonoBehaviour {

	public void OnTrigger2DEnter(Collider2D coli)
    {
            //coli.gameObject.GetComponent<PlayerFish>().
    }

    public void OnCollision2DEnter(Collision2D coli)
    {
        if (coli.gameObject.GetComponent<PlayerFish>())
        {
            GameObject.FindObjectOfType<FishGF>().nextStep = true;
            Destroy(this);
        }
        
        //coli.gameObject.GetComponent<PlayerFish>().
    }
}
