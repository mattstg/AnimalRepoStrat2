using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class OpacityFade : MonoBehaviour {

    private float fadeDuration = 1;
    private float fadeProgress = 1;
    private float targetOpacity = 1;

	void Start ()
    {
        if (fadeDuration <= 0)
            fadeDuration = 1;
        fadeProgress = fadeDuration;
    }
	
	void Update ()
    {
        float presentOpacity = GetPresentOpacity();
        if (presentOpacity != targetOpacity)
        {
            if (fadeDuration <= 0)
                fadeDuration = 1;
            float delta = Time.deltaTime / (fadeDuration - fadeProgress);
            float newOpacity = presentOpacity + (targetOpacity - presentOpacity) * delta;
            fadeProgress += Time.deltaTime;
            if (fadeProgress > fadeDuration)
            {
                newOpacity = targetOpacity;
                fadeProgress = fadeDuration;
            }
            ApplyOpacity(newOpacity);
        }
	}

    public void SetTargetOpacity(float target, float duration)
    {
        targetOpacity = target;
        fadeProgress = 0;
        fadeDuration = duration;
    }

    public void ApplyOpacity(float newOpacity)
    {
        Color c = gameObject.GetComponent<SpriteRenderer>().color;
        c.a = newOpacity;
        gameObject.GetComponent<SpriteRenderer>().color = c;
    }

    public float GetPresentOpacity()
    {
        return gameObject.GetComponent<SpriteRenderer>().color.a;
    }
}
