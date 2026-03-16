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
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject P = GameObject.FindGameObjectWithTag("Player");
        player = P.transform;
    }

    
    void Update()
    {
        Run Plyer = player.GetComponent<Run>();
        if (elevator)
        {
            rb.isKinematic = true;
            Vector3 pos = transform.position;
            float newY = Mathf.Lerp(pos.y, Y, Time.deltaTime * 2);
            transform.position = new Vector3(pos.x, newY, pos.z);
            
            if (Mathf.Abs(Y - transform.position.y)< 0.05f)
            {
                transform.position = new Vector3(pos.x, Y, pos.z);
                elevator = false;
                rb.isKinematic = false;
            }
        }
        else 
        {
            if (player == null) return;

            float distance = Vector3.Distance(transform.position, player.position);
            
            if (distance <= 20 && speed > 0 && Time.time >= Timing + 3f)
            {
                // ... (логика движения)
                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0;
                transform.position += direction * speed * Time.deltaTime;
                Quaternion rotate = Quaternion.LookRotation(direction) * Quaternion.Euler(-90, 0, 0);
                transform.rotation = rotate;
                if (distance < 1f)
                {
                    Plyer.hp -= damage;
                    Plyer.health.set_health(Plyer.hp);
                    Rigidbody playerRB = Plyer.GetComponent<Rigidbody>();

                    if (playerRB != null)
                    {

                        Vector3 pushDirection = (player.position - transform.position).normalized;
                        playerRB.velocity = Vector3.zero;
                        pushDirection.y = 0.3f;
                        Plyer.blocker = true;
                        playerRB.AddForce(pushDirection.normalized * 400f, ForceMode.Impulse);

                    }

                    Timing = Time.time;
                }
                
            }
            if (Time.time >= Timing + 1.0f)
            {
                Plyer.blocker = false;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            Run Plyer = player.GetComponent<Run>();
            if (Plyer.isAttacking)
            {
                Destroy(gameObject);
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
            rb.AddForce(pushDirection.normalized * 80f, ForceMode.Acceleration);
        }
    }



}
