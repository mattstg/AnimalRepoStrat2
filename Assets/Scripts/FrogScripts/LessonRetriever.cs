using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////////////////
/// If altering the number of lessons, you must go into the respective Gameflow class (ex FrogGF) and chance the value  introLessons/outroLessons in StartFlow()
/// 535 characters per lesson max
/////////////////////////////////////////////////////

public enum LessonType { Frog, Fish, Bower, Duck, Caribou, Post, Intro }
public class LessonRetriever
{

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
        switch (lessonType)
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
        switch (lessonNumber)
        {
            case 0: //1
                return "<b>Bid For Life</b> is a game about <b>reproductive strategies in the wild</b>.\n\n" +
                    "All animal species use a variety of behaviors in order to <b>increase their chances of parenting successful offspring</b>.\n\n" +
                    "Such strategies involve, for example, having very <b>few offspring</b> and spending <b>a lot of time and energy</b> on caring for those offspring, or having very <b>many</b> offspring while spending <b>little or no energy</b> caring for them. Some species perform <b>mating rituals</b>, such as <b>dancing</b>, <b>sparring</b>, or displaying <b>reproductive readiness signals</b>.\n\n" +
                    "Different species use <b>different combinations of strategies</b> like these in order to achieve <b>reproductive success</b> within their environments.";
            case 1:
                return "Some species tend to have <b>many offspring</b> and provide <b>little</b> or <b>no parental care</b> for their young. These species tend to have <b>high infant mortality rates</b>, but they often thrive due to their <b>high birth rates</b>.\n\n" +
                    "These species are known as <b><i>r</i>-species</b>, and include many <b>fish</b> and <b>insects</b>. They tend to be <b>small, grow quickly,</b> and live in <b>unstable</b> or <b>dynamic environments</b>.";
            case 2://2
                return "At the other extreme live animals who tend to have <b>very few offspring</b>, but who <b>invest large amounts of energy</b> into <b>raising</b> and <b>protecting</b> those offspring.\n\n" +
                    "These species are known as <b><i>K</i>-species</b>, and include animals such as <b>elephants, bears,</b> and <b>humans</b>. While these species tend to produce few young, their <b>high degree of parental care</b> makes it more likely that those young will <b>survive</b>.\n\n" +
                    "<b><i>K</i>-species</b> tend to be <b>large,</b> have <b>low mortality rates, grow slowly,</b> and remain <b>dependent upon parents</b> for <b>longer periods</b>.";
            case 3://3
                return "In reality, <b><i>r</i></b> and <b><i>K</i></b> do not represent two categories of animals. Rather, <b><i>r</i></b> and <b><i>K</i></b> are the <b>two extremes in a range of animal behaviors</b>, and individual species can find themselves anywhere in that range.\n\n" +
                    "Keep in mind that <b><i>r</i></b> and <b><i>K</i></b> are just <b>concepts</b> that people use to help <b>describe</b> and <b>compare</b> the many <b>different reproductive strategies</b> found throughout the animal kingdom. Many animals fall <b>somewhere in the middle</b>, and some have traits from <b>both extremes</b> of the <b><i>r</i>–<i>K</i></b> range.";
            case 4://4
                return "Many species of animals also engage in <b>specific behaviors</b> and <b>mating rituals</b> aimed at <b>attracting mates</b>.\n\n" +
                    "Typically, these rituals are <b>a show of desirable traits</b>, such as healthy feathers, strength, or a strong voice. Selecting a mate with those qualities will <b>increase the probability</b> that <b>an animal's offspring will have those same desirable traits themselves</b>, giving those offspring a high chance of <b>survival</b> and <b>reproductive success</b>.\n\n" +
                    "Other behaviors, such as <b>herding</b> or <b>nesting</b>, are aimed at <b>protecting the young</b>.";
            case 5://outro1
                return "<b>Bid For Life</b> is made up of <b>five mini-games</b> in which you will play as a member of <b>five different animal species</b>.\n\n" +
                    "Make sure you pay attention to the <b>short lessons</b> before and after each game, because you can <b>boost your score</b> by doing well on the <b>quizzes</b>!\n\n" +
                    "Have fun!";
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
                return "In this game, you will play as a <b>male spadefoot toad</b>.\n\n" +
                    "Spadefoot toads live in arid places, and spend much of their time hibernating underground. When a heavy rainfall occurs, the toads emerge and seek out puddles of rainwater.\n\n" +
                    "When they find water, the male toads begin producing their mating calls. By the time the females arrive, there can be thousands of toads assembled around single pools of water.\n\n" +
                    "For one single night, the toads engage in what is known as <b>explosive breeding</b>.";
            case 1:
                return "Each male competes with the others to mate with as many females as possible.\n\n" +
                    "Speed is the most important factor here, because only the tadpoles who have matured by the time the puddle dries up will survive. Female spadefoot toads can lay clutches of thousands of eggs, but most will never become adult toads.\n\n" +
                    "Instead of providing parental care, male spadefoot toads focus their energies on maximizing their mating. Suitable mating conditions may not come again for multiple years.";
            case 2:
                return "You have just found a fresh puddle following a rare desert thunderstorm. Many other toads have also arrived.\n\n" +
                    "But beware: the noisy toads have attracted some hungry predators!\n\n" +
                    "The more living descendants you have when the puddle dries up, the higher your score will be.";
            case 3:
                return "Spadefoot toads are considered an opportunistic <i>r</i>-species.\n\n" +
                    "They have large amounts of offspring, but few survive. They have short life expectancy, and their young mature very quickly. They do not protect their offspring, but rather focus on having as many as possible. That way, it is very likely that at least some of them will reach adulthood.\n\n" +
                    "A male spadefoot toad who does not take full advantage of the rare explosive breeding event is likely to have far fewer descendants than his rivals.";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }

