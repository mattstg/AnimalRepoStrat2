﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class PlayerFish : Fish {

    bool playerEnabled = true;
    InputManager inputManager;
    Rigidbody2D rb2d;
    bool triggeredEndGame = false;
    // Use this for initialization
    void  Start () {
        inputManager = GetComponentInChildren<InputManager>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
		followAi = false;
        moveSpeed = 1.3f;
	}
	
	// Update is called once per frame
	void Update () {
        if(playerEnabled)
		    FishUpdate ();
	}

    public void SetPlayerEnabled(bool _playerEnabled)
    {

        playerEnabled = _playerEnabled;
        inputManager.enabled = playerEnabled;
        if (!playerEnabled)
        {
            rb2d.velocity = new Vector2();
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void OnTriggerEnter2D(Collider2D coli)
    {
        if(coli.gameObject.CompareTag("BoostZone"))
        {
            moveSpeed = 2.0f;
        }
        if (!triggeredEndGame && coli.gameObject.name == "EndZone")
        {
            GameObject.FindObjectOfType<FishGF>().nextStep = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
            triggeredEndGame = true;
        }
        //if oosdgfa
    }

    public void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.gameObject.CompareTag("BoostZone"))
        {
            moveSpeed = 1.3f;
        }
        //if oosdgfa
    }
}
