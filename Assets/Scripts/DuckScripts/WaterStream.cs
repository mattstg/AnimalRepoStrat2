using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStream : MonoBehaviour {

    public float forceOfStream = 0;
    public Vector2 dirOfStream = new Vector2(1,0);

	public void OnCollisionEnter2D(Collision2D coli)
    {
        if(coli.gameObject.GetComponent<Rigidbody2D>())
        {
            coli.gameObject.GetComponent<Rigidbody2D>().AddForce(dirOfStream * forceOfStream*Time.deltaTime,ForceMode2D.Impulse);
        }
    }

    public void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.gameObject.GetComponent<Rigidbody2D>())
        {
            coli.gameObject.GetComponent<Rigidbody2D>().AddForce(dirOfStream * forceOfStream * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
