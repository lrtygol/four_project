using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crips : MonoBehaviour
{
    public float Y = 191f;
    public bool elevator = true;
    public int speed = 3;
    private Transform player;


    void Start()
    {
        GameObject P = GameObject.FindGameObjectWithTag("Player");
        player = P.transform;
    }

    
    void Update()
    {

        if (elevator)
        {
            Vector3 pos = transform.position;
            float newY = Mathf.Lerp(pos.y, Y, Time.deltaTime * 2);
            transform.position = new Vector3(pos.x, newY, pos.z);
            
            if (Mathf.Abs(newY - pos.y)< 0.05f)
            {
                elevator = false;
            }
        }
        else 
        {
            if (player == null) return;

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= 8 && speed > 0)
            {
                // ... (логика движения)
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
                // ... (логика вращения)
            }
        }
    }
}
