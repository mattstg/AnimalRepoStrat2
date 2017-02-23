using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Snake : MonoBehaviour {


    float frogPts = 1.2f;
    float tadpolePt = .6f;
    public float pointsEaten;
    float snakeSpeed = .9f;
    public Vector2 snakeMoveDir = new Vector2(0, -1);

    public void Update()
    {
        transform.position = MathHelper.V3toV2(transform.position) + snakeSpeed * snakeMoveDir * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D coli)
    {
        ResolveCollision(coli.gameObject);
    }
    public void OnTriggerEnter2D(Collider2D coli)
    {
        ResolveCollision(coli.gameObject);
    }

    public void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.GetComponent<SnakeManager>())
        {
            coli.GetComponent<SnakeManager>().SnakeReachedEnd(this);
            pointsEaten = 0;
        }
    }
    /*public void OnCollisionExit2D(Collision2D coli)
    {
        ResolveCollision(coli.gameObject);
    }
    public void OnTriggerExit2D(Collider2D coli)
    {
        ResolveCollision(coli.gameObject);
    }*/

    public void ResolveCollision(GameObject coli)
    {
        if(coli.GetComponent<Frog>())
        {
            Frog food = coli.GetComponent<Frog>();
            food.FrogEaten();
            pointsEaten += frogPts; 
        }
        else if(coli.GetComponent<Tadpole>())
        {
            Tadpole food = coli.GetComponent<Tadpole>();
            food.KillTadpole();
            pointsEaten += tadpolePt;
        }
    }
}
