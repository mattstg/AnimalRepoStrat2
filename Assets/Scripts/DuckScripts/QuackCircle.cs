using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class QuackCircle : MonoBehaviour {
    public GameObject player;
    float qRadius;
    public float maxAlpha = .5f;
    public float circleVisibleLife = 2f;
    public float currentAlpha = 0f;

    // Use this for initialization
    void Start () {
       qRadius = player.GetComponent<PlayerDuck>().quackRadius;
        float growthFactor = ((qRadius * 2) - 10) / 10;

       transform.localScale += new Vector3(growthFactor,growthFactor,0);
    }
	
	// Update is called once per frame
	void Update () {

        if (currentAlpha >= 0)
        {
            Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = currentAlpha;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
            currentAlpha -= maxAlpha / (circleVisibleLife / Time.deltaTime);

        }

        else if (currentAlpha < 0)
        {
            currentAlpha = 0f;
        }
        

    }
}
