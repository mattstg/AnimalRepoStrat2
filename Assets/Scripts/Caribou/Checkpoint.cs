using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public void OnTriggerExit2D(Collider2D coli)
    {
        if (coli.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameObject.FindObjectOfType<CaribouGF>().SaveCheckpoint(transform.position);
            Destroy(gameObject);
        }
    }
}
