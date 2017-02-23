using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Fox : MonoBehaviour {

	enum FoxState { Idle, FocusedOnGoal} //idle for wander, focused on goal for returning or chasing
	public GameObject waypointParent;
	public Transform foxHole;
	public Transform mouthLoc;
    float radius_growth = .1f;
    List<Vector2> waypoints;
	float patrolBoxSize = 4;
	Vector2 patrolOrigin;
	FoxState currentState = FoxState.Idle;
	public Vector2 goalPos;
	Transform chaseTarget;
	float foxWalkSpeed = 1.5f;
	float foxRunSpeed = 3.5f;
	float foxCurrentSpeed = 1.5f;
	Transform caughtYoung;
	float idleWaitTime = 3;
	int currentWaypointIndex = 0;
	float timeWandering; //used to stop from getting stuck
    bool justAteYoung = false;
    readonly float GVateYoungCooldownMax = 3;
    float ateYoungCooldown = 3;

    DucklingManager ducklingManager;
    FlockManager flockManager;

    // Use this for initialization
    void Awake()
	{
		goalPos = patrolOrigin = transform.position;
		waypoints = new List<Vector2> ();
		foreach (Transform t in waypointParent.transform)
			{
				waypoints.Add (t.position);
				//Destroy (t.gameObject);
			}
        ducklingManager = GameObject.FindObjectOfType<DucklingManager>();
        flockManager = GameObject.FindObjectOfType<FlockManager>();
    }

	
	// Update is called once per frame
	void Update () {
		timeWandering += Time.deltaTime;

        if (justAteYoung)
        {
            ateYoungCooldown -= Time.deltaTime;
            if (ateYoungCooldown <= 0)
            {
                ateYoungCooldown = GVateYoungCooldownMax;
                justAteYoung = false;
                transform.FindChild("sightRadius").gameObject.SetActive(false); //refresh hack
                transform.FindChild("sightRadius").gameObject.SetActive(true);
            }
        }


		if (caughtYoung) {
			caughtYoung.position = mouthLoc.transform.position;
			goalPos = foxHole.position;
		}

		if (!chaseTarget) 
		{
			if (Vector2.Distance (goalPos, transform.position) < .1f) 
			{
				timeWandering = 0;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
				idleWaitTime -= Time.deltaTime;
				if (idleWaitTime <= 0) 
				{
                    if (caughtYoung)
                    {
                        Destroy(caughtYoung.gameObject);
                        caughtYoung = null;
                        transform.FindChild("sightRadius").gameObject.SetActive(false); //refresh hack
                        transform.FindChild("sightRadius").gameObject.SetActive(true);
                        justAteYoung = true;
                        ateYoungCooldown = GVateYoungCooldownMax;
                    }
					idleWaitTime = 3;
					GetNewWanderPoint ();
				}

			} else {
				MoveTowardsGoal ();
			}
		} else {
			MoveTowardsGoal ();
		}

		if (timeWandering >= 7 && !chaseTarget) {
			GetNewWanderPoint ();
			timeWandering = 0;
		}
	}

	private void GetNewWanderPoint()
	{
		currentWaypointIndex++;
		currentWaypointIndex %= waypoints.Count;
		goalPos = waypoints [currentWaypointIndex];
		foxCurrentSpeed = foxWalkSpeed;
	}

	public void OnCollisionEnter2D(Collision2D coli)
	{
        Duckling duckling = coli.gameObject.GetComponent<Duckling>();
        if (duckling != null && !caughtYoung) {
			caughtYoung = coli.gameObject.transform;
			chaseTarget = null;
			idleWaitTime = 3;
            duckling.isDead = true;
            ducklingManager.Ducklings.Remove(duckling);
            flockManager.flock.Remove(caughtYoung.GetComponent<FlockingAI>());
            Destroy (caughtYoung.GetComponent<CircleCollider2D> ());
			Destroy (caughtYoung.GetComponent<Rigidbody2D> ());
			Destroy (caughtYoung.GetComponent<Duckling> ());

		}
	}

	public void OnTriggerEnter2D(Collider2D coli)
	{
		if (coli.GetComponent<Duckling> () && !chaseTarget && !caughtYoung && !justAteYoung) {
			foxCurrentSpeed = foxRunSpeed;
			chaseTarget = coli.transform;

		}

	}

	public void OnTriggerExit2D(Collider2D coli)
	{
		if (coli.GetComponent<Duckling> () && coli.GetComponent<Duckling> ().transform == chaseTarget) {
			chaseTarget = null;
			foxCurrentSpeed = foxWalkSpeed;
			GetNewWanderPoint ();
		}

	}

    public void HeardAQuack()
    {
        Transform t = transform.FindChild("sightRadius");
        t.localScale = t.localScale * (1  + radius_growth);
    }

    private void MoveTowardsGoal()
	{
		if (chaseTarget)
			goalPos = chaseTarget.position;
		float angToGoal = MathHelper.AngleBetweenPoints(this.transform.position, goalPos);
		float distanceToGoal = Vector2.Distance (goalPos, transform.position);
		transform.eulerAngles = new Vector3 (0, 0, angToGoal);
		Vector2 goalDir = goalPos - MathHelper.V3toV2(transform.position);
		float speed = foxCurrentSpeed;
		if (distanceToGoal < foxCurrentSpeed * Time.deltaTime)
			speed = distanceToGoal;
		//GetComponent<Rigidbody2D> ().AddRelativeForce (goalDir.normalized * foxCurrentSpeed * Time.deltaTime,ForceMode2D.Impulse);
		GetComponent<Rigidbody2D> ().velocity = goalDir.normalized * speed;
		//transform.position = Vector2.MoveTowards(transform.position, goalPos, foxCurrentSpeed * Time.deltaTime);
	}
}
