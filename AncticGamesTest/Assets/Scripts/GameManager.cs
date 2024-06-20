using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab, aiPrefab, staticBallPrefab;

    public List<Collider2D> StaticBalls = new List<Collider2D>();
    public GameObject playerBall;
    public UiManager uiManager;

   

    public int staticBallNumber;
    public int noOfAI;
    public int PlayerScore, AiScore;

    public enum BallColor
    {
        Red, Green, Blue, Yellow, Cyan,
    }

    public float mapSize;

  


    private void Start()
    {
        SpawnPlayer();
        SpawnStaticBalls(staticBallNumber);
        SpawnAI(noOfAI);
    }

    public void SpawnPlayer()
    {
        playerBall = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);  
        playerBall.gameObject.tag = "Player";

        Player playerScript = playerBall.GetComponent<Player>();

        int Range = Random.Range(0, 5);

        switch (Range)
        {
            case 0 :
                playerScript.color = Player.BallColor.Red;
                break;
            case 1 :
                playerScript.color = Player.BallColor.Green;
                break;
            case 2:
                playerScript.color = Player.BallColor.Blue;
                break;
            case 3:
                playerScript.color = Player.BallColor.Yellow;
                break;
            case 4:
                playerScript.color = Player.BallColor.Cyan;
                break;
        }

    }
    void SpawnAI(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Vector2 position = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
            AIBall aIBall = Instantiate(aiPrefab, position, Quaternion.identity).GetComponent<AIBall>();

            int Range = Random.Range(0, 5);

            switch (Range)
            {
                case 0:
                    aIBall.color = AIBall.BallColor.Red;
                    break;
                case 1:
                    aIBall.color = AIBall.BallColor.Green;
                    break;
                case 2:
                    aIBall.color = AIBall.BallColor.Blue;
                    break;
                case 3:
                    aIBall.color = AIBall.BallColor.Yellow;
                    break;
                case 4:
                    aIBall.color = AIBall.BallColor.Cyan;
                    break;
            }
        }
    }
    void SpawnStaticBalls(int num)
    {


        for (int i = 0; i < num; i++)
        {
            bool ballPlaced = false;

            while (!ballPlaced)
            {
                Vector2 position = new Vector2(Random.Range(-mapSize, mapSize), Random.Range(-mapSize, mapSize));
                GameObject newBall = Instantiate(staticBallPrefab, position, Quaternion.identity);
                Collider2D newBallCollider = newBall.GetComponent<Collider2D>();

                if (!IsOverlapping(newBallCollider))
                {
                    StaticBalls.Add(newBallCollider);
                    StaticBalls[i].tag = "StaticBall";
                    ballPlaced = true;
                }
                else
                {
                    Destroy(newBall);
                }
            }

            StaticBall staticBallScript = StaticBalls[i].GetComponent<StaticBall>();

            int Range = Random.Range(0, 5);

            switch (Range)
            {
                case 0:
                    staticBallScript.color = StaticBall.BallColor.Red;
                    break;
                case 1:
                    staticBallScript.color = StaticBall.BallColor.Green;
                    break;
                case 2:
                    staticBallScript.color = StaticBall.BallColor.Blue;
                    break;
                case 3:
                    staticBallScript.color = StaticBall.BallColor.Yellow;
                    break;
                case 4:
                    staticBallScript.color = StaticBall.BallColor.Cyan;
                    break;
            }
        }
    }
    public void EndGame()
    {

    }
    

    bool IsOverlapping(Collider2D newBall)
    {
        foreach (Collider2D existingBall in StaticBalls)
        {
            if (newBall.bounds.Intersects(existingBall.bounds))
            {
                return true;
            }
        }
        return false;
    }
}
