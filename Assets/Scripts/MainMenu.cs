using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public static bool Sound_Active = true;
    public GameObject muteButton;

    void Start()
    {
        LOLSDK.Init("com.Pansimula.BidForLife");
        LOLAudio.Instance.PlayAudio("OpenningSong.mp3", false, true);
    }

	public void StartPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FrogScene");
        LOLAudio.Instance.StopAudio("OpenningSong.mp3");
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
