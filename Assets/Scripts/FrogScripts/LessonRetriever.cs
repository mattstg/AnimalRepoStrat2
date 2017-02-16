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
        case 0: //1
			return "Bid For Life is a game about reproductive strategies in the wild. All animal species engage in behaviors aimed at increasing their chances of parenting successful offspring.\nSome species tend to have many offspring and provide little or no parental care for their young. These species tend to have high infant mortality rates, but they often thrive due to their high birth rates. These species are known as r species, and include many fish and insects. They tend to be small, grow quickly, and live in unstable or dynamic environments.";
		case 1://2
			return "At the other extreme live animals that tend to have very few offspring, but which invest large amounts of energy into raising and protecting those offspring. These species are known as K species, and include animals such as elephants, bears, and humans. While these species tend to produce few young, the high degree of parental care makes it more likely that those young will survive. K species tend to be large, have low mortality rates, grow slowly, and spend longer periods of their lives dependent upon parents.";
		case 2://3
			return "In reality, r and K do not represent two categories of animals. Rather, r and K are the two extremes in a range of animal behaviors, and individual species can find themselves anywhere in that range. Keep in mind that r and K are just concepts people use to help describe and compare the many different reproductive strategies throughout the animal kingdom. Many animals fall somewhere in the middle, and some have traits from both extremes of the r-K range. ";
		case 3://4
			return "Many species of animals also engage in specific mating rituals and behaviors aimed at attracting mates. Typically, this is a show of desirable traits, such as healthy feathers, strength, a strong voice. Selecting a mate with those behaviors will mean your offspring will be more likely to have those desirable traits themselves. Other behaviours are  herding  or nesting behaviours, aimed to protect the young.";
		case 4://outro1
			return "Bid For Life is made up of five mini-games in which you will play as a member of five different animal species. Make sure you pay attention to the short lessons before and after each game, because you can boost your score by doing well on the quizzes! Have fun!";
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
        case 0: //intro1
			return "In this game, you will play as a pacific salmon. Salmon are hatched in freshwater rivers, but they eventually make their way out to the saltwater ocean, where they will spend most of their lives. Once they reach sexual maturity, they engage in one of nature’s spectacular events: the salmon run. Each year, millions of salmon return to the rivers where they were born, and swim hundreds of kilometers upstream to the pools where they were born. Along the way, many are caught by predators (especially grizzly bears).";
		case 1://intro2
			return "The pacific salmon that make it to their spawning grounds mate there for the only time in their lives. They do not engage in selective mating; rather, the females lay huge clusters of eggs on the riverbed, which the males then fertilize together.  Shortly after mating, all pacific salmon die, and the life-cycle continues. Most of the young won’t survive to adulthood, but those who do will do so completely without their parents.";
		case 2://intro3
			return "It is autumn, and you have just arrived at the river mouth that leads to the pool where you once hatched.  Alongside countless other salmon, you strike up against the current. Watch out for the grizzly bears! They wait for the salmon run all year, and are experts at fishing!\nThe faster you reach your spawning ground, the higher your score will be.";
       	case 3://outro1
			return " Like spadefoot toads, salmon are considered r-selected species. They give birth to huge amounts of young, and provide no parental care. The run up the river itself serves as a selection process for salmon. Only those that can survive the difficult, dangerous journey up to the spawning grounds get to mate and pass on their genes.  For a salmon, completing the run safely is the secret to successful mating. ";
		case 4://outro2
			return "Although it may seem bizarre and dangerous, the salmon run is a reproductive strategy that allows young salmon to grow up in the relative safety of freshwater pools. Though most offspring will die before they make it back out to the ocean, some will go on to run the river in their own time, and face the same bears and waterfalls that challenged their parents years before. ";
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
			return " In this game you will play as a male bowerbird. Bowerbirds are considered a K species. They usually lay one egg, which takes about three weeks to hatch. The female raises her young for two months after they hatch. Some bowerbirds take up to eight years to mature. Bowerbirds are particularly famous for their mating rituals. Male bowerbirds create small structures made of twigs called bowers, and adorn them with decorations. These can include  pebbles, shells, feathers, flowers, and berries. ";
		case 1: //L2
			return "Females who are interested in mating visit the bowers of multiple males, often returning to each one several times. Males  try especially hard to find blue decorations, since blue is the favorite color of most female bowerbirds. Unlike spadefoot toads and salmon, female bowerbirds are extremely selective in their mating. Since they have so few offspring, they must ensure that the children they do have are as healthy and strong as possible. Therefore, it pays off to find the most impressive male in the neighborhood for a mate.";
        case 2: //L3
			return "It is mating season, and you have just completed your twig bower. You’ve spotted a female in the area who seems to be interested in finding a mate. However, a few rival males in the area seem to have taken notice as well. Outperform your rivals by collecting the most impressive assortment of decorations for your bower!\n To get a high score, make sure the female bowerbird chooses you over the others. The faster she chooses you, the higher the score!";
        case 3: //outro1
			return "Competition over female bowerbirds is fierce, and it is the main challenge males will face in their struggle to reproduce. Many of them fail to mate at all, unable to compete with their neighbors. While salmon are selected for mating by their ability to survive the run, male bowerbirds are selected for mating by their treasure-hunting and decorating skills. Since they have so few children, female bowerbirds must ensure that each one high a high chance of survival. Therefore, they select only the owners of the best Bowers as mates.";
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
				return "In this game you will play as a female wood duck with ten day-old ducklings in your care. Wood ducks make their nests high up in trees, near large bodies of water. When wood duck hatchlings are a day old, they jump from the nest down to the forest floor (up to a twenty-meter drop!). The mother then leads her hatchlings to the nearest body of water.";
            case 1://intro2
				return "This is the most dangerous time in the young ducklings’ lives, for they are easy targets for predators. The mother does not defend her offspring during the march, but she repeatedly calls them to her as she leads the way. Though wood ducks have more offspring than many K species, they have far fewer than salmon or toads, and about two thirds of them never reach adulthood. The mother duck provides moderate care for her young in the form of incubating the eggs, leading them to the water, and then raising them until they can fly.";
            case 2://into3
				return "It is early spring, and your hatchlings have just jumped to the forest floor. There is a large lake nearby to the south, but beware: this forest is a home for red foxes, and they would have no trouble catching a duckling if they see one. Call your young to you when they wander, and head for the open waters! The more ducklings you can safely lead to the lake, the higher your score will be.";
            case 3://outro1
				return "It is safe to say that wood ducks are somewhere in the middle between r and K. The ducklings are born in medium-sized clutches, mature at a medium pace, have a medium mortality rate, and receive a medium amount of parental care. Though the initial walk from the nest to the water is perilous, it allows wood ducks to lay their eggs in the relative safety of tall trees. As the ducklings who make it to the water mature, several of them will fall prey to snakes, turtles, bass, and other predators. Some however, will probably survive.";
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
			return "In this game, you will play as a female barren-ground caribou. Barren-ground caribou migrate each year from northern tundra in the summer to taiga forests to the south in winter. They travel in groups of up to fifty. They mate during their migration to the south, and females give birth to a single calf about seven months later.. Some caribou migrate further than any other land mammals, walking up to five-thousand kilometers in a single year! Calves tend to reach maturity at around two years old. Fewer than half make it that far. ";
            case 1: //2
			return "The arctic wolf is the main predator of the barren-ground caribou. While wolves won’t usually attack the adults, they will target the young and the weak. The herd will try to keep the calves in the center, to shield them from the wolves, but in the chaos of a chase, calves sometimes fall through the cracks. Eventually, the caribou will always outrun the wolves, but not always without losses. Since caribou usually have only one calf a year, losing a child is a heavy blow, so it is very important to defend them from predators.";
            case 2://3
			return "It is early autumn, and your calf is four months old. Soon, the lakes will freeze over, and the herd will begin their seasonal mating.  Wolves are chasing your herd relentlessly to the south, and you must guide your calf to safety. The wolves cannot run for nearly as long as the caribou can, so your calf must only avoid them until the greater endurance of the herd prevails. The sooner your calf gets to safety, the higher your score will be!";
            case 3: //O1
			return "Barren-ground caribou are considered a K species. The females spend a lot of time pregnant and have at most one calf a year. They invest a lot of energy into the survival of their young. Since it is far easier for a herd of caribou to protect a group of calves together than it is for an isolated mother and father to protect a single calf, it is beneficial for all the caribou to live and travel together as a herd. ";
            case 4: //O2
			return "With strength in numbers and speed on their side, barren-ground caribou successfully navigate between the winter feeding grounds in the south and the summer calving grounds in the north. A female can expect to have one calf that eventually reaches maturity every couple years, and that calf will make the awe-inspiring seasonal migration with its herd every year for the rest of its life.";
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
                return "As you can see, many of these animals had different takes on reproduction strategies, but there isn’t one strategy better than the rest. Each animal as you saw had a different take on how to ensure the continuation of it’s species. From the building nests, maturing quickly, or simply just having more kids than predators can eat.  Now, a final test to proven what you’ve learnt.";
            case 1:
                return "This has been Bid for Life. I hope you enjoyed experiencing snippets of life from the viewpoint of animals, each who have different, but viable methods of  surviving and reproducing in their environments. There are plenty more resources online if you wish to look up some of the more unique facts about these animals. Such as how the grizzly bears massively hunt the salmon leaving large piles of  fish that feed the trees in the forest, or the how the bower and other birds have unique dances.";
            case 2:
                return "Have a great day!";
            default:
                Debug.Log("Lesson out of bounds " + lessonNumber);
                return "";
        }
    }
}
