using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class FinalScene : MonoBehaviour {

	public void CompleteGame()
    {
        LOLSDK.Instance.CompleteGame();
    }
}
