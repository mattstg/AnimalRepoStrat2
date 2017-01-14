using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour{
	List<Item> itemList = new List<Item>();

	void Start(){
		itemList.AddRange(GetComponentsInChildren<Item> ());
	}

	public Item getRandomItem(){
		return itemList[Random.Range (0, itemList.Count)];
	}
		
}
