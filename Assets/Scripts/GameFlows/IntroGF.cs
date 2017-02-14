using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class IntroGF : GameFlow {

    protected override void StartFlow()
    {
        introLessons = 2;
        outroLessons = 3;
        lessonType = LessonType.Intro;
        nextSceneName = "FrogScene";
        stage = -1;
        nextStep = true;
    }

    public override void Update()
    {
        audioLooper.Update();
        if (nextStep)
        {
            nextStep = false;
            stage++;
            switch (stage)
            {
                case 0:
                    StartMusic();
                    DisplayLesson();
                    break;
                case 1:
                    PostGameQuestions(); //summary questions
                    break;
                case 2:
                    DisplayLesson();
                    break;
                case 3:
                    CloseMusic();
                    GoToNextScene();
                    break;
                default:
                    break;
            }
        }
    }

}
