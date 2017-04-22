using System.Collections;
using System.Collections.Generic;
using System;


public class staticWaypoint {
	public float x;
	public float y;
	private int numberOfWaypoints = -1;
	public bool hasNextWaypoint = false;

	public staticWaypoint[] ways = new staticWaypoint[3];

	public staticWaypoint(staticWaypoint[] _ways, float _x, float _y, bool _hasNext, int numWay){
		hasNextWaypoint = _hasNext;
		numberOfWaypoints = numWay;
		ways = _ways;
		x = _x;
		y = _y;

	}

	public staticWaypoint getNextWaypoint(){
		return ways[new Random ().Next (0, numberOfWaypoints + 1)];
	}
}
