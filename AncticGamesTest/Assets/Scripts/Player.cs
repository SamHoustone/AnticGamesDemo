using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject pointer;

    public float maxInitVelocity = 10f;
    public float deceleration = 1f;
    public int movesLeft = 10;

    private Vector2 velocity;

    public bool canMove;
    public bool isMoving;

    public List<GameObject> hitObjects = new List<GameObject>();

    public GameManager gameManager;
    public enum BallColor
    {
        Red, Green, Blue, Yellow, Cyan,
    }

    public BallColor color;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        SpriteRenderer mat = GetComponent<SpriteRenderer>();
        switch (color)
        {
            case BallColor.Red:
                mat.color = Color.red;
                break;
            case BallColor.Green:
                mat.color = Color.green;
                break; 
            case BallColor.Blue:
                mat.color = Color.blue;
                break;
            case BallColor.Yellow:
                mat.color = Color.yellow;
                break;
            case BallColor.Cyan:
                mat.color = Color.cyan;
                break;
        }

    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && !isMoving)
        {
            Vector2 startPos = transform.position;
            Vector2 pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (pointerPos - startPos).normalized;

            float distance = Vector2.Distance(startPos, pointerPos);
            float initalVelocity = Mathf.Min(maxInitVelocity, distance);

            velocity = direction * initalVelocity;

            // pointer Direction
            pointer.SetActive(true);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            pointer.transform.rotation = Quaternion.Euler(0, 0, angle);

            gameManager.uiManager.UpdatePower(distance);
        }

        if (Input.GetMouseButtonUp(0) && !isMoving)
        {
            canMove = true;
        }
        if (canMove == true)
        {
            MoveTheBall();
        }
    }
    private void MoveTheBall()
    {
        isMoving = true;
        pointer.SetActive(false);

        transform.Translate(velocity * Time.deltaTime);
        velocity = Vector2.MoveTowards(velocity, Vector2.zero, deceleration * Time.deltaTime);

        if (velocity.magnitude < 0.1f)
        {
            velocity = Vector2.zero;
            isMoving = false;
            canMove = false;

            CollectScore();
            movesLeft--;
            gameManager.uiManager.UpdateMoves(movesLeft);

            if (movesLeft == 0)
            {
                gameManager.uiManager.UpdateEndScreen();
            }
        }
    }
    private void CollectScore()
    {
        for (int i = 0; i < hitObjects.Count; i++)
        {
            if (hitObjects[i].GetComponent<StaticBall>().color.HumanName() == color.HumanName())
            {
                Debug.Log("GOOD");
                gameManager.uiManager.UpdatePlayerScore(10);
            }
            else
            {
                Debug.Log("BAD");
                gameManager.uiManager.UpdatePlayerScore(-10);
            }
            Destroy(hitObjects[i].gameObject);
        }
        hitObjects.Clear();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StaticBall"))
        {
            hitObjects.Add(collision.gameObject);
            if (collision.GetComponent<StaticBall>().color.HumanName() == color.HumanName())
            {
                collision.GetComponent<StaticBall>().SetScoreUI(0);
            }
            else
            {
                collision.GetComponent<StaticBall>().SetScoreUI(1);
            }
        }
        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("STOP");
            velocity = Vector2.zero;
        }
    }
}
