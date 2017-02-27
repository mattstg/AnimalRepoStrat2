﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using LoLSDK;

public class Tutorial : MonoBehaviour {

    GameFlow gameFlow;
    public Image image;

    void Awake()
    {
        gameFlow = GameObject.FindObjectOfType<GameFlow>();
    }

    public void Next()
    {
        gameFlow.NextTutorialButtonPressed();
    }

    public void DisplayTutorialImage(LessonType lessonType, int currentTut)
    {
        image.sprite = TutorialRetriever.Instance.GetTutorialImage(lessonType, currentTut);
    }
}
