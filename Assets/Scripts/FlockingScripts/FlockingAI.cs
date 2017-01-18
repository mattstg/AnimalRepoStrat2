using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockingAI : MonoBehaviour
{   //Settings initiate as a decent caribou/buffalo flocker
	//Works best with 1 mass and 4 linear drag
	//tested on circles of scale 4,2,0 with colliders 


	//bool shouldLog = true;
	public Rigidbody2D rigidbody;

	float speed;
	public Vector2 speedRange = new Vector2(10, 15);
	float rotationSpeed;
	public Vector2 rotationSpeedRange = new Vector2(12, 16);

	Vector2 averageHeading;
	public float averageHeadingWeight = 15f; //multiplier for strength of averageHeading influence

	Vector2 averagePosition;
	public float averagePositionWeight = 1f; //multiplier for strength of averagePosition influence

	public float neighbourDistance = 6f; //distance at which other animal has influence
	public float repulsionDistance = 0.75f; //distance at which other animal has a repulsive effect
	public float repulsionWeight = 15f; //multiplier for strength of repulsion influence

	public int updateFrequency = 5; //the average number of ticks between vector updates

	public bool usesWaypoints = false;
	public float waypointWeight = 1f; //multiplier for strength of waypoint influence
	public float waypointReachedProximity;
	public bool followsLeaders = false;
	public float leaderWeight = 1f; //multiplier for strength of leader influence
	public float leaderInfluenceDistance;
	public bool followsMother = false;
	public float motherWeight = 1f; //multiplier for strength of mother influence
	public bool usesRandom = true; //applies a random influence on direction
	public float randomWeight = 1.5f; //multiplier for strength of random influence
	public float randomRange = 45f; //the largest degree turn left or right that the random vector can be
	public bool usesDirectionalInertia = true;
	public float inertiaWeight = 1f;


	public bool randomizesSpeed = true;
	public bool randomizesRotationSpeed = true;
	public bool speedNormalizing = false; // causes animals to adjust their speed to the average of those within neighbourDistance

	int activeWaypoint = 0;

	 


	// Methods

	Vector2 V3toV2(Vector3 _vector3) //makes a Vector2 out of the x and y of a Vector3
	{
		float x = _vector3.x;
		float y = _vector3.y;
		Vector2 newVector = new Vector2(x, y);
		return newVector;
	}

	void ApplyRules()
	{
		List<GameObject> animals = new List<GameObject>();
		animals = GameObject.FindObjectOfType<FlockManager>().flock;

		Vector2 thisPos = V3toV2(this.transform.position);

		//vectors below are vectors that could influence direction
		Vector2 vCenter = Vector2.zero;
		Vector2 vRepel = Vector2.zero;
		Vector2 vHeading = Vector2.zero;
		Vector2 vWaypoint = Vector2.zero;
		Vector2 vLeader = Vector2.zero;
		Vector2 vMother = Vector2.zero;
		Vector2 vRandom = Vector2.zero;
		Vector2 vInertia = Vector2.zero;

		int localGroupSize = 0; //this number will represent how many animals are within neighborDistance of this animal 
		float localGroupSpeed = 0f; //this will be used to calculate local average speed for when speedNormalizing = false
		float dist; //will be distance between this animal and whatever animal is being checked by the foreach loop below

		foreach (GameObject _animal in animals)
		{
			if (_animal != this.gameObject)
			{
				dist = Vector2.Distance(_animal.transform.position, this.transform.position);
				if (dist <= neighbourDistance)
				{
					Vector2 _animalsPos = V3toV2(_animal.transform.position);
					Vector2 _animalsForward = V3toV2(_animal.transform.right);  // right because unity 2d is stupid

					vCenter += _animalsPos;
					vHeading += _animalsForward;
					localGroupSize++;

					if (dist < repulsionDistance)
						vRepel += (thisPos - _animalsPos);

					FlockingAI _animalsAI = _animal.GetComponent<FlockingAI>(); //getting _animal's speed for calculating average local speed
					localGroupSpeed += _animalsAI.speed;
				}
			}
		}

		if (localGroupSize > 0)
		{
			vCenter /= localGroupSize;
			vCenter -= thisPos;
			vCenter = vCenter.normalized; //now equals normalized vector from this animal to local average position

			vHeading = vHeading.normalized; //now equals normalized vector from this animal towards local directional average

			vRepel = vRepel.normalized; //now equals normalized vector from this animal away from all repelling animals


		}

		if (usesWaypoints)
		{
            if (GameObject.FindObjectOfType<WaypointManager>().waypointPositions[activeWaypoint] != null)
            {
                vWaypoint = GameObject.FindObjectOfType<WaypointManager>().waypointPositions[activeWaypoint];
                vWaypoint -= thisPos;
                vWaypoint = vWaypoint.normalized;
            } //now equals normalized vector towards active waypoint
		}

		if (followsLeaders)
		{
            List<Vector2> golist = new List<Vector2>();
            golist = GameObject.FindObjectOfType<LeaderManager> ().leaders;
            if (golist[0] != null)
            {
                foreach (Vector2 _leader in golist)
			    {
				
				float distanceFromLeader = Vector2.Distance(_leader, thisPos);
                    if (distanceFromLeader <= leaderInfluenceDistance)
                    {
                        Vector2 v = (_leader - V3toV2( this.transform.position));
                        Vector2 vNormalized = v.normalized;
                        vLeader += vNormalized;//now equals normalized vector towards all influencing leaders
                    }
				}
			}
		}

		if (followsMother)
		{

		}

		if (usesRandom)
		{
			float x = this.transform.right.x;
			float y = this.transform.right.y;
			float deltaAngle = Random.Range (-randomRange, randomRange);
			deltaAngle =deltaAngle * Mathf.Deg2Rad;
			float cosTheta = Mathf.Cos (deltaAngle);
			float sinTheta = Mathf.Sin (deltaAngle);
			float newX = (x * cosTheta) - (y * sinTheta);
			float newY = (x * sinTheta) + (y * cosTheta);

			vRandom.x = newX;
			vRandom.y = newY;
			//vRandom = vRandom.normalized;// now equals a normalized random vector
		}

		if (usesDirectionalInertia) 
		{
			vInertia = this.transform.right;
		}

		if (speedNormalizing) //sets this animal speed to average local speed
		{
            if (localGroupSize != 0)
            {
                localGroupSpeed /= localGroupSize;
                speed = localGroupSpeed;
            }
            else
            {
                speed = Random.Range(speedRange.x, speedRange.y);
            }
		}
		if (randomizesSpeed)
			speed = Random.Range(speedRange.x, speedRange.y); //redetermines speed randomly
		if (randomizesRotationSpeed)
			rotationSpeed = Random.Range(rotationSpeedRange.x, rotationSpeedRange.y); //redetermines rotationSpeed randomly

        if (!followsLeaders)
            Debug.Log("3 " + speed);

        //THE ALMIGHTY ROTATE CODE

        Vector2 direction =
			(
				(vCenter * averagePositionWeight)
				+ (vRepel * repulsionWeight)
				+ (vHeading * averageHeadingWeight)
				+ (vWaypoint * waypointWeight)
				+ (vLeader * leaderWeight)
				+ (vMother * motherWeight)
				+ (vRandom * randomWeight)
				+ (vInertia * inertiaWeight));

		direction = direction.normalized;

		if (direction != Vector2.zero)
		{
			//quaternions in 2D are so goddamn annoying
			Vector3 direction3 = new Vector3(direction.x, direction.y, 0);
			float angle = Mathf.Atan2(direction3.y, direction3.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

		}



	}


	void Start()
	{

		GameObject.FindObjectOfType<FlockManager>().flock.Add(this.gameObject); // adds this animal to FlockManager's flock list

		rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
		speed = Random.Range(speedRange.x, speedRange.y);
		rotationSpeed = Random.Range(rotationSpeedRange.x, rotationSpeedRange.y);
        if (!followsLeaders)
            Debug.Log("2 " + speed);


        /*foreach(GameObject animal in GameObject.FindObjectOfType<FlockManager>().flock)
        {
            Debug.Log(speed);
        }                     */

    }


	void Update()
	{
		if (usesWaypoints) 
		{
			Vector2 waypointCoords = GameObject.FindObjectOfType<WaypointManager> ().waypointPositions [activeWaypoint];
			float distanceToWaypoint = Vector2.Distance (waypointCoords,
				                           this.transform.position);
			if (distanceToWaypoint <= waypointReachedProximity) 
			{
				int numOfWP = GameObject.FindObjectOfType<WaypointManager> ().waypointPositions.Count;
				if (activeWaypoint + 1 < numOfWP)
					activeWaypoint++;
				else
					usesWaypoints = false;

					
			}
		}
      


        if (Random.Range (0, updateFrequency) == 0) { //1 chance per <updateFrequency> each update that rules will be applied 
			ApplyRules ();
		}

		Vector2 thisFacingDir = V3toV2 (this.transform.right);
        if (!followsLeaders)
            Debug.Log(thisFacingDir.ToString() + " " + speed);
		rigidbody.AddForce (thisFacingDir * speed);


		//if (shouldLog)
		//{
		//    Debug.Log(this.transform.right.x + " " + this.transform.right.y + " " + this.transform.right.z);
		//   shouldLog = false;    
		//}
		//if (this.transform.forward == Vector3.zero)
		// Debug.Log("zero");
		//if (thisFacingDir == Vector2.zero)
		//  Debug.Log("zero"); 

	}

	}


