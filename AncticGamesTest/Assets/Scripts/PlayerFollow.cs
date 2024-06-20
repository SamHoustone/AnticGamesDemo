using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 Offset;

    private void Update()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        transform.position = playerTransform.position + Offset;
    }
}
