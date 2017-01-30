using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockingAI : MonoBehaviour
{   //Settings initiate as a decent caribou/buffalo flocker
    //Works best with 1 mass and 4 linear drag
    //tested on circles of scale 4,2,0 with colliders 

    bool waypointCountDone = false;
	int numOfWP;
    bool isCorpse = false;
	Rigidbody2D rigidbody;
    int activeWaypoint = 0;
    float speed;
    float rotationSpeed;

    public bool turnsHead = true;
    public GameObject head;
    public float headRotationSpeed = 20f;

    public int updateFrequency = 5; //the average number of ticks between vector updates

    public Vector2 speedRange = new Vector2(10, 15);
	
	public Vector2 rotationSpeedRange = new Vector2(12, 16);

	public bool isCalf = false;

    public bool isDuckling = false;

    public bool fleesFromPredators = true;
    public float predFleeDistance = 25f;
    public float fleeWeight = 1f;

    


	public float averageHeadingWeight = 15f; //multiplier for strength of averageHeading influence

	public float averagePositionWeight = 1f; //multiplier for strength of averagePosition influence

	public float neighbourDistance = 6f; //distance at which other animal has influence

    public float repulsionDistance = 0.75f; //distance at which other animal has a repulsive effect

    public float repulsionWeight = 15f; //multiplier for strength of repulsion influence

	

	public bool usesWaypoints = false;
	public float waypointWeight = 1f; //multiplier for strength of waypoint influence
	public float waypointReachedProximity;

    public bool followsLeaders = false;
	public float leaderWeight = 1f; //multiplier for strength of leader influence
	public float leaderInfluenceDistance;

    public bool followsMother = false;
    public GameObject mother;
    public float motherWeight = 1f; //multiplier for strength of mother influence

    public bool usesRandom = true; //applies a random influence on direction
	public float randomWeight = 1.5f; //multiplier for strength of random influence
	public float randomRange = 45f; //the largest degree turn left or right that the random vector can be
    public int randVectUpdateFrequency = 30;
    Vector2 vRandom = Vector2.zero;

    public bool usesDirectionalInertia = true;
	public float inertiaWeight = 1f;


	public bool randomizesSpeed = true;

    public bool randomizesRotationSpeed = true;

    public bool speedNormalizing = false; // causes animals to adjust their speed to the average of those within neighbourDistance

    public bool isPredator = false;
    public float huntRange = 40f;
    public float huntWeight = 1f;
    public bool scavenges = false;
    public float scavengeDistance = 15f;
    public float scavengeWeight = 12;
    public float interPredatorRepelDistance = 5f;
    public float interPredatorRepelWeight = 1f;
    public float predNeighborhoodSize = 20f;
    public float predAverageHeadingWeight = 1f;
    public float predAveragePositionWeight = 1f;






    // Methods

    Vector2 V3toV2(Vector3 _vector3) //makes a Vector2 out of the x and y of a Vector3
	{
		float x = _vector3.x;
		float y = _vector3.y;
		Vector2 newVector = new Vector2(x, y);
		return newVector;
	}

    public void Dies()
    {
        isCorpse = true;
        rigidbody.bodyType = RigidbodyType2D.Static;
        GameObject.FindObjectOfType<FlockManager>().flock.Remove(this.gameObject);
        GameObject.FindObjectOfType<CalfManager>().calfTransforms.Remove(this.transform);
        GameObject.FindObjectOfType<Corpsemanager>().Corpses.Add(this.gameObject);
        gameObject.AddComponent<DecayCounter>();
        

    }

    void OnCollisionEnter2D(Collision2D coli)
    {
        FlockingAI ai = coli.gameObject.GetComponent<FlockingAI>();
        if(ai && ai.isCalf && !(ai.isCorpse))
        {
            ai.Dies();
        }
    }

    void ApplyRules()
    {
        if (!isCorpse)
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
            Vector2 vInertia = Vector2.zero;
            Vector2 vFlee = Vector2.zero;
            Vector2 vHunt = Vector2.zero;
            Vector2 vScavenge = Vector2.zero;
            Vector2 vPredCenter = Vector2.zero;
            Vector2 vPredHeading = Vector2.zero;
            Vector2 vPredRepel = Vector2.zero;



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

                        if (!isPredator)
                        {
                            if (dist < repulsionDistance)
                                vRepel += (thisPos - _animalsPos);
                        }
                        else
                        {
                            if (!(_animal.GetComponent<FlockingAI>().isCalf)) //predators don't get repelled by calves
                                if (dist < repulsionDistance)
                                    vRepel += (thisPos - _animalsPos);

                        }

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
                    vWaypoint = vWaypoint.normalized; //now equals normalized vector towards active waypoint
                }
            }

            if (followsLeaders)
            {
                List<Vector2> poslist = new List<Vector2>();
                poslist = GameObject.FindObjectOfType<LeaderManager>().leaders;
                float closestLeaderDistance = 1000;
                Vector2 closestLeaderPos = Vector2.zero;
                if (poslist.Count != 0)
                {
                    foreach (Vector2 _leader in poslist)
                    {
                        float distanceFromLeader = Vector2.Distance(_leader, thisPos);
                        if (distanceFromLeader < closestLeaderDistance)
                        {
                            closestLeaderDistance = distanceFromLeader;
                            closestLeaderPos = _leader;
                        }
                    }

                    if (closestLeaderDistance <= leaderInfluenceDistance)
                    {
                        Vector2 v = closestLeaderPos - V3toV2(this.transform.position);
                        Vector2 vNormalized = v.normalized;
                        vLeader = vNormalized;//now equals normalized vector towards all influencing leaders
                    }

                }
            }

            if (followsMother)
            {
                
                Vector2 v = (V3toV2(mother.transform.position) - V3toV2(this.transform.position));
                Vector2 vNormalized = v.normalized;
                vMother = v.normalized;

                if (isDuckling && !gameObject.GetComponent<Duckling>().isDead)
                {
                    vMother *= gameObject.GetComponent<Duckling>().quackStrength;
                }

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

            }
            if (randomizesSpeed)
                speed = Random.Range(speedRange.x, speedRange.y); //redetermines speed randomly
            if (randomizesRotationSpeed)
                rotationSpeed = Random.Range(rotationSpeedRange.x, rotationSpeedRange.y); //redetermines rotationSpeed randomly

            if (fleesFromPredators)
            {
                List<Vector2> predPositions = new List<Vector2>();
                predPositions = GameObject.FindObjectOfType<PredatorManager>().predatorPositions;
                float closestPredDistance = 1000;
                Vector2 closestPredPos = Vector2.zero;
                if (predPositions.Count != 0)
                {
                    foreach (Vector2 _predPosition in predPositions)
                    {
                        float distanceFromPred = Vector2.Distance(_predPosition, thisPos);
                        if (distanceFromPred < predFleeDistance)
                        {
                            closestPredDistance = distanceFromPred;
                            closestPredPos = _predPosition;
                        }
                    }

                    if (closestPredDistance <= predFleeDistance)
                    {
                        Vector2 v = V3toV2(this.transform.position) - closestPredPos;
                        Vector2 vNormalized = v.normalized;
                        vFlee = vNormalized;
                    }
                }
            }

            if (isPredator)
            {
                //List<Vector2> predators = new List<Vector2>();
                //predators = GameObject.FindObjectOfType<PredatorManager>().predatorPositions;
                List<Transform> predtransforms = new List<Transform>();
                predtransforms = GameObject.FindObjectOfType<PredatorManager>().predatorTransforms;
                List<Transform> calfTrans = GameObject.FindObjectOfType<CalfManager>().calfTransforms;
                float closestCalfDistance = 1000;
                Vector2 closestCalfPos = Vector2.zero;
                if (calfTrans.Count != 0)
                {
                    foreach (Transform _calfTrans in calfTrans)
                    {
                        Vector2 _calfPos = V3toV2(_calfTrans.position);
                        float distanceFromCalf = Vector2.Distance(_calfPos, thisPos);
                        if (distanceFromCalf < closestCalfDistance)
                        {
                            closestCalfDistance = distanceFromCalf;
                            closestCalfPos = _calfPos;
                        }
                    }

                    if (closestCalfDistance <= huntRange)
                    {
                        Vector2 v = closestCalfPos - thisPos;
                        Vector2 vNormalized = v.normalized;
                        vHunt = vNormalized;//now equals normalized vector towards all influencing leaders
                    }
                }
                foreach (Transform _predatorTrans in predtransforms)
                {
                    if (_predatorTrans != this.transform)
                    {
                        float distance;
                        Vector2 predatorPos = V3toV2(_predatorTrans.position);
                        distance = Vector2.Distance(predatorPos, thisPos);
                        if (distance <= predNeighborhoodSize)
                        {
                            Vector2 _predsForward = V3toV2(_predatorTrans.right);  // right because unity 2d is stupid

                            vPredCenter += predatorPos;
                            vPredCenter = vPredCenter.normalized;
                            vPredHeading += _predsForward;
                            vPredHeading = vPredCenter.normalized;
                            if (distance < interPredatorRepelDistance)
                            {
                                vPredRepel += (thisPos - predatorPos);
                                vPredRepel = vPredRepel.normalized;
                            }


                        }
                    }
                }
            }

            if (scavenges)
            {
                float closestCorpseDistance = 1000;
                Vector2 closestCorpsePos = Vector2.zero;
                List<GameObject> corpses = GameObject.FindObjectOfType<Corpsemanager>().Corpses;
                if (corpses.Count != 0)
                {   
                    foreach (GameObject _corpse in corpses)
                    {
                        Vector2 corpsePos = V3toV2(_corpse.transform.position);
                        float distanceFromCorpse = Vector2.Distance(corpsePos, thisPos);
                        if (distanceFromCorpse < closestCorpseDistance)
                        {
                            closestCorpseDistance = distanceFromCorpse;
                            closestCorpsePos = corpsePos;
                        }
                    }
                    

                    if (closestCorpseDistance <= scavengeDistance)
                    {
                        Vector2 v = closestCorpsePos - thisPos;
                        Vector2 vNormalized = v.normalized;
                        vScavenge = vNormalized;
                        
                    }
                }
            }


            //THE ALMIGHTY ROTATE CODE
            Vector2 direction;
            
                
            
                direction =
                      ((vCenter * averagePositionWeight)
                    + (vRepel * repulsionWeight)
                    + (vHeading * averageHeadingWeight)
                    + (vWaypoint * waypointWeight)
                    + (vLeader * leaderWeight)
                    + (vMother * motherWeight)
                    + (vRandom * randomWeight)
                    + (vInertia * inertiaWeight)
                    + (vFlee * fleeWeight)
                    + (vHunt * huntWeight)
                    + (vPredRepel * interPredatorRepelWeight)
                    + (vPredHeading * predAverageHeadingWeight)
                    + (vPredCenter * predAveragePositionWeight)
                    + (vScavenge * scavengeWeight));


            direction = direction.normalized;

            if (direction != Vector2.zero)
            {
                //quaternions in 2D are so goddamn annoying
                Vector3 direction3 = new Vector3(direction.x, direction.y, 0);
                float angle = Mathf.Atan2(direction3.y, direction3.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
                if(turnsHead)
                {
                    Quaternion q2 = q * Quaternion.Euler(0, 0, 270f);
                    head.transform.rotation = Quaternion.Slerp(head.transform.rotation, q2, Time.deltaTime * headRotationSpeed);
                }
                
            }



        }
    }

            

                void Start()
                 {
                    if (!isPredator)
                    {
                        GameObject.FindObjectOfType<FlockManager>().flock.Add(this.gameObject); // adds this animal to FlockManager's flock list
                        
                    }
                    
                    rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
                    speed = Random.Range(speedRange.x, speedRange.y);
                    rotationSpeed = Random.Range(rotationSpeedRange.x, rotationSpeedRange.y);

                }


                void Update()
                 {
        if (usesRandom)
        {
            if (Random.Range(0, randVectUpdateFrequency) == 0)
            {
                float x = this.transform.right.x;
                float y = this.transform.right.y;
                float deltaAngle = Random.Range(-randomRange, randomRange);
                deltaAngle = deltaAngle * Mathf.Deg2Rad;
                float cosTheta = Mathf.Cos(deltaAngle);
                float sinTheta = Mathf.Sin(deltaAngle);
                float newX = (x * cosTheta) - (y * sinTheta);
                float newY = (x * sinTheta) + (y * cosTheta);

                vRandom.x = newX;
                vRandom.y = newY;
                vRandom = vRandom.normalized;// now equals a normalized random vector
            }
        }

        if (!waypointCountDone && usesWaypoints)
                    {
                        numOfWP = GameObject.FindObjectOfType<WaypointManager>().waypointPositions.Count;
                        waypointCountDone = true;
                    }

                    if (usesWaypoints)
                    {
                        Vector2 waypointCoords = GameObject.FindObjectOfType<WaypointManager>().waypointPositions[activeWaypoint];
                        float distanceToWaypoint = Vector2.Distance(waypointCoords, V3toV2(this.transform.position));
                        if (distanceToWaypoint <= waypointReachedProximity)
                        {
                
                if (activeWaypoint + 1 < numOfWP)
                {
                    activeWaypoint++;
                    
                }
                else
                    usesWaypoints = false;


                        }
                    }

        if (!(isDuckling && gameObject.GetComponent<Duckling>() == null))
        {
            if (Random.Range(0, updateFrequency) == 0)    //1 chance per <updateFrequency> each update that rules will be applied 
            { 
                ApplyRules();
            }

            Vector2 thisFacingDir = V3toV2(this.transform.right);


            rigidbody.AddForce(thisFacingDir * speed);
        }

                }

            }
        


