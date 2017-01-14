using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour {

    //consts
    readonly float spawningYPos = 8;
    readonly Vector2 xspawnRange = new Vector2(-5f, 5f);

    public float costMultiplier = 1.5f; ///total number of snakes times this is cost for next
    public float currentBanked = 0;
    int numOfSnakes = 0;

    //snake creation
    public int snakesQueued = 0;
    public float snakeSpawnCounter = 0;
    
    public void Start()
    {
        snakesQueued = 3;
        numOfSnakes = 3;
    }

    private void CreateSnake()
    {
        GameObject snakego = Instantiate(Resources.Load("Prefabs/Snake")) as GameObject;
        snakego.transform.SetParent(GameObject.FindObjectOfType<FrogWS>().snakeParent);
        Snake newSnake = snakego.GetComponent<Snake>();
        SetupSnake(newSnake);        
    }

    public void Update()
    {
        snakeSpawnCounter -= Time.deltaTime;
        if(snakesQueued > 0 && snakeSpawnCounter <= 0)
        {
            CreateSnake();
            snakesQueued--;
            snakeSpawnCounter = Random.Range(1, 2);
        }
    }
    

    public void SnakeReachedEnd(Snake snake, float points)
    {
        if(points <= 0 && numOfSnakes > 3)
        {
            numOfSnakes--;
        }
        else
        {
            currentBanked += points;
            if (currentBanked >= numOfSnakes * costMultiplier)
            {
                currentBanked -= numOfSnakes * costMultiplier;
                snakesQueued += 2;
                numOfSnakes++;
            }
            else
            {
                snakesQueued++;
            }
        }
        Destroy(snake.gameObject);
    }

    private void SetupSnake(Snake snake)
    {
        float xpos = Random.Range(xspawnRange.x, xspawnRange.y);
        snake.transform.position = new Vector3(xpos, spawningYPos, 0);
    }
}