    private string RetFishLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0: //intro1
                return "In this game, you will play as a <b>pacific salmon</b>.\n\n" +
                    "Salmon are hatched in freshwater rivers, but they eventually make their way out to the saltwater ocean, where they will spend most of their lives.\n\n" +
                    "Once they reach sexual maturity, they engage in one of nature’s spectacular events: the <i>salmon run</i>. Each year, millions of salmon return to the rivers where they were born, and swim hundreds of kilometers upstream to the pools where they spawned.\n\n" +
                    "Along the way, many are caught by predators (especially grizzly bears).";
            case 1://intro2
                return "The pacific salmon that succeed in making it to their spawning grounds mate there for the only time in their lives.\n\n" +
                    "They do not engage in selective mating. Rather, the females lay huge clusters of eggs on the riverbed, which the males then fertilize together.\n\n" +
                    "Shortly after mating, all pacific salmon die, and the life-cycle continues.\n\n" +
                    "Most of the young won’t survive to adulthood, but those who do will do so completely without their parents.";
            case 2://intro3
                return "It is autumn, and you have just arrived at the river mouth that leads to the pool where you once hatched. Alongside countless other salmon, you strike up against the current.\n\n" +
                    "Watch out for the grizzly bears! They wait for the salmon run all year, and are experts at fishing!\n\n" +
                    "The faster you reach your spawning ground, the higher your score will be.";
            case 3://outro1
                return "Like spadefoot toads, salmon are considered <i>r</i>-selected species. They give birth to huge amounts of young, and provide no parental care.\n\n" +
                    "The run up the river itself serves as a selection process for salmon. Only those that can survive the difficult, dangerous journey up to the spawning grounds get to mate and pass on their genes. For a salmon, completing the run safely is the secret to successful mating.";
            case 4://outro2
                return "Although it may seem bizarre and dangerous, the salmon run is a reproductive strategy that allows young salmon to grow up in the relative safety of freshwater pools.\n\n" +
                    "Though most offspring will die before they make it back out to the ocean, some will go on to run the river in their own time, and face the same bears and waterfalls that challenged their parents years before.";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }

    private string RetBowerLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0: //L1
                return "In this game you will play as a <b>male bowerbird</b>.\n\n" +
                    "Bowerbirds are considered a <i>K</i>-species. They usually lay one egg, which takes about three weeks to hatch. The female then raises her young for about two months. Some bowerbirds take up to eight years to mature.\n\n" +
                    "Bowerbirds are particularly famous for their mating rituals. Male bowerbirds create small structures made of twigs called <i>bowers</i>, and adorn them with decorations. These can include pebbles, shells, feathers, flowers, and berries. ";
            case 1: //L2
                return "Females who are interested in mating visit the bowers of multiple males, often returning to each one several times.\n\n" +
                    "Males try especially hard to find blue decorations, since blue is the favorite color of most female bowerbirds.\n\n" +
                    "Unlike spadefoot toads and salmon, female bowerbirds are extremely selective in their mating. Since they have so few offspring, they must ensure that the children they do have are as healthy and strong as possible. Therefore, it pays off to find the most impressive male in the neighborhood for a mate.";
            case 2: //L3
                return "It is mating season, and you have just completed your twig bower.\n\n" +
                    "You’ve spotted a female in the area who seems to be interested in finding a mate. However, a few rival males in the area seem to have taken notice as well.\n\n" +
                    "Outperform your rivals by collecting the most impressive assortment of decorations for your bower!\n\n" +
                    "To get a high score, make sure the female bowerbird chooses you over the others. The faster she chooses you, the higher the score!";
            case 3: //outro1
                return "Competition over female bowerbirds is fierce, and it is the main challenge males will face in their struggle to reproduce. Many of them fail to mate at all, unable to compete with their neighbors.\n\n" +
                    "While salmon are selected for mating by their ability to survive the run, male bowerbirds are selected for mating by their treasure-hunting and decorating skills.\n\n" +
                    "Since they have so few children, female bowerbirds must ensure that each one has a high chance of survival. Therefore, they select only the owners of the best bowers as mates.";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }

    private string RetDuckLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0: //intro1
                return "In this game you will play as a <b>female wood duck</b> with ten newly-hatched ducklings in your care.\n\n" +
                    "Wood ducks make their nests high up in trees, near large bodies of water. When wood duck hatchlings are a day old, they jump from the nest down to the forest floor (up to a twenty-meter drop!).\n\n" +
                    "The mother then leads her hatchlings to the nearest body of water.";
            case 1://intro2
                return "This is the most dangerous time in the young ducklings’ lives, for they are easy targets for predators. The mother does not defend her offspring during the march, but she repeatedly calls them to her as she leads the way.\n\n" +
                    "Though wood ducks have more offspring than many <i>K</i>-species, they have far fewer than salmon or toads, and about two thirds of them never reach adulthood.\n\n" +
                    "The mother duck provides moderate care for her young in the form of incubating the eggs, leading the ducklings to the water, and then raising them until they can fly.";
            case 2://into3
                return "It is early spring, and your hatchlings have just jumped to the forest floor.\n\n" +
                    "There is a large lake nearby to the south, but beware: this forest is a home for red foxes, and they would have no trouble catching a duckling if they see one.\n\n" +
                    "Call your young to you when they wander, and head for the open waters!\n\n" +
                    "The more ducklings you can safely lead to the lake, the higher your score will be.";
            case 3://outro1
                return "It is safe to say that wood ducks are somewhere in the middle between <i>r</i> and <i>K</i>. The ducklings are born in medium-sized clutches, mature at a medium pace, have a medium mortality rate, and receive a medium amount of parental care.\n\n" +
                    "Though the initial walk from the nest to the water is perilous, it allows wood ducks to lay their eggs in the relative safety of tall trees.\n\n" +
                    "As the ducklings who make it to the water mature, several of them will fall prey to snakes, turtles, bass, and other predators. Some however, will probably survive.";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }

    private string RetCaribouLesson(int lessonNumber)
    {
        switch (lessonNumber)
        {
            case 0:  //1
                return "In this game, you will play as a <b>female barren-ground caribou</b>.\n\n" +
                    "Barren-ground caribou migrate each year from northern tundra in the summer to taiga forests to the south in winter. They travel in groups of up to fifty. They mate during their migration to the south, and females give birth to a single calf about seven months later.\n\n" +
                    "Some caribou migrate further than any other land mammal, walking up to five-thousand kilometers in a single year!\n\n" +
                    "Calves tend to reach maturity at around two years old. Fewer than half make it that far.";
            case 1: //2
                return "The arctic wolf is the main predator of the barren-ground caribou.\n\n" +
                    "While wolves won’t usually attack the adults, they will target the young and the weak. The herd will try to keep the calves in the center, to shield them from the wolves. But in the chaos of a chase, calves sometimes fall through the cracks.\n\n" +
                    "Eventually, the caribou will always outrun the wolves, but not always without losses. Since caribou usually have only one calf a year, losing a child is a heavy blow, so it is very important to defend them from predators.";
            case 2://3
                return "It is early autumn, and your calf is four months old. Soon, the lakes will freeze over, and the herd will begin its seasonal mating.\n\n" +
                    "Wolves are chasing your herd relentlessly to the south, and you must guide your calf to safety. The wolves cannot run for nearly as long as the caribou can, so your calf must only avoid them until the greater endurance of the herd prevails.\n\n" +
                    "The sooner your calf gets to safety, the higher your score will be!";
            case 3: //O1
                return "Barren-ground caribou are considered a <i>K</i>-species. The females spend a lot of time pregnant and have at most one calf a year. They invest a lot of energy into the survival of their young.\n\n" +
                    "Since it is far easier for a herd of caribou to protect a group of calves together than it is for an isolated mother and father to protect a single calf, it is beneficial for all the caribou to live and travel together as a herd.";
            case 4: //O2
                return "With strength in numbers and speed on their side, barren-ground caribou successfully navigate between the winter feeding grounds in the south and the summer calving grounds in the north.\n\n" +
                    "A female can expect to have one calf that eventually reaches maturity every couple years, and that calf will make the awe-inspiring seasonal migration with its herd every year for the rest of its life.";
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
                return "As you have seen, animals engage in all kinds of behaviors in their ongoing struggle to survive and reproduce.\n\n" +
                    "Whether it's building nests, performing dances, or running rivers, all animals have ways of making a bid on life. While each strategy has its own costs and benefits, no one stategy is better than than the others.\n\n" +
                    "Now, a final test to see what you've learned!";
            case 1:
                return "This has been <b>Bid for Life</b>. We hope you have enjoyed witnessing and using a few of the many strategies of survival and reproduction that exist in the vast animal kingdom!";
            case 2:
                return "Have a great day!";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }
}
