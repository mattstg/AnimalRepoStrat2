using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTreeScript : MonoBehaviour {

    //List<Transform> activeCollisions = new List<Transform>();

    /*public void OnCollisionEnter2D(Collision2D coli)
    {
        ResolveCollision(coli.gameObject, true);
    }
    
    public void OnCollisionExit2D(Collision2D coli)
    {
        ResolveCollision(coli.gameObject, false);
    }
   
    */

    public void OnTriggerExit2D(Collider2D coli)
    {
        ResolveCollision(coli.gameObject, false);
    }

    public void OnTriggerEnter2D(Collider2D coli)
    {
        ResolveCollision(coli.gameObject, true);
    }

    private void ResolveCollision(GameObject go, bool entering)
    {
        if (go.CompareTag("Tree"))
        {
            if (!entering)
            {
                SetAllLeafTransparency(go, 1f);
            }
            else
            {
                SetAllLeafTransparency(go, .2f);
            }
        }
    }

    private void SetAllLeafTransparency(GameObject tree,float toSet)
    {
        foreach (Transform t in tree.transform)
        {
            if (t.name == "Leaves")
            {
                Color c = t.GetComponent<SpriteRenderer>().color;
                c.a = toSet;
                t.GetComponent<SpriteRenderer>().color = c;
            }
        }
    }
}
