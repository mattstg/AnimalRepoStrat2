using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////////////////
/// If altering the number of lessons, you must go into the respective Gameflow class (ex FrogGF) and chance the value  introLessons/outroLessons in StartFlow()
/////////////////////////////////////////////////////

public enum LessonType { Intro, Frog, Fish, Bower, Duck, Caribou, Post }
public class LessonRetriever  {

    #region Singleton
    private static LessonRetriever instance;

    private LessonRetriever() { }

    public static LessonRetriever Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LessonRetriever();
            }
            return instance;
        }
    }
    #endregion

    public string RetrieveLesson(LessonType lessonType, int lessonNumber)
    {
        switch(lessonType)
        {
            case LessonType.Intro:
                return RetIntroLesson(lessonNumber);
            case LessonType.Frog:
                return RetFrogLesson(lessonNumber);
            case LessonType.Fish:
                return RetFishLesson(lessonNumber);
            case LessonType.Bower:
                return RetBowerLesson(lessonNumber);
            case LessonType.Duck:
                return RetDuckLesson(lessonNumber);
            case LessonType.Caribou:
                return RetCaribouLesson(lessonNumber);
            case LessonType.Post:
                return RetPostLesson(lessonNumber);
        }
        return "";
    }

    private string RetIntroLesson(int lessonNumber)
    {
        switch(lessonNumber)
        {
            case 0:
                return "";
            case 1:
                return "";
            case 2:
                return "";
            case 3:
                return "";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }

    private string RetFrogLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0:
                return "this is the pre game";
            case 1:
                return "This is the pre game 2";
            case 2:
                return "this is the post game";
            case 3:
                return "this is the post game2";
            case 4:
                return "this is the post game3";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }

    private string RetFishLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0:
                return "this is the pre game";
            case 1:
                return "This is the pre game 2";
            case 2:
                return "this is the post game";
            case 3:
                return "this is the post game2";
            case 4:
                return "this is the post game3";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }

    private string RetBowerLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0:
                return "this is the pre game";
            case 1:
                return "This is the pre game 2";
            case 2:
                return "this is the post game";
            case 3:
                return "this is the post game2";
            case 4:
                return "this is the post game3";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }

    private string RetDuckLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0:
                return "this is the pre game";
            case 1:
                return "This is the pre game 2";
            case 2:
                return "this is the post game";
            case 3:
                return "this is the post game2";
            case 4:
                return "this is the post game3";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }

    private string RetCaribouLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0:
                return "this is the pre game";
            case 1:
                return "This is the pre game 2";
            case 2:
                return "this is the post game";
            case 3:
                return "this is the post game2";
            case 4:
                return "this is the post game3";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }

    private string RetPostLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0:
                return "";
            case 1:
                return "";
            case 2:
                return "";
            case 3:
                return "";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }
}
