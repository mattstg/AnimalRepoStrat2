using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMB : MonoBehaviour {

	public Sprite unpressedGraphic;
	public Sprite pressedGraphic;

    public Sprite infoDefault;
    public Sprite infoSelected;
    public Sprite infoCorrect;
    public Sprite infoIncorrect;
    public Button infoButton;

    Slot slot;
	public List<Button> buttons = new List<Button> ();
	public Image popupImage;

	int selectedAns = 0;
    bool isExplaining = false;
    bool isEvaluating = false;

	public void InitializeSlot(Slot _slot)
	{
		slot = _slot;
		buttons [0].GetComponentInChildren<Text> ().text = _slot.questiontext;
		RefreshSlot ();
	}

    public void SetIsExplaining(bool isExp)
    {
        if (isExp)
        {
            isExplaining = true;
            infoButton.GetComponent<Image>().sprite = infoSelected;
        }
        else
        {
            isExplaining = false;
            infoButton.GetComponent<Image>().sprite = infoDefault;
        }
    }

    public void SetIsEvaluating(bool isEval)
    {
        if (isEval)
        {
            isEvaluating = true;
            if (GetIsCorrect())
            {
                infoButton.interactable = false;
                infoButton.GetComponent<Image>().sprite = infoCorrect;
            }
            else
            {
                infoButton.interactable = false;
                infoButton.GetComponent<Image>().sprite = infoIncorrect;
            }
        }
        else if (!isEval)
        {
            isEvaluating = false;
            infoButton.interactable = true;
            infoButton.GetComponent<Image>().sprite = infoDefault;
        }
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
        SetIsExplaining(false);
        SetIsEvaluating(true);
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
        SetIsExplaining(false);
        SetIsEvaluating(false);
    }

	public void ClosePopupPressed()
	{
        popupImage.gameObject.SetActive(false);
        RefreshSlot();
	}

	public void ButtonPressed(int butID)
	{
		if (butID == 0 ) {
            if (isEvaluating)
                return;
            if (!isExplaining) {
                QuestionExplainedPressed();
                return;
            } else {
                ClosePopupPressed();
                return;
            }
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
        SetIsExplaining(false);
        SetIsEvaluating(true);
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
        SetIsExplaining(true);
	}
}
