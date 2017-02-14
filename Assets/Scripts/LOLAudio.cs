using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class LOLAudio
{
#region Singleton
    private static LOLAudio instance;

    public static LOLAudio Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LOLAudio();
            }
            return instance;
        }
    }
    #endregion

    bool isWebGlBuild = true;

    private LOLAudio(){}

    public void PlayAudio(string _name, bool background = false, bool loop = false)
    {
        if (!MainMenu.Sound_Active)
            return;

        if (!isWebGlBuild)
            PlayLocalAudio(_name, background, loop);
        else
            LOLSDK.Instance.PlaySound("Resources/" + _name, background, loop);
    }

    public void StopAudio(string _name)
    {
        LOLSDK.Instance.StopSound("Resources/" + _name);
    }

    private void PlayLocalAudio(string _name, bool background, bool loop)
    {
        /*
        audioSrc.Play();
            audioSrc.loop = loop;
            */
    }
}