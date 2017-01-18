﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockManager : MonoBehaviour
{
    public GameObject animalPrefab;

    public int startPop = 100;

    int spawnBoundary = 20;
    public List<GameObject> flock = new List<GameObject>();
	

	// Use this for initialization
	void Start()
	{
        for (int i = 0; i < startPop; i++)
        {
            Vector3 position = new Vector3(Random.Range(-spawnBoundary, spawnBoundary), Random.Range(-spawnBoundary, spawnBoundary), 0);
            Instantiate(animalPrefab, position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }

    }

	// Update is called once per frame
	void Update()
	{

	}
}