using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAiProxHandler : MonoBehaviour {

    //This is whats added to the auto generated object created by FlockAiProxActivator

    public void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.gameObject.GetComponent<FlockingAI>())
        {
            Debug.Log("well this is convient");
        }
    }
}
