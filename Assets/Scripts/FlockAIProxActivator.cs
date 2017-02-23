using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

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
        flockAiProxActivator.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        flockAiProxActivator.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        flockAiProxActivator.AddComponent<FlockAiProxHandler>();
        Destroy(this);
    }

   
}
