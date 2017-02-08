using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTreeScript : MonoBehaviour {

    public float activationRadius;

    public void Start()
    {
        GameObject treeProxActivator = new GameObject();
        treeProxActivator.name = "treeProxActivator";
        treeProxActivator.AddComponent<FollowTransform>().toFollow = transform;
        CircleCollider2D cc2d = treeProxActivator.AddComponent<CircleCollider2D>();
        cc2d.radius = activationRadius;
        cc2d.isTrigger = true;
        treeProxActivator.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        treeProxActivator.AddComponent<TreeRevealer>();
        Destroy(this);
    }
}
