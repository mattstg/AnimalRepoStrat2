using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour {

    public int carryingCapacity = 10; //how many it can handle
    public List<Tadpole> activeTadpoles = new List<Tadpole>();
    float tadpoleKillCounter = 3;

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

    public void Update()
    {
        if (activeTadpoles.Count > carryingCapacity)
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


}
