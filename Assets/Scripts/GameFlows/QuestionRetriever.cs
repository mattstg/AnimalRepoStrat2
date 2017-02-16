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
        s1.SetAns(1, "R-Species", false);
        s1.SetAns(2, "K-Species", true);

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
        gm.titleText.text = "Frog Quiz";

        Slot s1 = new Slot();
        s1.SetQuestion("Does the Frog have more of a R or K extreme of a reproductive strategy?", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s1.SetAns(1, "R-Species", true);
        s1.SetAns(2, "K-Species", false);

        Slot s2 = new Slot();
        s2.SetQuestion("Does the frog have a high or low mortality rate?", "Mortality rate is how often a member of the species will persih.");
        s2.SetAns(1, "High", true);
        s2.SetAns(2, "Low", false);

        Slot s3 = new Slot();
        s3.SetQuestion("Do spadefoot frogs take care of their children?", "Do spadefoot frogs actively protect or nourish their young?");
        s3.SetAns(1, "Yes", false);
        s3.SetAns(2, "No", true);

        Slot s4 = new Slot();
        s4.SetQuestion("Male frogs will sing to let female frogs know that there is a good habitat to lay their eggs. Which type of reproductive readiness signal is this?", "");
        s4.SetAns(1, "Visual", false);
        s4.SetAns(2, "Auditory", true);
        s4.SetAns(3, "Olfactory", false);
        s4.SetAns(4, "Courtship", false);

        Slot s5 = new Slot();
        s5.SetQuestion("Is this frog an example of an opportunistic species?", "An opportunistic species reproduce during brief windows of time");
        s5.SetAns(1, "Yes", true);
        s5.SetAns(2, "No", false);

        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s4);
        gm.AddSlot(s5);
    }

    private void SetupFishQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Salmon Run";

        Slot s1 = new Slot();
        s1.SetQuestion("Which kind of reproductive extreme does the Salmon exhibit?", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s1.SetAns(1, "R-Species", true);
        s1.SetAns(2, "K-Species", false);

        Slot s2 = new Slot();
        s2.SetQuestion("Do salmon carefully choose their partners?", "Do salmon participate in selective breeding?");
        s2.SetAns(1, "Yes", false);
        s2.SetAns(2, "No", true);

        Slot s3 = new Slot();
        s3.SetQuestion("Do salmon preform mating dances?", "Do salmon dance to impress other salmon?");
        s3.SetAns(1, "Yes", false);
        s3.SetAns(2, "No", true);

        Slot s4 = new Slot();
        s4.SetQuestion("Do salmon take care of their young?", "Do salmon protect or nourish their young?");
        s4.SetAns(1, "Yes", false);
        s4.SetAns(2, "No", true);

        Slot s5 = new Slot();
        s5.SetQuestion("How do animals pass along their genetic information?", "What is required to make young?");
        s5.SetAns(1, "Through hibernation", false);
        s5.SetAns(1, "Through migration", false);
        s5.SetAns(1, "Through reproduction", true);
        s5.SetAns(1, "Through photosynthesis", false);

        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s4);
        gm.AddSlot(s5);
    }

    private void SetupBowerQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Bower birds";

        Slot s0 = new Slot();
        s0.SetQuestion("Which kind of reproductive extreme does the Bower exhibit?", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s0.SetAns(1, "R-Species", false);
        s0.SetAns(2, "K-Species", true);

        Slot s1 = new Slot();
        s1.SetQuestion("Why do animal some animals carefully select a mate?", "Why are some animals picky?");
        s1.SetAns(1, "They don't", false);
        s1.SetAns(2, "So their offspring will gain desirable traits", true);
        s1.SetAns(3, "So they will gain desirable traits", false);

        Slot s2 = new Slot();
        s2.SetQuestion("Some male birds perform elaborate dances to attract females. Why do females have the males dance?", "What might she be looking for");
        s2.SetAns(1, "To prepare the ground for a nest", false);
        s2.SetAns(2, "To engage in asexual reproduction", false);
        s2.SetAns(3, "To frighten potential predators", false);
        s2.SetAns(4, "To increase the chances of successful offspring", true);

        Slot s3 = new Slot();
        s3.SetQuestion("Whats other strategy does the Bower Bird use to attract females?", "Other than dancing, what does she look for");
        s3.SetAns(1, "By migrating south", false);
        s3.SetAns(2, "Building a Bower nest", true);
        s3.SetAns(3, "Finding fish in a nearby lake", false);
        s3.SetAns(4, "By digging a deep hole", false);

        Slot s4 = new Slot();
        s4.SetQuestion("The bower bird dancing is an example of ", "The bower dances at the nest to impress the female");
        s4.SetAns(1, "Courtship behavior", true);
        s4.SetAns(2, "Persuasion behavior", false);
        s4.SetAns(3, "Visual behavior", false);
        s4.SetAns(4, "Copulation behavior", false);

        gm.AddSlot(s0);
        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s4);
    }

    private void SetupDuckQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Wood Ducks";

        Slot s0 = new Slot();
        s0.SetQuestion("Which kind of reproductive extreme does the Duck exhibit?", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s0.SetAns(1, "R-Species", false);
        s0.SetAns(2, "K-Species", false);
        s0.SetAns(3, "Middle", true);

        Slot s1 = new Slot();
        s1.SetQuestion("How does the mother Wood Duck protect its eggs?", "What behaviour best protected the eggs");
        s1.SetAns(1, "Hiding them in the lake", false);
        s1.SetAns(2, "Hiding them in a nest high in a tree", true);
        s1.SetAns(3, "Herding them away from predators", false);

        Slot s2 = new Slot();
        s2.SetQuestion("How does a mother Wood Duck help its ducklings after they have hatched?", "How did she help them reach water");
        s2.SetAns(1, "Courtship dance", false);
        s2.SetAns(2, "Mating call", false);
        s2.SetAns(3, "Herding them away from predators", true);
        s2.SetAns(4, "Teaching them to fly", false);

        Slot s3 = new Slot();
        s3.SetQuestion("Does the Wood Duck produce few or many offspring?", "The game depicted a roughly normal batch");
        s3.SetAns(1, "less than a 2 or 3", false);
        s3.SetAns(2, "around a dozen", true);
        s3.SetAns(3, "about as much as a frog", false);

        Slot s4 = new Slot();
        s4.SetQuestion("What level of parental investment does the Wood Duck show?", "Do they raise the young, abandon them, help them a little?");
        s4.SetAns(1, "None", false);
        s4.SetAns(2, "somewhere in the middle", true);
        s4.SetAns(3, "alot", false);

        Slot s5 = new Slot();
        s5.SetQuestion("Why does the Wood Duck build a nest high in the trees? ", "What benefit does nesting have?");
        s5.SetAns(1, "To protect its eggs from predators", true);
        s5.SetAns(2, "To attract a mate", false);
        s5.SetAns(3, "To have somewhere to sleep", false);

        gm.AddSlot(s0);
        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s4);
        gm.AddSlot(s5);
    }

    private void SetupCaribouQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Caribou Migration";

        Slot s0 = new Slot();
        s0.SetQuestion("Usually how many calfs does a female Caribou have per year?", "How many babies does a female caribou have per year?");
        s0.SetAns(1, "One", true);
        s0.SetAns(2, "Five", false);
        s0.SetAns(3, "Fifty", false);

        Slot s1 = new Slot();
        s1.SetQuestion("Which kind of reproductive extreme does the Caribou exhibit?", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s1.SetAns(1, "R-species", false);
        s1.SetAns(2, "Middle", false);
        s1.SetAns(3, "K-species", true);

        Slot s2 = new Slot();
        s2.SetQuestion("True of False: a caribou calf does not need the protection of a herd.", "How is the baby protected, by itself, individuals, or herds");
        s2.SetAns(1, "True", false);
        s2.SetAns(2, "False", true);

        Slot s4 = new Slot();
        s4.SetQuestion("Behaviour preformed by the caribou to protect young", "The behaviour shown in game");
        s4.SetAns(1, "Courtship dance", false);
        s4.SetAns(2, "Mating Call", false);
        s4.SetAns(3, "Herding", true);
        s4.SetAns(4, "Nest Building", false);


        gm.AddSlot(s0);
        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s4);
    }

    private void SetupFinalQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Bid for life";
        

        Slot s1 = new Slot();
        s1.SetQuestion("What best desribes the strategy of... Producing many offspring?", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s1.SetAns(1, "K-species", false);
        s1.SetAns(2, "R-species", true);
        s1.SetAns(3, "Both or Independant behaviour", false);

        Slot s2 = new Slot();
        s2.SetQuestion("Slow sexual maturity", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s2.SetAns(1, "K-species", true);
        s2.SetAns(2, "R-species", false);
        s2.SetAns(3, "Both or Independant behaviour", false);

        Slot s3 = new Slot();
        s3.SetQuestion("High parental investment", "Select if this trait is more of a reproductive trait for R or K extreme species");
        s3.SetAns(1, "K-species", true);
        s3.SetAns(2, "R-species", false);
        s3.SetAns(3, "Both or Independant behaviour", false);

        Slot s5 = new Slot();
        s5.SetQuestion("Drawback of no parental care", "What negative comes from no parental care");
        s5.SetAns(1, "Lower mortality rates", false);
        s5.SetAns(2, "Higher mortality rates", true);


        Slot s7 = new Slot();
        s7.SetQuestion("Which species mentioned is opportunistic?", "Which ones mate during brief windows");
        s7.SetAns(1, "Spade foot-toad", true);
        s7.SetAns(2, "Wood duck", false);
        s7.SetAns(3, "Baren ground caribou", false);


        Slot s8 = new Slot();
        s8.SetQuestion("Nest building is an example of what kind of mating strategy", "Which category does it fall under");
        s8.SetAns(1, "Protecting young", true);
        s8.SetAns(2, "Herding", false);
        s8.SetAns(3, "Mating Ritual", false);

        Slot s9 = new Slot();
        s9.SetQuestion("The main clues by which organisms show their readiness to reproduce are", "How did animals in the games let others know they were ready to mate");
        s9.SetAns(1, "Color, shape, and size", false);
        s9.SetAns(2, "Mimicry, commensalism, and parasitism", false);
        s9.SetAns(3, "Visual, auditory, and olfactory.", true);
        s9.SetAns(4, "Both A and B are correct", false);


        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s5);
        gm.AddSlot(s7);
        gm.AddSlot(s8);
        gm.AddSlot(s9);
    }
}
