﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class TreeRevealer : MonoBehaviour {

    public float fadeDuration = 1;

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
                SetAllLeafTransparency(go, .27f);
            }
        }
    }

    private void SetAllLeafTransparency(GameObject tree, float toSet)
    {
        foreach (Transform t in tree.transform)
        {
            if (t.name == "Leaves")
            {
                if (t.GetComponent<OpacityFade>())
                {
                    t.GetComponent<OpacityFade>().SetTargetOpacity(toSet, fadeDuration);
                }
                else
                {
                    Color c = t.GetComponent<SpriteRenderer>().color;
                    c.a = toSet;
                    t.GetComponent<SpriteRenderer>().color = c;
                }
            }
        }
    }
}
