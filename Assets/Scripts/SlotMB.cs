using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMB : MonoBehaviour {

	public Sprite unpressedGraphic;
	public Sprite pressedGraphic;

	Slot slot;
	public List<Button> buttons = new List<Button> ();
	public Image popupImage;
	int selectedAns = 0;

	public void InitializeSlot(Slot _slot)
	{
		slot = _slot;
		buttons [0].GetComponentInChildren<Text> ().text = _slot.questiontext;
		RefreshSlot ();
	}

	public bool GetIsCorrect()
	{
		return slot.IsCorrectAns (selectedAns);
	}

	public void DisplayIncorrectImage()
	{
		foreach (Button b in buttons)
            b.transform.parent.gameObject.SetActive(false);
            //b.gameObject.SetActive (false);
        buttons[0].transform.parent.gameObject.SetActive(true);
        popupImage.gameObject.SetActive (true);
		popupImage.GetComponentInChildren<Text> ().text = slot.GetWrongPopup (selectedAns);
	}

	public void RefreshSlot()
	{
		buttons [0].gameObject.SetActive (true);
		foreach (KeyValuePair<int,Slot.SlotInfo> kv in slot.slotInfoDict) {
			Button but = buttons [kv.Key];
			but.GetComponentInChildren<Text> ().text = kv.Value.text;
            but.transform.parent.gameObject.SetActive(true);
            //but.gameObject.SetActive (true);
		}
	}

	public void ClosePopupPressed()
	{
        popupImage.gameObject.SetActive(false);
        RefreshSlot();
	}

	public void ButtonPressed(int butID)
	{
		if (butID == 0) {
			QuestionExplainedPressed ();
			return;
		}

		selectedAns = butID;
		for(int i = 1; i < 5; i++){
			if (selectedAns == i)
				buttons [i].GetComponent<Image> ().sprite = pressedGraphic;
			else
				buttons [i].GetComponent<Image> ().sprite = unpressedGraphic;
				
		}
	}

	public void QuestionIsCorrect()
	{
		foreach (Button b in buttons)
            b.transform.parent.gameObject.SetActive(false);
            //b.gameObject.SetActive (false);
        buttons[0].transform.parent.gameObject.SetActive(true);
                popupImage.gameObject.SetActive (true);
		popupImage.GetComponentInChildren<Text> ().text = slot.slotInfoDict[selectedAns].popupText;
		if(popupImage.GetComponentInChildren<Button> ())
			popupImage.GetComponentInChildren<Button> ().gameObject.SetActive (false);
	}

	public void QuestionExplainedPressed()
	{
        foreach (Button b in buttons)
        {
            b.transform.parent.gameObject.SetActive(false);
//            Debug.Log("p name " + b.transform.parent.name);
                }
            //b.gameObject.SetActive (false);
        buttons[0].transform.parent.gameObject.SetActive(true);
		popupImage.gameObject.SetActive (true);
		popupImage.GetComponentInChildren<Text> ().text = slot.questionExplained;
	}
}
