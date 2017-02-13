using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class FlockAiProxHandler : MonoBehaviour {

    //This is whats added to the auto generated object created by FlockAiProxActivator
    List<Transform> enabledObjects = new List<Transform>();

    public void OnTriggerEnter2D(Collider2D coli)
    {
        if (!enabledObjects.Contains(coli.transform))
        {
            if (coli.gameObject.GetComponent<FlockingAI>())
                coli.gameObject.GetComponent<FlockingAI>().enabled = true;
			
            if (coli.gameObject.GetComponent<Fish>())
                coli.gameObject.GetComponent<Fish>().enabled = true;
			
			if (coli.gameObject.GetComponent<Animator>())
				coli.gameObject.GetComponent<Animator>().enabled = true;
			
			if (coli.gameObject.GetComponent<FishLoader> ()) 
				coli.gameObject.GetComponent<FishLoader> ().enabled = true;

            if (coli.gameObject.GetComponent<Wolf>())
                coli.gameObject.GetComponent<Wolf>().enabled = true;

            enabledObjects.Add(coli.gameObject.transform);
        }
    }
}
