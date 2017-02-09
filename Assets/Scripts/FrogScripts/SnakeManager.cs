using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class SnakeManager : MonoBehaviour {

    public enum cardinalDir { North, East, South, West }
    //consts
    float averageFoodPoints;
    float totalPoints = 3;
    int snakesReturned = 1;
    List<Snake> activeSnakes = new List<Snake>();


    public void Start()
    {
        SetupGame();
    }

    public void SetupGame()
    {
        foreach (Snake s in activeSnakes)
            Destroy(s.gameObject);
        activeSnakes = new List<Snake>();
        CreateSnake();
        CreateSnake();
        CreateSnake();
        averageFoodPoints = 0;
        totalPoints = 3;
        snakesReturned = 1;
    }

    private void CreateSnake()
    {
        GameObject snakego = Instantiate(Resources.Load("Prefabs/Snake")) as GameObject;
        snakego.transform.SetParent(GameObject.FindObjectOfType<FrogWS>().snakeParent);
        Snake newSnake = snakego.GetComponent<Snake>();
        cardinalDir cardDir = (cardinalDir)(Random.Range(0, 4));
        SetupSnake(newSnake,cardDir);
        activeSnakes.Add(newSnake);   
    }
    

    public void SnakeReachedEnd(Snake snake)
    {
        snakesReturned++;
        totalPoints += snake.pointsEaten;
        averageFoodPoints = Mathf.Max(totalPoints / snakesReturned,3);
        //Debug.Log(string.Format("snake returned. point {0}, total pts {1}, total snakes {2}, score avrg {3}", snake.pointsEaten, totalPoints, snakesReturned, averageFoodPoints));
        bool resetSnake = true;
        int avrgPts = (int)averageFoodPoints;
        if(avrgPts != activeSnakes.Count)
        {
            if(avrgPts > activeSnakes.Count)
            {
                CreateSnake();
                //Debug.Log("Created snake");  
                //Make more snakes
            }
            else if (activeSnakes.Count <= 3)
            {
                //Debug.Log("reset snake norm");
                //Its fine, dont remove more snakes
            }
            else
            {//remove snakes
                //Debug.Log("Delete Snake");
                resetSnake = false;
                Destroy(snake.gameObject);
                activeSnakes.Remove(snake);
            }
        }


        if (resetSnake)
        {
            snake.pointsEaten = 0;
            cardinalDir cardDir = (cardinalDir)(Random.Range(0, 4));
            SetupSnake(snake, cardDir);
        }
    }

    private void SetupSnake(Snake snake, cardinalDir headingDir)
    {
        Vector2 spawnBoundry = new Vector2(6.2f, 4.8f);
        float snakeLength = 2;
        switch (headingDir)
        {
            case cardinalDir.North:
                snake.snakeMoveDir = new Vector2(0, 1);
                snake.transform.position = new Vector2(Random.Range(-spawnBoundry.x, spawnBoundry.x), -spawnBoundry.y - snakeLength);
                snake.transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case cardinalDir.East:
                snake.snakeMoveDir = new Vector2(1, 0);
                snake.transform.position = new Vector2(-spawnBoundry.x - snakeLength, Random.Range(-spawnBoundry.y, spawnBoundry.y));
                snake.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case cardinalDir.South:
                snake.snakeMoveDir = new Vector2(0, -1);
                snake.transform.position = new Vector2(Random.Range(-spawnBoundry.x, spawnBoundry.x), spawnBoundry.y + snakeLength);
                snake.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case cardinalDir.West:
                snake.snakeMoveDir = new Vector2(-1, 0);
                snake.transform.position = new Vector2(spawnBoundry.x + snakeLength, Random.Range(-spawnBoundry.y, spawnBoundry.y));
                snake.transform.eulerAngles = new Vector3(0, 0, 270);
                break;
            default:
                snake.snakeMoveDir = new Vector2();
                break;
        }


    }
}
