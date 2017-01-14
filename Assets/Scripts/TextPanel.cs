using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TextPanel : MonoBehaviour {

	public GameFlow gameflow;
	public Button nextButton;

    public Text text;
    string completeText;
    public float lettersPerSecond = 5;
    bool started = false;
    int curLetter;
    float timeBanked = 0;

    public void SetText(string _completeText)
    {
        completeText = _completeText;
        curLetter = 0;
        text.text = "";
        started = false;
        timeBanked = 0;
    }

    public void StartWriting()
    {
		nextButton.gameObject.SetActive(false);
        started = true;
    }

    public void Update()
    {
        if (!started)
            return;

        timeBanked++;
        if (curLetter < completeText.Length)
        {
            if (timeBanked >= 1 / lettersPerSecond)
            {
                timeBanked -= (1 / lettersPerSecond);
                text.text += completeText[curLetter++];
            }
        }
        else
        {
            started = false; //reached end
			nextButton.gameObject.SetActive(true);
        }
    }

	public void NextPressed()
	{
		gameflow.TextButtonNextPressed ();
	}

}
