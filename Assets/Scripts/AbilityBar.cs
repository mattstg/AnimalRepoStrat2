using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBar : MonoBehaviour {

	public void SetFillAmount(float perc)
    {
        //if negative or over
        perc = Mathf.Clamp(perc, 0, 1);
    }
}
