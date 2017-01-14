using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderManager : MonoBehaviour
{
	public List<GameObject> leaders = new List<GameObject>();
	// Use this for initialization
	void Start()
	{
		List<GameObject> listGos = new List<GameObject> ();
		listGos.AddRange(GetComponentsInChildren<GameObject>());
		foreach (GameObject t in listGos) 
		{
			leaders.Add (t);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}