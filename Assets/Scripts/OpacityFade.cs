using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class OpacityFade : MonoBehaviour {

    private float fadeDuration = 1;
    private float fadeProgress = 1;
    private float sourceOpacity = 1;
    private float targetOpacity = 1;
    private SpriteRenderer spriteRenderer;

	void Awake ()
    {
        if (fadeDuration <= 0)
            fadeDuration = 1;
        fadeProgress = fadeDuration;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

    public void SetPresentOpacity(float opacity)
    {
        sourceOpacity = opacity;
        targetOpacity = opacity;
        fadeProgress = fadeDuration;
        ApplyOpacity(targetOpacity);
    }

    public void ApplyOpacity(float newOpacity)
    {
        Color c = spriteRenderer.color;
        c.a = newOpacity;
        spriteRenderer.color = c;
    }

    public float GetPresentOpacity()
    {
        return spriteRenderer.color.a;
    }
}
