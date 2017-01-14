using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tadpole : MonoBehaviour {

    bool isMale;
    float timeRemainingToGrow;
    Vector2 tadpoleHatchRange = new Vector2(15,30);
    Puddle puddle = null;
	public bool playerDescendant = false;
	// Use this for initialization
	void Start () {
        isMale = Random.Range(0f, 1f) > .5f;
        timeRemainingToGrow = Random.Range(tadpoleHatchRange.x, tadpoleHatchRange.y);
        float randAngle = Random.Range(0, 360);
        Vector2 randStartForce = MathHelper.DegreeToVector2(randAngle) * 5;
        GetComponent<Rigidbody2D>().AddForce(randStartForce);
    }
	
	// Update is called once per frame
	void Update () {
        timeRemainingToGrow -= Time.deltaTime;
        if (!puddle)
            KillTadpole();

        if (timeRemainingToGrow <= 0)
        {
            GameObject newFrog = Instantiate(Resources.Load("Prefabs/Frog")) as GameObject;
            newFrog.transform.SetParent(GameObject.FindObjectOfType<FrogWS>().frogParent);
            newFrog.transform.position = transform.position;
			newFrog.GetComponent<Frog>().CreateFrog(isMale,playerDescendant);
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
