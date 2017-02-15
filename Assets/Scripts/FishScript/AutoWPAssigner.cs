using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class AutoWPAssigner : MonoBehaviour {

    public bool upwardsOnly;

	public void Start()
    {
        waypointScript[] wps = GameObject.FindObjectsOfType<waypointScript>();
        float dist = 0;
        float closestDistance = 9999;
        int closestIndex = 0;
        for(int i = 0; i < wps.Length; i++)
        {
            if (wps[i].transform.position.y > transform.position.y || !upwardsOnly)
            {
                dist = (Vector2.Distance(transform.position, wps[i].transform.position));
                if (dist < closestDistance)
                {
                    closestIndex = i;
                    closestDistance = dist;
                }
            }
        }
        GetComponent<FlockingAI>().currentWaypoint = wps[closestIndex];
        Destroy(this);
        //Debug.Log("fish: " + name + " got wp: " + wps[closestIndex].name);
    }
}
