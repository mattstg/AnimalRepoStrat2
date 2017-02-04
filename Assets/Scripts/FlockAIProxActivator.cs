using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAIProxActivator : MonoBehaviour {

    //This script creates a collider that follows the attached object, and activates any flocking AI that it encounters
    public Vector2 size;
	// Use this for initialization
	void Start () {
        GameObject flockAiProxActivator = new GameObject();
        flockAiProxActivator.name = "flockAiProxActivator";
        flockAiProxActivator.AddComponent<FollowTransform>().toFollow = transform;
        BoxCollider2D bc2d = flockAiProxActivator.AddComponent<BoxCollider2D>();
        bc2d.size = size;
        bc2d.isTrigger = true;
        flockAiProxActivator.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        flockAiProxActivator.AddComponent<FlockAiProxHandler>();
        Destroy(this);
    }

   
}
