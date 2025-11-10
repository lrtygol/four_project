using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProPreJectile : MonoBehaviour
{
    public float speed = 50f;
    public float LifeTime = 5f;
    public int damage = 10;
    public GameObject sprite;
    private Quaternion Rotation = Quaternion.Euler(-90f, 0, 0);

    private Vector3 Direction;
    void Start()
    {
        Destroy(gameObject, LifeTime);
    }
    public void launch(Vector3 PlayerPos)
    {
        Direction = (PlayerPos - transform.position).normalized;
    }
    
    void Update()
    {
        transform.position += Direction * speed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        Instantiate(sprite, transform.position, Rotation);
        Run Plyer = other.GetComponent<Run>();
        if (Plyer != null)
        {
            getDamage(Plyer, 50);
        }
        else if (other.CompareTag("place") || !other.isTrigger)
        {
            Explode();
        }


    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        Instantiate(sprite, transform.position, Rotation);
        
        if (collision.gameObject.CompareTag("place"))
        {
            Explode();
        }


    }
    void Explode()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        Run Plyer = player.GetComponent<Run>();
        
        
        if (Plyer != null)
        {
            float Distance = Vector3.Distance(transform.position, player.transform.position);
            if (Distance < 10f)
                {
                getDamage(Plyer, damage);
                }
        
        }
    }
    void getDamage(Run Plyer, int finalDamage)
    {
        Plyer.hp -= finalDamage;

        Plyer.health.set_health(Plyer.hp);
    }
}
