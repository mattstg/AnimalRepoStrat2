using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {


    float frogPts = .75f;
    float tadpolePt = .25f;
    float pointsEaten;
    Vector2 snakeSpeed = new Vector2(0, -1);

    public void Update()
    {
        transform.position = MathHelper.V3toV2(transform.position) + snakeSpeed * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D coli)
    {
        ResolveCollision(coli.gameObject);
    }
    public void OnTriggerEnter2D(Collider2D coli)
    {
        ResolveCollision(coli.gameObject);
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
        else if(coli.GetComponent<SnakeManager>())
        {
            coli.GetComponent<SnakeManager>().SnakeReachedEnd(this,pointsEaten);
            pointsEaten = 0;
        }
    }
}
