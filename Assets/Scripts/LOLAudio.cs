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

    //bool isWebGlBuild = false;

    private LOLAudio()
    {
        if (!MainMenu.SDK_Initialized)
        {
            MainMenu.SDK_Initialized = true;
            LOLSDK.Init("com.Pansimula.BidForLife");
        }
    }

    public void PlayAudio(string _name, bool loop = false)
    {
        if (!MainMenu.Sound_Active)
            return;

        LOLSDK.Instance.PlaySound("Resources/" + _name, false, loop);
        //if (!isWebGlBuild)
        //    PlayLocalAudio(_name, false, loop);
        //else
        //    LOLSDK.Instance.PlaySound("Resources/" + _name, false, loop);
    }

    public void StopAudio(string _name)
    {
        LOLSDK.Instance.StopSound("Resources/" + _name);
    }

    /*private void PlayLocalAudio(string _name, bool background, bool loop)
    {
        _name = System.IO.Path.GetFileNameWithoutExtension(_name);
        Debug.Log("filename: " + _name);

        if(!loop)
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>(_name), new Vector3());
        else
        {
            GameObject go = new GameObject();
            AudioSource audioSrc = go.AddComponent<AudioSource>();
            audioSrc.clip = Resources.Load<AudioClip>(_name);
            audioSrc.loop = true;
            audioSrc.Play();
        }
        
        audioSrc.Play();
            audioSrc.loop = loop;
            
    }*/
}