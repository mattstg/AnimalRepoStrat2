using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour {

    public GameObject tutorial;
	public GraphManager graphManager;
	public TextPanel textPanel;

	public void Start()
	{
		StartFlow ();
	}

	public virtual void AnsweredGraphCorrectly()
	{
		
	}

	public virtual void TextButtonNextPressed()
	{

	}

	public virtual void StartFlow()
	{

	}
}
