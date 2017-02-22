using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Tadpole : MonoBehaviour {

    Frog.FrogInfo frogInfo;
    float timeRemainingToGrow;
    Vector2 tadpoleHatchRange = new Vector2(15,30);
    Puddle puddle = null;
    bool tadpoleInitialized = false;
    bool oneUpdateWarning = true;

    // Use this for initialization
    public void Start()
    {
        if (!tadpoleInitialized) //cinematic tadpoles dont get birthed
            BirthTadpole(new Frog.FrogInfo(0, false, MathHelper.Fiftyfifty()));
    }


    public void BirthTadpole(Frog.FrogInfo _frogInfo)
    {
        tadpoleInitialized = true;
        frogInfo = new Frog.FrogInfo(_frogInfo);
        timeRemainingToGrow = Random.Range(tadpoleHatchRange.x, tadpoleHatchRange.y);
        float randAngle = Random.Range(0, 360);
        Vector2 randStartForce = MathHelper.DegreeToVector2(randAngle) * 5;
        GetComponent<Rigidbody2D>().AddForce(randStartForce);
    }
	
	// Update is called once per frame
	void Update () {
        timeRemainingToGrow -= Time.deltaTime;
        if (!puddle)
            if (!oneUpdateWarning)
                KillTadpole();
            else
                oneUpdateWarning = false;

        if (timeRemainingToGrow <= 0)
        {
            if (GameObject.FindObjectOfType<FrogWS>().frogParent.childCount < FrogGF.maxFrogCount)
            {
                GameObject newFrog = Instantiate(Resources.Load("Prefabs/Frog")) as GameObject;
                newFrog.transform.SetParent(GameObject.FindObjectOfType<FrogWS>().frogParent);
                newFrog.transform.position = transform.position;
                newFrog.GetComponent<Frog>().CreateFrog(frogInfo);
            }
            KillTadpole();
        }

	}

    public void KillTadpole()
    {
        if(puddle)
            puddle.TadpoleLeaves(this);
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.GetComponent<Puddle>())
        {
            puddle = coli.GetComponent<Puddle>();
            puddle.AddTadpole(this);
        }
    }

    public void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.GetComponent<Puddle>())
            KillTadpole();
    }
}
