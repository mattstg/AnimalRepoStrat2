using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class IntroGF : GameFlow {

    protected override void StartFlow()
    {
        introLessons = 5;
        outroLessons = 1;
        lessonType = LessonType.Intro;
        nextSceneName = "FrogScene";
        stage = -1;
        nextStep = true;
    }

    public override void Update()
    {
        //if(audioLooper != null)
         //   audioLooper.Update();
        if (nextStep)
        {
            nextStep = false;
            stage++;
            switch (stage)
            {
                case 0:
                    //StartMusic(); already playing from main menu scene
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

    protected override void GoToNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FrogScene");
    }

}
