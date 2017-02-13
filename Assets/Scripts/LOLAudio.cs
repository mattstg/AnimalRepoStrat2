using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

//Thank you to maulanagin who provided most of the code 
//http://community.legendsoflearning.com/discussion/632/script-for-testing-audio/p1

public class LOLAudio : MonoBehaviour
{
    private static LOLAudio s_Instance = null;
    public static LOLAudio Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = GameObject.FindObjectOfType(typeof(LOLAudio)) as LOLAudio;
            }
            return s_Instance;
        }
    }

    GameObject _AudioSourceObject;
    Dictionary<string, AudioSource> _ASLibrary = new Dictionary<string, AudioSource>();

    public void Play(string name)
    {
#if UNITY_EDITOR
        LocalPlay("Audio/" + name);
#else
        LOLSDK.Instance.PlaySound(name, false, false);
#endif

    }

    void LocalPlay(string soundFileName)
    {
        StartCoroutine(IELocalPlay(soundFileName));
    }

    IEnumerator IELocalPlay(string soundFileName)
    {


        string path;

        path = Application.streamingAssetsPath + "/Resources/Audio/" + soundFileName;

        // Start a download of the given URL
        WWW request = new WWW("file://" + path);

        // Wait for download to complete
        yield return request;

        if (request.error != null)
        {
            Debug.LogError(request.error);
        }
        else {
            // use request.audio 
            AudioClip loadedMp3 = request.GetAudioClip(false, false, AudioType.WAV);
            loadedMp3.name = soundFileName;
            AudioSource audioSource;

            if (_ASLibrary.TryGetValue(soundFileName, out audioSource))
            {
                audioSource.Play();
            }
            else {
                AudioSource newAudioSource = this.gameObject.AddComponent<AudioSource>();
                _ASLibrary.Add(soundFileName, newAudioSource);
                newAudioSource.playOnAwake = false;
                newAudioSource.clip = loadedMp3;
                newAudioSource.Play();

            }
        }
    }
}