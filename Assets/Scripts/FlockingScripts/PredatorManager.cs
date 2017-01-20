using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorManager : MonoBehaviour {



    public List<Vector2> predatorPositions = new List<Vector2>();
    public List<Transform> predatorTransforms = new List<Transform>();
    int numOfPredators;

    // Use this for initialization
    void Start()
    {
        foreach (Transform t in this.transform)
        {

            predatorPositions.Add(t.position);
            predatorTransforms.Add(t);
        }
        numOfPredators = predatorPositions.Count;

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numOfPredators; i++)
        {
            predatorPositions[i] = predatorTransforms[i].position;

        }
    }
}