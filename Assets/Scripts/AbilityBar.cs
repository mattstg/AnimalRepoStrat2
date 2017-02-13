using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour {

    public Image abilityBar;
    
    public void Start()
    {
       // abilityBar.fill
    }

	public void SetFillAmount(float perc)
    {
        perc = Mathf.Clamp(perc, 0, 1);
        abilityBar.fillAmount = perc;
    }
}
