using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Snake : MonoBehaviour {

    float frogPts = .65f;
    float tadpolePt = .35f;
    //public float pointsEaten;
    float snakeSpeed = .9f;
    public Vector2 snakeMoveDir = new Vector2(0, -1);

    public void UpdateSnake(float dt)
    {
        transform.position = MathHelper.V3toV2(transform.position) + snakeSpeed * snakeMoveDir * dt;
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
        if(coli.CompareTag("SnakeExit"))
            SnakeManager.Instance.SnakeReachedEnd(this);
    }

    public void ResolveCollision(GameObject coli)
    {
        //only eats tadpoles this way since they have colliders, frogs are checked using SnakeManager
        if(coli.CompareTag("Tadpole"))
        {
            Tadpole food = coli.GetComponent<Tadpole>();
            food.KillTadpole();
            //pointsEaten += tadpolePt;
        }
    }
}
