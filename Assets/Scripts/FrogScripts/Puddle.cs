using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Puddle : MonoBehaviour {

    public int carryingCapacity = 10; //how many it can handle
    public List<Tadpole> activeTadpoles = new List<Tadpole>();
    float tadpoleKillCounter = 3;
    public Vector2 originalSize;
    public float originalCarryingCapacity;
    SpriteRenderer spriteRenderer;
    float originalAlpha;

    public void Awake()
    {
        originalSize = transform.localScale;
        originalCarryingCapacity = carryingCapacity;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalAlpha = spriteRenderer.color.a;
    }

    public void AddTadpole(Tadpole tapdole)
    {
        //Debug.Log("tadpole added: " + activeTadpoles.Count + "/" + carryingCapacity);
		if (activeTadpoles.Count > carryingCapacity * 1.5f)
			KillTadpole (tapdole);
		else
       	 	activeTadpoles.Add(tapdole);
    }

    public void TadpoleLeaves(Tadpole tadpole)
    {
        //Debug.Log("tadpole left: " + activeTadpoles.Count + "/" + carryingCapacity);
        activeTadpoles.Remove(tadpole);
    }

    public void ToggleColliders(bool setActive)
    {
        GetComponent<PolygonCollider2D>().enabled = setActive;
        transform.GetChild(0).gameObject.SetActive(setActive);
    }

    public void Update()
    {
        if (activeTadpoles.Count > carryingCapacity && activeTadpoles.Count > 0)
        {
            tadpoleKillCounter -= Time.deltaTime;
            if (tadpoleKillCounter <= 0)
            {
                //Debug.Log("tadpole killed: " + activeTadpoles.Count + "/" + carryingCapacity);
                tadpoleKillCounter = 3;
				KillTadpole(activeTadpoles[activeTadpoles.Count - 1]);
            }
        }
    }

	private void KillTadpole(Tadpole t)
	{
        if(t != null)
		    Destroy(t.gameObject);
		activeTadpoles.Remove(t);
	}

    public void SetAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        float newAlpha = alpha * originalAlpha;
        if (newAlpha >= 0f && newAlpha <= 1f)
            c.a = newAlpha;
        else
            c.a = Mathf.Min(Mathf.Max(newAlpha, 0), 1);
        spriteRenderer.color = c;
    }
}
