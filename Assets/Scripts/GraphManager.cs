using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphManager : MonoBehaviour {

	GameFlow gameflow;

	string slotmbPrefab = "Prefabs/SlotMB";

	public Transform grid;
	public Button gridButton;
	public Text titleText;

	public bool allCorrect = false;
    public bool hasEvaluated = false;

    public void Awake()
    {
        gameflow = GameObject.FindObjectOfType<GameFlow>();
    }

	public void ResetGraphManager()
	{
		allCorrect = false;
		foreach (Transform t in grid)
			Destroy (t.gameObject);
	}

	public void GridButtonPressed ()
	{
		if (!allCorrect)
        {
            if (!hasEvaluated)
            {
                int questions = 0;
                int correct = 0;
                foreach (Transform t in grid)
                {
                    questions++;
                    SlotMB slotmb = t.GetComponent<SlotMB>();
                    if (slotmb.GetIsCorrect())
                    {
                        slotmb.QuestionIsCorrect();
                        correct++;
                    }
                    else
                    {
                        slotmb.DisplayIncorrectImage();
                    }
                }
                if (questions == correct)
                {
                    allCorrect = true;
                    gridButton.GetComponentInChildren<Text>().text = "Perfect!";
                }
                else
                {
                    gridButton.GetComponentInChildren<Text>().text = "Try Again";
                }
                hasEvaluated = true;
            }
            else  //if (hasEvaluated)
            {
                foreach (Transform t in grid)
                {
                    SlotMB slotmb = t.GetComponent<SlotMB>();
                    if (slotmb.GetIsCorrect())
                    {
                        continue;
                    }
                    else
                    {
                        slotmb.ClosePopupPressed();
                    }
                }
                gridButton.GetComponentInChildren<Text>().text = "Submit";
                hasEvaluated = false;
            }

		} 
		else 
		{
			gameflow.AnsweredGraphCorrectly ();
		}

//		Debug.Log (string.Format ("Score is {0}/{1}", correct, questions));
	}

	public void AddSlot(Slot slot)
	{
		GameObject go = Instantiate (Resources.Load (slotmbPrefab)) as GameObject;
		go.GetComponent<SlotMB> ().InitializeSlot (slot);
		go.transform.SetParent (grid);
	}
}
