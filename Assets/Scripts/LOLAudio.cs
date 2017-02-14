using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

    bool isEditorMode;

    private LOLAudio()
    {
        #if UNITY_EDITOR
                isEditorMode = true;
        #else
                isEditorMode = false;
        #endif
    }

    public void PlayAudio(AudioSource audioSrc, string name, bool background = false, bool loop = false)
    {
        if (!MainMenu.Sound_Active)
            return;

        if (isEditorMode)
        {
            audioSrc.Play();
            audioSrc.loop = loop;
        }
        else
            LOLSDK.Instance.PlaySound(name, background, loop);
    }

    public void StopAudio(string name)
    {
        LOLSDK.Instance.StopSound(name);
    }
}