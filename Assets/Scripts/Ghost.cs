using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int speed = 3;
    public Transform player;
    private float distance;
    void Start()
    {
        
    }

    
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= 8)
        {
            transform.LookAt(player);
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
        }
        
    }
}
