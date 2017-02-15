using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionRetriever : MonoBehaviour {


    #region Singleton
    private static QuestionRetriever instance;

    private QuestionRetriever() { }

    public static QuestionRetriever Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QuestionRetriever();
            }
            return instance;
        }
    }
    #endregion

    public void SetupQuestions(GraphManager gm, LessonType lessonType)
    {
        switch (lessonType)
        {
            case LessonType.Intro:
                SetupIntroQuestions(gm);
                break;
            case LessonType.Frog:
                SetupFrogQuestions(gm);
                break;
            case LessonType.Fish:
                SetupFishQuestions(gm);
                break;
            case LessonType.Bower:
                SetupBowerQuestions(gm);
                break;
            case LessonType.Duck:
                SetupDuckQuestions(gm);
                break;
            case LessonType.Caribou:
                SetupCaribouQuestions(gm);
                break;
            case LessonType.Post:
                SetupFinalQuestions(gm);
                break;
        }
    }

    private void SetupIntroQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "What aspects of reproductive whatever";

        Slot s1 = new Slot();
        s1.SetQuestion("Body size", "Select what size frogs are relative to most animals in the animal kingdom");
        s1.SetAns(1, "Small", true, "F");
        s1.SetAns(2, "Meduim", false, "What? You think they jump to the sky?");
        s1.SetAns(3, "Large", false, "Ya.. no comment, id say try again but I don't see that would help");

        Slot s2 = new Slot();
        s2.SetQuestion("Dinosaurs became birds", "True or false, simple shit dont strain your brain");
        s2.SetAns(1, "True", true, "Correct: Dinosaurs hyper evolved when the astroid fused atoms in unstable isotopes");
        s2.SetAns(2, "False", false, "You can't handle the truth");

        gm.AddSlot(s1);
        gm.AddSlot(s2);
    }

    private void SetupFrogQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "What aspects of reproductive whatever";

        Slot s1 = new Slot();
        s1.SetQuestion("Body size", "Select what size frogs are relative to most animals in the animal kingdom");
        s1.SetAns(1, "Small", true, "F");
        s1.SetAns(2, "Meduim", false, "What? You think they jump to the sky?");
        s1.SetAns(3, "Large", false, "Ya.. no comment, id say try again but I don't see that would help");

        Slot s2 = new Slot();
        s2.SetQuestion("Dinosaurs became birds", "True or false, simple shit dont strain your brain");
        s2.SetAns(1, "True", true, "Correct: Dinosaurs hyper evolved when the astroid fused atoms in unstable isotopes");
        s2.SetAns(2, "False", false, "You can't handle the truth");

        gm.AddSlot(s1);
        gm.AddSlot(s2);
    }

    private void SetupFishQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "What aspects of reproductive whatever";

        Slot s1 = new Slot();
        s1.SetQuestion("Body size", "Select what size frogs are relative to most animals in the animal kingdom");
        s1.SetAns(1, "Small", true, "F");
        s1.SetAns(2, "Meduim", false, "What? You think they jump to the sky?");
        s1.SetAns(3, "Large", false, "Ya.. no comment, id say try again but I don't see that would help");

        Slot s2 = new Slot();
        s2.SetQuestion("Dinosaurs became birds", "True or false, simple shit dont strain your brain");
        s2.SetAns(1, "True", true, "Correct: Dinosaurs hyper evolved when the astroid fused atoms in unstable isotopes");
        s2.SetAns(2, "False", false, "You can't handle the truth");

        gm.AddSlot(s1);
        gm.AddSlot(s2);
    }

    private void SetupBowerQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "What aspects of reproductive whatever";

        Slot s1 = new Slot();
        s1.SetQuestion("Body size", "Select what size frogs are relative to most animals in the animal kingdom");
        s1.SetAns(1, "Small", true, "F");
        s1.SetAns(2, "Meduim", false, "What? You think they jump to the sky?");
        s1.SetAns(3, "Large", false, "Ya.. no comment, id say try again but I don't see that would help");

        Slot s2 = new Slot();
        s2.SetQuestion("Dinosaurs became birds", "True or false, simple shit dont strain your brain");
        s2.SetAns(1, "True", true, "Correct: Dinosaurs hyper evolved when the astroid fused atoms in unstable isotopes");
        s2.SetAns(2, "False", false, "You can't handle the truth");

        gm.AddSlot(s1);
        gm.AddSlot(s2);
    }

    private void SetupDuckQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "What aspects of reproductive whatever";

        Slot s1 = new Slot();
        s1.SetQuestion("Body size", "Select what size frogs are relative to most animals in the animal kingdom");
        s1.SetAns(1, "Small", true, "F");
        s1.SetAns(2, "Meduim", false, "What? You think they jump to the sky?");
        s1.SetAns(3, "Large", false, "Ya.. no comment, id say try again but I don't see that would help");

        Slot s2 = new Slot();
        s2.SetQuestion("Dinosaurs became birds", "True or false, simple shit dont strain your brain");
        s2.SetAns(1, "True", true, "Correct: Dinosaurs hyper evolved when the astroid fused atoms in unstable isotopes");
        s2.SetAns(2, "False", false, "You can't handle the truth");

        gm.AddSlot(s1);
        gm.AddSlot(s2);
    }

    private void SetupCaribouQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "What aspects of reproductive whatever";

        Slot s1 = new Slot();
        s1.SetQuestion("Body size", "Select what size frogs are relative to most animals in the animal kingdom");
        s1.SetAns(1, "Small", true, "F");
        s1.SetAns(2, "Meduim", false, "What? You think they jump to the sky?");
        s1.SetAns(3, "Large", false, "Ya.. no comment, id say try again but I don't see that would help");

        Slot s2 = new Slot();
        s2.SetQuestion("Dinosaurs became birds", "True or false, simple shit dont strain your brain");
        s2.SetAns(1, "True", true, "Correct: Dinosaurs hyper evolved when the astroid fused atoms in unstable isotopes");
        s2.SetAns(2, "False", false, "You can't handle the truth");

        gm.AddSlot(s1);
        gm.AddSlot(s2);
    }

    private void SetupFinalQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "What aspects of reproductive whatever";

        Slot s1 = new Slot();
        s1.SetQuestion("Body size", "Select what size frogs are relative to most animals in the animal kingdom");
        s1.SetAns(1, "Small", true, "F");
        s1.SetAns(2, "Meduim", false, "What? You think they jump to the sky?");
        s1.SetAns(3, "Large", false, "Ya.. no comment, id say try again but I don't see that would help");

        Slot s2 = new Slot();
        s2.SetQuestion("Dinosaurs became birds", "True or false, simple shit dont strain your brain");
        s2.SetAns(1, "True", true, "Correct: Dinosaurs hyper evolved when the astroid fused atoms in unstable isotopes");
        s2.SetAns(2, "False", false, "You can't handle the truth");

        gm.AddSlot(s1);
        gm.AddSlot(s2);
    }
}
