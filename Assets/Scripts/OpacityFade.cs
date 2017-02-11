using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class OpacityFade : MonoBehaviour {

    private float fadeDuration = 1;
    private float fadeProgress = 1;
    private float sourceOpacity = 1;
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

            //float delta = Time.deltaTime / (fadeDuration - fadeProgress);
            //float newOpacity = presentOpacity + (targetOpacity - presentOpacity) * delta;

            float integral = GetIntegral(fadeProgress / fadeDuration);
            float newOpacity = targetOpacity * integral + sourceOpacity * (1 - integral);
            fadeProgress += Time.deltaTime;
            if (fadeProgress > fadeDuration)
            {
                fadeProgress = fadeDuration;
            }
            ApplyOpacity(newOpacity);
        }
	}

    public float GetVelocity(float progress)
    {
        if (progress >= 0 && progress <= 1)
        {
            return -4 * Mathf.Abs(progress - 0.5f) + 2;
        }
        else
        {
            return 0;
        }
    }

    public float GetIntegral(float progress)
    {
        if (progress < 0)
        {
            return 0;
        }
        else if (progress > 1)
        {
            return 1;
        }
        else if (progress <= 0.5)
        {
            return progress * GetVelocity(progress) / 2;
        }
        else
        {
            return 0.5f + ((progress - 0.5f) * (GetVelocity(progress) + 2) / 2);
        }
    }

    public void SetTargetOpacity(float target, float duration)
    {
        sourceOpacity = GetPresentOpacity();
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
