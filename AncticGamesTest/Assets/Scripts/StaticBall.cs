using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBall : MonoBehaviour
{
    private Vector2 initialScale;
    private float breathSpeed = 1f;
    private static float timeAccumulator;
    private static float lastTimeUpdate;
    private static float scale;
    public enum BallColor
    {
        Red, Green, Blue, Yellow, Cyan,
    }

    public GameObject positive, negative;
    public BallColor color;

    private void Start()
    {
        initialScale = transform.localScale;
        timeAccumulator = 0f;

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

    public void SetScoreUI(int index)
    {
        if (index == 0)
        {
            positive.SetActive(true);
        }
        else
        {
            negative.SetActive(true);
        }

    }
    void FixedUpdate()
    {
        if (Time.time - lastTimeUpdate > 0.01f) // Adjust the interval as needed
        {
            lastTimeUpdate = Time.time;
            timeAccumulator += Time.deltaTime * breathSpeed;
            scale = Mathf.PingPong(timeAccumulator, 0.3f) + 0.75f;
        }

        transform.localScale = initialScale * scale;
    }
}
