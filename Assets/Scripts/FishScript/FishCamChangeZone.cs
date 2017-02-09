using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class FishCamChangeZone : MonoBehaviour {

    bool camApplied = false;
    Vector3 pos;
    Vector3 rotation;

    public void OnTriggerEnter2D(Collider2D coli)
    {
        if(coli.GetComponent<Fish>())
        {
            camApplied = true;
            //pos & rotation
        }
    }
}
