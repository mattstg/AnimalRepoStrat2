using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteActivator : MonoBehaviour {

	Dictionary<Vector2,List<Transform>> grid;
	public Vector2 gridDimensions;
	public float gridSize;  //please use divisible sizes
	public Transform rockParent;
	public Transform fernParent;
	public bool rockPartialDisable = false;

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
		if (curUpdate >= updateTimer) 
		{
			curUpdate = 0;
			Vector2 curGrid = new Vector2 ((int)transform.position.x, (int)transform.position.y);
			if (curGrid != lastGrid)
			{
				//Turn off everything around last grid, then turn on everything around curGrid
				for (int x = lastGrid.x - 1; x <= lastGrid.x + 1; x++)
					for (int y = lastGrid.y - 1; y <= lastGrid.y + 1; y++) 
					{
						if (grid.ContainsKey (new Vector2 (x, y)))
						{
							foreach (Transform t in grid[new Vector2(x,y)])
							{
								if (rockPartialDisable && t.name == "rock")
								{
									//Disable just sprite renderer
								} else {
									//diasble object entirelly
								}
							}
						}
					}
			}
		}
	}

}
