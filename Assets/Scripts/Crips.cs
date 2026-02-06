using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crips : MonoBehaviour
{
    public float Y = 191f;
    public bool elevator = true;
    public int speed = 3;
    public int damage = 5;
    private Transform player;
    private float Timing;

    void Start()
    {
        GameObject P = GameObject.FindGameObjectWithTag("Player");
        player = P.transform;
    }

    
    void Update()
    {
        Run Plyer = player.GetComponent<Run>();
        if (elevator)
        {
            Vector3 pos = transform.position;
            float newY = Mathf.Lerp(pos.y, Y, Time.deltaTime * 2);
            transform.position = new Vector3(pos.x, newY, pos.z);
            
            if (Mathf.Abs(Y - transform.position.y)< 0.05f)
            {
                transform.position = new Vector3(pos.x, Y, pos.z);
                elevator = false;
            }
        }
        else 
        {
            if (player == null) return;

            float distance = Vector3.Distance(transform.position, player.position);
            
            if (distance <= 20 && speed > 0)
            {
                // ... (логика движения)
                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0;
                transform.position += direction * speed * Time.deltaTime;
                Quaternion rotate = Quaternion.LookRotation(direction) * Quaternion.Euler(-90, 0, 0);
                transform.rotation = rotate;
                if (distance < 1f && Time.time >= Timing + 3f)
                {
                    Plyer.hp -= damage;

                    Plyer.health.set_health(Plyer.hp);

                    Timing = Time.time;
                }
                // ... (логика вращения)
            }
        }

    }



    void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("crip"))
        {
            Debug.Log("да");
            Vector3 pushDirection = transform.position - collision.transform.position;
            pushDirection.y = 0;
            transform.position += pushDirection * Time.deltaTime * 2f;
        }
    }



}
