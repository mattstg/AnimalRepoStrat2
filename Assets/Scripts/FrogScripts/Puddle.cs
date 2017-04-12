using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class Puddle : MonoBehaviour {

    public static bool ReducedBirthActive = false;
    public static bool BirthsDenied = false;


    int carryingCapacity = 45; //how many it can handle, once it reaches max, each additional birth only produces 1 tadpole. Unless at maxLimit
    int maxLimit = 60; //if max limit tadpoles, clears it
    List<Tadpole> activeTadpoles = new List<Tadpole>();
    float tadpoleKillCounter = 3;
    public Vector2 originalSize;
    SpriteRenderer spriteRenderer;
    float originalAlpha;

    public void Awake()
    {
        originalSize = transform.localScale;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalAlpha = spriteRenderer.color.a;
    }

    public void ClearTadpoleList()
    {
        activeTadpoles = new List<Tadpole>();
        ReducedBirthActive = BirthsDenied = false;
    }

    public void AddTadpole(Tadpole tapdole)
    {
		activeTadpoles.Add(tapdole);
        SetLimits();
    }

    private void SetLimits()
    {
        int activeCount = activeTadpoles.Count;
        ReducedBirthActive = BirthsDenied = false;
        if (activeCount > carryingCapacity)
            ReducedBirthActive = true;
        if (activeCount >= maxLimit)
            BirthsDenied = true;
    }

    public void TadpoleLeaves(Tadpole tadpole)
    {
        activeTadpoles.Remove(tadpole);
        SetLimits();
    }

    public void ToggleColliders(bool setActive)
    {
        GetComponent<PolygonCollider2D>().enabled = setActive;
        transform.GetChild(0).gameObject.SetActive(setActive);
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
