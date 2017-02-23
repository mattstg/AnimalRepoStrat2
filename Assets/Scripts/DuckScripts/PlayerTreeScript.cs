using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class PlayerTreeScript : MonoBehaviour {

    public float activationRadius;
    public float fadeDuration;

    public void Start()
    {
        GameObject treeProxActivator = new GameObject();
        treeProxActivator.name = "treeProxActivator";
        treeProxActivator.AddComponent<FollowTransform>().toFollow = transform;
        CircleCollider2D cc2d = treeProxActivator.AddComponent<CircleCollider2D>();
        cc2d.radius = activationRadius;
        cc2d.isTrigger = true;
        treeProxActivator.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        treeProxActivator.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        treeProxActivator.AddComponent<TreeRevealer>();
        treeProxActivator.GetComponent<TreeRevealer>().fadeDuration = fadeDuration;
        Destroy(this);
    }
}
