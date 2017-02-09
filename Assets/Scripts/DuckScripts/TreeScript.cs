using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class TreeScript : MonoBehaviour {

    List<Transform> activeCollisions = new List<Transform>();

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
        if(!entering)
        {
            activeCollisions.Remove(go.transform);
            if (activeCollisions.Count == 0)
                SetAllLeafTransparency(1);                
        }
        else
        {
            //if(go.GetComponent<>) Will be fox, duck and duckling in future
            //activeCollisions.Add(go.transform)
            //SetAllLeafTransparency(.33);

        }
    }

    private void SetAllLeafTransparency(float toSet)
    {
        foreach (Transform t in transform)
        {
            Color c = t.GetComponent<SpriteRenderer>().color;
            c.a = toSet;
            t.GetComponent<SpriteRenderer>().color = c;
        }
    }
}
