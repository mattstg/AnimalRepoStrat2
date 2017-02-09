using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class BowerListManager : MonoBehaviour {

	List<Bower> bowerList = new List<Bower>();
	public int i = 0;
	public int count;
	// Use this for initialization
	void Start () {
		bowerList.AddRange(GetComponentsInChildren<Bower> ());
		count = bowerList.Count;
	}



	// Update is called once per frame
	void Update () {
		
	}
		
	public Bower getBower(int ii){
		return bowerList [ii];
	}


	public Bower getNextBower(){
		Bower toRet = bowerList [i];
		if (i == count - 1) {
			i = 0;
		} else {
			i++;
		}
		return toRet;
	}

}
