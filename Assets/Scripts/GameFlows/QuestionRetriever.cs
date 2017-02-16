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
        gm.titleText.text = "Animals with this trait tend to be on which extreme of common reproductive strategies";

        Slot s1 = new Slot();
        s1.SetQuestion("Big body size", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s1.SetAns(1, "R-Species", true);
        s1.SetAns(2, "K-Species", false);

        Slot s2 = new Slot();
        s2.SetQuestion("High number of offspring", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s2.SetAns(1, "R-Species", true);
        s2.SetAns(2, "K-Species", false);

        Slot s3 = new Slot();
        s3.SetQuestion("Care for young", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s3.SetAns(1, "R-Species", false);
        s3.SetAns(2, "K-Species", true);

        Slot s4 = new Slot();
        s4.SetQuestion("High infant mortality rate", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s4.SetAns(1, "R-Species", true);
        s4.SetAns(2, "K-Species", false);

        Slot s5 = new Slot();
        s5.SetQuestion("Early dependance on parents", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s5.SetAns(1, "R-Species", false);
        s5.SetAns(2, "K-Species", true);

        Slot s6 = new Slot();
        s6.SetQuestion("Low dependance on parents", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s6.SetAns(1, "R-Species", true);
        s6.SetAns(2, "K-Species", false);

        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s4);
        gm.AddSlot(s5);
        gm.AddSlot(s6);
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
