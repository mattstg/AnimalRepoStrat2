using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteActivator : MonoBehaviour {

	Dictionary<Vector2,List<Transform>> grid;
	//public Vector2 gridDimensions;
	public float gridSize;  //please use divisible sizes
	public Transform rockParent;
	public Transform fernParent;
    public Transform treeParent;
    public List<Transform> waterParents;
	public bool rockPartialDisable = false;

	float updateTimer = 3;
	float curUpdate = 2.5f;
	Vector2 lastGrid = new Vector2(-10,-10);
	//public Transform waterParent;

    public void Start()
    {
        SetupSpriteActivator();
    }

	public void SetupSpriteActivator()
	{
		grid = new Dictionary<Vector2, List<Transform>> ();
        if(rockParent)
        foreach (Transform t in rockParent)
        {
            AddToGrid(t);
            if (rockPartialDisable)
                t.GetComponent<SpriteRenderer>().enabled = false;
            else
                t.gameObject.SetActive(false);
        }
        if(fernParent)
        foreach (Transform t in fernParent)
        {
            AddToGrid(t);
            t.gameObject.SetActive(false);
        }

        if(waterParents != null)
        foreach (Transform waterParent in waterParents)
        {
            foreach (Transform t in waterParent)
            {
                AddToGrid(t);
                t.gameObject.SetActive(false);
            }
        }

        if (treeParent)
            foreach (Transform t in treeParent)
            {
                AddToGrid(t);
                t.gameObject.SetActive(false);
            }
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
			Vector2 curGrid = new Vector2 ((int)(Camera.main.transform.position.x / gridSize), (int)(Camera.main.transform.position.y / gridSize));
           // Debug.Log("curgrid: " + curGrid);
			if (curGrid != lastGrid)
			{
                //Turn off everything around last grid, then turn on everything around curGrid
                for (int x = (int)lastGrid.x - 1; x <= lastGrid.x + 1; x++)
                {
                    for (int y = (int)lastGrid.y - 1; y <= lastGrid.y + 1; y++)
                    {
                        if (grid.ContainsKey(new Vector2(x, y)))
                        {
                            //Debug.Log("disabling grid: " + new Vector2(x, y));
                            foreach (Transform t in grid[new Vector2(x, y)])
                            {
                                SetActive(t, false);
                            }
                        }
                    }
                }

                //Turn on all things for new grid
                for (int x = (int)curGrid.x - 1; x <= curGrid.x + 1; x++)
                {
                    for (int y = (int)curGrid.y - 1; y <= curGrid.y + 1; y++)
                    {
                        if (grid.ContainsKey(new Vector2(x, y)))
                        {
                            foreach (Transform t in grid[new Vector2(x, y)])
                            {
                                //Debug.Log("enabling grid: " + new Vector2(x, y));
                                SetActive(t, true);
                            }
                        }
                    }
                }

                lastGrid = curGrid;
            }
		}
	}

    private void SetActive(Transform t,bool setActive)
    {
        if (rockPartialDisable && t.name == "rock")
        {
            t.GetComponent<SpriteRenderer>().enabled = setActive;
        }
        else
        {
            t.gameObject.SetActive(setActive);
        }
    }

}
