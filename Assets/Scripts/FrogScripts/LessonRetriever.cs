using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////////////////
/// If altering the number of lessons, you must go into the respective Gameflow class (ex FrogGF) and chance the value  introLessons/outroLessons in StartFlow()
/// 535 characters per lesson max
/////////////////////////////////////////////////////

public enum LessonType { Frog, Fish, Bower, Duck, Caribou, Post, Intro }
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

    private string RetFrogLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0:
                return " In this game, you will play as a male spadefoot toad. Spadefoot toads live in arid places, and spend much of their time hibernating underground. When a heavy rainfall occurs, the toads emerge and seek out puddles of rainwater. When they find water, the male toads begin producing their mating calls. By the time the females arrive, there can be thousands of toads assembled around single pools of water. For one night, the toads engage in what is known as explosive breeding.";
            case 1:
                return "Each male competes with the others to mate with as many females as possible. Speed is the most important factor here, because only the tadpoles who have matured by the time the puddle dries up will survive. Female spadefoot toads can lay clutches of thousands of eggs, but most will never become adult frogs. Instead of providing parental care, male spadefoot toads focus their energies on maximizing their mating. Suitable mating conditions may not come again for multiple years.";
            case 2:
                return "You have just found a group of fresh puddles following a rare desert thunderstorm. Many other toads have also arrived. But beware: the noisy toads have attracted some hungry predators! The more living descendants you have when the puddles dry up, the higher your score will be.";
            case 3:
                return "Spadefoot toads are considered an opportunistic r species. They have large amounts of offspring, but few survive. They have short life expectancy, and the young mature very quickly. They do not protect their offspring, but rather focus on having as many as possible. That way, it is very like that at least some of them will reach adulthood. A male spadefoot toad who does not take full advantage of the rare explosive breeding event is likely to have far fewer descendants than his rivals. ";
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
}
