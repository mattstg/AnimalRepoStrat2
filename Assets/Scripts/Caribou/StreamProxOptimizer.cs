using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamProxOptimizer : MonoBehaviour {

    public List<Transform> allAffectedTransformParents;
    List<AffectedByRiver> affectedList;
    public Transform northStream;
    public Transform southStream;
    public Transform player;
    bool northActive = true;
    bool southActive = true;

    public void Start()
    {
        affectedList = new List<AffectedByRiver>();
        foreach (Transform parents in allAffectedTransformParents)
        {
            foreach(Transform t in parents)
            {
                AffectedByRiver abr = t.GetComponent<AffectedByRiver>();
                if(abr)
                    affectedList.Add(abr);
            }
        }
        allAffectedTransformParents.Clear();
    }

    // Update is called once per frame
    void Update () {
		if(southActive && (player.transform.position.y > 812 || player.transform.position.y < 504))
        {
            southActive = false;
            southStream.gameObject.SetActive(false);
            foreach (AffectedByRiver abr in affectedList)
                abr.Cleanse();
            player.GetComponent<AffectedByRiver>().Cleanse();
        }
        if(!southActive && (player.transform.position.y <= 812 && player.transform.position.y >= 504))
        {
            southActive = true;
            southStream.gameObject.SetActive(true);
        }


        if (northActive && (player.transform.position.y > 1348 || player.transform.position.y < 1145))
        {
            northActive = false;
            northStream.gameObject.SetActive(false);
            foreach (AffectedByRiver abr in affectedList)
                abr.Cleanse();
            player.GetComponent<AffectedByRiver>().Cleanse();
        }
        if (!northActive && (player.transform.position.y <= 1348 && player.transform.position.y >= 1145))
        {
            northActive = true;
            northStream.gameObject.SetActive(true);
        }
    }
}
