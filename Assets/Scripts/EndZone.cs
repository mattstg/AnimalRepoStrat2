using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D coli)
    {
        if (coli.gameObject.layer == LayerMask.NameToLayer("Player"))
            GameObject.FindObjectOfType<GameFlow>().nextStep = true;
    }
}
