using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class LakeBarrier : MonoBehaviour {

    float forceOfStream = 3;
    Vector2 dirOfStream = new Vector2(0, -1);

    public void OnCollisionStay2D(Collision2D coli)
    {
        if (coli.gameObject.GetComponent<Duckling>())
        {
            Vector2 p = coli.gameObject.transform.position;
            coli.gameObject.transform.position = Vector2.MoveTowards(p, p + dirOfStream * forceOfStream, forceOfStream * Time.deltaTime);
        }
    }

    public void OnTriggerStay2D(Collider2D coli)
    {
        if (coli.gameObject.GetComponent<Duckling>())
        {
            Vector2 p = coli.gameObject.transform.position;
            coli.gameObject.transform.position = Vector2.MoveTowards(p, p + dirOfStream * forceOfStream, forceOfStream * Time.deltaTime);
        }
    }
}
