using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class WaterStream : MonoBehaviour {

    public float forceOfStream = 0;
    public Vector2 dirOfStream = new Vector2(1,0);

	public void OnCollisionStay2D(Collision2D coli)
    {
        if (coli.gameObject.GetComponent<Rigidbody2D>())
        {
			Vector2 p = coli.gameObject.transform.position;
			coli.gameObject.transform.position = Vector2.MoveTowards (p, p + dirOfStream * forceOfStream, forceOfStream * Time.deltaTime);
        }
    }

    public void OnTriggerStay2D(Collider2D coli)
    {
        if (coli.gameObject.GetComponent<Rigidbody2D>())
		{
            Vector2 p = coli.gameObject.transform.position;
			coli.gameObject.transform.position = Vector2.MoveTowards (p, p + dirOfStream * forceOfStream, forceOfStream * Time.deltaTime);
		}
    }
}
