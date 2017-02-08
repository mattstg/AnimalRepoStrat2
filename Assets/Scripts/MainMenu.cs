﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public static bool Sound_Active = true;
    public GameObject muteButton;

	public void StartPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FrogScene");
    }

    public void MutePressed()
    {
        Sound_Active = !Sound_Active;
        if(Sound_Active)
        {
            muteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Sound");
        }
        else
        {
            muteButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Mute");
        }
    }
}