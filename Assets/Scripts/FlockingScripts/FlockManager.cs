﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockManager : MonoBehaviour
{

	public List<GameObject> flock = new List<GameObject>();
	public int population;

	// Use this for initialization
	void Start()
	{
		population = flock.Count;

	}

	// Update is called once per frame
	void Update()
	{

	}
}