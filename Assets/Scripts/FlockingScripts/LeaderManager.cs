using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderManager : MonoBehaviour
{
       public int posUpdateFrequency = 5;
       public List<Vector2> leaders = new List<Vector2>();
       int numOfLeaders;
    // public List<GameObject> listGos = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

        //GameObject[] tempI = GetComponentsInChildren<GameObject>();
        // listGos.AddRange(tempI);
        
        foreach (Transform t in GetComponentsInChildren<Transform>()) 
		{
             
			leaders.Add (t.position);
		}
        int numOfLeaders = leaders.Count;
	}

	// Update is called once per frame
	void Update()
    {
        if (Random.Range(0, posUpdateFrequency) == 0)
        {
            for (int i = 0; i < numOfLeaders; i++)
            {
                Transform trans = GetComponentsInChildren<Transform>()[i];
                Vector3 pos3 = trans.position;
                Vector2 pos2 = V3toV2(pos3);
                leaders[i] = pos2;
            }
        }  
    }

    Vector2 V3toV2(Vector3 _vector3) //makes a Vector2 out of the x and y of a Vector3
    {
        float x = _vector3.x;
        float y = _vector3.y;
        Vector2 newVector = new Vector2(x, y);
        return newVector;
    }
}