using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class ScoreScreenManager : MonoBehaviour {

    public static string nextSceneName;

    public Transform textParent;

    public void Start()
    {
        SetScore();
    }

    public void SetScore()
    {
        foreach (Transform t in textParent)
        {
            float _value;
            if (t.name == "Total")
            {
                _value = ProgressTracker.Instance.GetTotalScore();
            }
            else
            {
                string[] splitName = t.name.Split('-');
                LessonType lessonType = (LessonType)System.Enum.Parse(typeof(LessonType), splitName[0], true);
                switch (splitName[1])
                {
                    case "GS":
                        _value = ProgressTracker.Instance.GetRoundScore(lessonType);
                        break;
                    case "QM":
                        _value = ProgressTracker.Instance.GetMultScore(lessonType);
                        break;
                    case "T":
                        _value = ProgressTracker.Instance.GetMultScore(lessonType) * ProgressTracker.Instance.GetRoundScore(lessonType);
                        break;
                    default:
                        Debug.Log("you done goofed");
                        _value = 0;
                        break;
                }
            }
            t.GetComponent<Text>().text = _value.ToString();
        }
    }

    public void NextButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
