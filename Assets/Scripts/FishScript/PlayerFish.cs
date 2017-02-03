﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFish : Fish {

    bool playerEnabled = true;
    InputManager inputManager;
    Rigidbody2D rb2d;
    // Use this for initialization
    void  Start () {
        inputManager = GetComponentInChildren<InputManager>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
		followAi = false;
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
}
