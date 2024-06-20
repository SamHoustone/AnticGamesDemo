using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Camera.main.orthographicSize--;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Camera.main.orthographicSize++;
        }
    }
}
