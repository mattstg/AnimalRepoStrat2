using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class AutoWPAssigner : MonoBehaviour {
	WaypointManager link;
    public bool upwardsOnly;

	public void Start()
    {
		link = FindObjectOfType<WaypointManager> ();
		staticWaypoint[] waypoints = link.waypoints.ToArray ();
        float dist = 0;
        float closestDistance = 9999;
        int closestIndex = 0;
		for(int i = 0; i < waypoints.Length; i++)
        {
			if (waypoints[i].y > transform.position.y || !upwardsOnly)
            {
				dist = (Vector2.Distance (transform.position, new Vector2 (waypoints [i].x, waypoints [i].y)));
                if (dist < closestDistance)
                {
                    closestIndex = i;
                    closestDistance = dist;
                }
            }
        }
		GetComponent<FlockingAI>().currentWaypoint = waypoints[closestIndex];
        Destroy(this);
        //Debug.Log("fish: " + name + " got wp: " + wps[closestIndex].name);
    }
}
