using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public enum UISoundEffect { Click, Right, Wrong}
public class CanvasManager : MonoBehaviour {

    public AudioSource audioRight;
    public AudioSource audioWrong;
    public AudioSource audioClick;

    public void PlayAudio(int enumAsInt)
    {
        PlayAudio((UISoundEffect)enumAsInt);
    }

    public void PlayAudio(UISoundEffect soundType)
    {
        switch(soundType)
        {
            case UISoundEffect.Click:
                LOLAudio.Instance.PlayAudio(audioClick, "ClickNoise.wav", false, false);
                break;
            case UISoundEffect.Right:
                LOLAudio.Instance.PlayAudio(audioRight, "correct.wav", false, false);
                break;
            case UISoundEffect.Wrong:
                LOLAudio.Instance.PlayAudio(audioWrong, "wrong.wav", false, false);
                break;
        }
    }
}
