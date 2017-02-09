using UnityEngine; using LoLSDK;
using System.Collections;
using System.Collections.Generic;

public class LeaderManager : MonoBehaviour
{
       public int posUpdateFrequency = 5;
       public List<Vector2> leaders = new List<Vector2>();
	public List<Transform> leaderTrans = new List<Transform> ();
       int numOfLeaders;
    // public List<GameObject> listGos = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

        //GameObject[] tempI = GetComponentsInChildren<GameObject>();
        // listGos.AddRange(tempI);
        
        foreach (Transform t in this.transform) 
		{
             
			leaders.Add (t.position);
			leaderTrans.Add (t);
		}
        numOfLeaders = leaders.Count;
		//Debug.Log (numOfLeaders);
	}

	// Update is called once per frame
	void Update()
    {
        if (Random.Range(0, posUpdateFrequency) == 0)
        {
			
            for (int i = 0; i < numOfLeaders; i++)
            {

				leaders [i] = leaderTrans [i].position;
            }


        }  


    }

    
}