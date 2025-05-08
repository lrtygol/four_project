using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public enum axis { x, y }
    public axis Direction = axis.y;
    public int speed = 2;
    private Vector3 start_pos;
    public float distance = 3f;

    void Start()
    {
        start_pos = transform.position;
        
    }

    
    void Update()
    {
        float amplitude = Mathf.PingPong (Time.time * speed, distance * 2) -distance;
        Vector3 NewPos = start_pos;
        if (Direction == axis.x)
        {
            NewPos.x = amplitude + NewPos.x;

        }
        else
        {
            NewPos.y = amplitude + NewPos.y;
        }
        transform.position = NewPos;
    }
}
