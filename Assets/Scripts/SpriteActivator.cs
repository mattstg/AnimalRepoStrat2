using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteActivator : MonoBehaviour {

	Dictionary<Vector2,List<Transform>> grid;
	public Vector2 gridDimensions;
	public float gridSize;  //please use divisible sizes
	public Transform rockParent;
	public Transform fernParent;

	float updateTimer = 3;
	float curUpdate = 0;
	public Vector2 lastGrid;
	//public Transform waterParent;

	public void SetupSpriteActivator()
	{
		grid = new Dictionary<Vector2, List<Transform>> ();
		foreach (Transform t in rockParent)
			AddToGrid (t);
		foreach (Transform t in fernParent)
			AddToGrid (t);
	}

	private void AddToGrid(Transform t)
	{
		int gridx = (int)(t.transform.position.x / gridSize);
		int gridy = (int)(t.transform.position.y / gridSize);
		Vector2 v2 = new Vector2 (gridx, gridy);
		if (grid.ContainsKey (v2)) {
			grid [v2].Add (t);
		} else {
			grid.Add (v2, new List<Transform> (){t});
		}
	}

	public void Update()
	{
		curUpdate += Time.deltaTime;

	}

}
