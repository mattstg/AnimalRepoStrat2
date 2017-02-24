using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeEffector : MonoBehaviour {

	public void OnCollisionEnter2D(Collision2D coli)
    {
        ResolveCollision(coli.gameObject, true);
    }

    public void OnTriggerEnter2D(Collider2D coli)
    {
        ResolveCollision(coli.gameObject, true);
    }

    public void OnCollisionExit2D(Collision2D coli)
    {
        ResolveCollision(coli.gameObject, false);
    }

    public void OnTriggerExit2D(Collider2D coli)
    {
        ResolveCollision(coli.gameObject, false);
    }

    private void ResolveCollision(GameObject go, bool entering)
    {
        if (go.GetComponent<PlayerCaribou>())
        {
            go.GetComponent<PlayerCaribou>().speed = (entering) ? 8 : 12;
        }
        else if (go.GetComponent <FlockingAI>())
        {
            go.GetComponent<FlockingAI>().waterSlowCoeff = (entering) ? .67f : 1f;
        }
    }
}
