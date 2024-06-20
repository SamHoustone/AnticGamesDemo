using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIBall : MonoBehaviour
{
    public float speed = 5f;
    public float detectionRadius = 10f;
    public LayerMask ballLayer;
    public float detectionInterval = 1f;

    private Vector2 targetPosition;
    private bool hasTarget = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public int noOfTurns = 10;

    public enum BallColor
    {
        Red, Green, Blue, Yellow, Cyan,
    }

    public BallColor color;

    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        SetColor();
        StartCoroutine(FindTargetRoutine());
    }

    private void SetColor()
    {
        switch (color)
        {
            case BallColor.Red:
                spriteRenderer.color = Color.red;
                break;
            case BallColor.Green:
                spriteRenderer.color = Color.green;
                break;
            case BallColor.Blue:
                spriteRenderer.color = Color.blue;
                break;
            case BallColor.Yellow:
                spriteRenderer.color = Color.yellow;
                break;
            case BallColor.Cyan:
                spriteRenderer.color = Color.cyan;
                break;
        }
    }

    private IEnumerator FindTargetRoutine()
    {
        while (noOfTurns > 0)
        {
                FindTarget();

            yield return new WaitForSeconds(detectionInterval);
        }
    }

    private void FindTarget()
    {
        Debug.Log("MOVE");

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, ballLayer);
        float closestDistance = detectionRadius;
        hasTarget = false;

        foreach (var hitCollider in hitColliders)
        {
            StaticBall otherBall = hitCollider.GetComponent<StaticBall>();

            if (otherBall != null && otherBall.color.HumanName() == color.HumanName())
            {
                float distance = Vector2.Distance(transform.position, otherBall.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetPosition = otherBall.transform.position;
                    hasTarget = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager gm = FindAnyObjectByType<GameManager>();

        if (collision.gameObject.CompareTag("StaticBall"))
        {
            if (collision.GetComponent<StaticBall>().color.HumanName() == color.HumanName())
            {
                hasTarget = false;
                gm.uiManager.UpdateAiScore(10);
                noOfTurns--;
            }
            else
            {
                gm.uiManager.UpdateAiScore(-10);
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Border"))
        {
        }
    }
}
