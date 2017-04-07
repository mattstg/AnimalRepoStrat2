using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class SnakeManager  {

    #region Singleton
    private static SnakeManager instance;

    private SnakeManager() { }

    public static SnakeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SnakeManager();
            }
            return instance;
        }
    }
    #endregion

    public enum cardinalDir { North, East, South, West }
    //consts
    public bool playerIsTargetable = false;
    //float averageFoodPoints;
    //float totalPoints = 3;
    int snakesReturned = 1;
    List<Snake> activeSnakes = new List<Snake>();
    Stack<Snake> bankedSnakes = new Stack<Snake>();
    Transform player;

    public void SetupGame()
    {
        foreach (Snake s in activeSnakes)
            MonoBehaviour.Destroy(s.gameObject);
        activeSnakes = new List<Snake>();
        bankedSnakes = new Stack<Snake>();
        CreateSnake();
        CreateSnake();
        CreateSnake();
        //totalPoints = 3;
        //averageFoodPoints = 0;
        //snakesReturned = 1;
        player = FrogGV.frogWS.frogGF.playerFrog.transform;
    }

    private void CreateSnake()
    {
        Snake newSnake;
        if (bankedSnakes.Count > 0)
        {
            newSnake = bankedSnakes.Pop();
            newSnake.gameObject.SetActive(true);
        }
        else
        {
            GameObject snakego = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Snake")) as GameObject;
            snakego.transform.SetParent(FrogGV.frogWS.snakeParent);
            newSnake = snakego.GetComponent<Snake>();
        }
        cardinalDir cardDir = (cardinalDir)(Random.Range(0, 4));
        SetupSnake(newSnake,cardDir);
        activeSnakes.Add(newSnake);   
    }
    
    public void UpdateSnakeManager(float dt)
    {
        int prefferedSnakeCount = Mathf.Max(FrogGV.frogWS.frogParent.childCount / FrogGV.frogsPerSnake, FrogGV.minSnakes);
        if(prefferedSnakeCount > activeSnakes.Count)
            CreateSnake();

        //mmove snakes
        foreach (Snake s in activeSnakes)
            s.UpdateSnake(dt);

        //Check all snake collisions
        List<Frog> allFrogs = FrogGV.masterList;
        for(int i = allFrogs.Count - 1; i >= 0; i--)
        {
            foreach(Snake s in activeSnakes)
            {
                float dist = MathHelper.ApproxDist(s.transform.position, allFrogs[i].transform.position);
                if(dist < FrogGV.snakeEatRange)
                {
                    allFrogs[i].FrogEaten();
                    break;
                }
            }
        }
    }

    public void SnakeReachedEnd(Snake snake)
    {
        activeSnakes.Remove(snake);
        bankedSnakes.Push(snake);
        snake.gameObject.SetActive(false);
    }

    //Snake gets setup at random border depending on headingDir, random chance to spawn targeting player, so alligned with his position
    private void SetupSnake(Snake snake, cardinalDir headingDir)
    {
        bool playerTargeted = Random.Range(0,1f) > .8f;
        if (!player || !playerIsTargetable)
            playerTargeted = false;
        Vector2 spawnBoundry = new Vector2(3.5f, 2.6f);
        float snakeLength = 2;
        switch (headingDir)
        {
            case cardinalDir.North:
                snake.snakeMoveDir = new Vector2(0, 1);
                if(!playerTargeted)
                    snake.transform.position = new Vector2(Random.Range(-spawnBoundry.x, spawnBoundry.x), -spawnBoundry.y - snakeLength);
                else
                    snake.transform.position = new Vector2(player.position.x, -spawnBoundry.y - snakeLength);
                snake.transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case cardinalDir.East:
                snake.snakeMoveDir = new Vector2(1, 0);
                if(!playerTargeted)
                    snake.transform.position = new Vector2(-spawnBoundry.x - snakeLength, Random.Range(-spawnBoundry.y, spawnBoundry.y));
                else
                    snake.transform.position = new Vector2(-spawnBoundry.x - snakeLength, player.position.y);
                snake.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case cardinalDir.South:
                snake.snakeMoveDir = new Vector2(0, -1);
                if (!playerTargeted)
                    snake.transform.position = new Vector2(Random.Range(-spawnBoundry.x, spawnBoundry.x), spawnBoundry.y + snakeLength);
                else
                    snake.transform.position = new Vector2(player.position.x, spawnBoundry.y + snakeLength);
                snake.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case cardinalDir.West:
                snake.snakeMoveDir = new Vector2(-1, 0);
                if(!playerTargeted)
                    snake.transform.position = new Vector2(spawnBoundry.x + snakeLength, Random.Range(-spawnBoundry.y, spawnBoundry.y));
                else
                    snake.transform.position = new Vector2(spawnBoundry.x + snakeLength, player.position.y);
                snake.transform.eulerAngles = new Vector3(0, 0, 270);
                break;
            default:
                snake.snakeMoveDir = new Vector2();
                break;
        }


    }
}
