using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class FrogWS : MonoBehaviour {

    public SpriteRenderer aridBackground;
    public SpriteRenderer moistBackground;
    public Puddle puddle;
    public Transform frogParent;
    public Transform tadpoleParent;
    public Transform snakeParent;
    public FrogGF frogGF;

    public void Awake()
    {
        FrogGV.frogWS = this;
    }
        
}
