﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class WaterStream : MonoBehaviour {

	public float forceOfStream = 0;
	public Vector2 dirOfStream = new Vector2(1,0);

    public void OnTriggerEnter2D(Collider2D coli)
    {
        AffectedByRiver abr = coli.gameObject.GetComponent<AffectedByRiver>();
        if (abr != null)
            coli.gameObject.GetComponent<AffectedByRiver>().SetMoveBy(dirOfStream, this, forceOfStream, false);
    }

    public void OnTriggerExit2D(Collider2D coli)
    {
        AffectedByRiver abr = coli.gameObject.GetComponent<AffectedByRiver>();
        if (abr != null)
            coli.gameObject.GetComponent<AffectedByRiver>().SetMoveBy(dirOfStream, this, forceOfStream, true);
    }
    /*
    public void OnCollisionStay2D(Collision2D coli)
	{
		if (coli.gameObject.GetComponent<Rigidbody2D>())
		{
			Vector2 _old = coli.gameObject.transform.position;
			Vector2 _new = coli.gameObject.transform.position = Vector2.MoveTowards (_old, _old + dirOfStream * forceOfStream, forceOfStream * Time.deltaTime);
           

            if (coli.gameObject.GetComponent<PlayerDuck> () != null) {
                Vector2 offset = dirOfStream * forceOfStream * Time.deltaTime;
                PlayerDuck player = coli.gameObject.GetComponent<PlayerDuck> ();
                player.targetPos = player.targetPos + new Vector3(offset.x, offset.y, 0);
			}
		}
	}

	public void OnTriggerStay2D(Collider2D coli)
	{
		if (coli.gameObject.GetComponent<Rigidbody2D>())
		{
			Vector2 _old = coli.gameObject.transform.position;
			Vector2 _new = coli.gameObject.transform.position = Vector2.MoveTowards (_old, _old + dirOfStream * forceOfStream, forceOfStream * Time.deltaTime);
            
            if (coli.gameObject.GetComponent<PlayerDuck> () != null) {
                Vector2 offset = dirOfStream * forceOfStream * Time.deltaTime;
                PlayerDuck player = coli.gameObject.GetComponent<PlayerDuck> ();
                player.targetPos = player.targetPos + new Vector3(offset.x, offset.y, 0);
            }
		}
	}
    */
   
}
