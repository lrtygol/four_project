using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProPreJectile : MonoBehaviour
{
    public float speed = 50f;
    public float LifeTime = 5f;
    public int damage = 50; 
    public GameObject sprite;
    public bool onehit = false;
    private Quaternion Rotation = Quaternion.Euler(-90f, 0, 0);
    private bool reflected = false;
    private AudioSource audioSource;
    public AudioClip Explode_mp3;
    public AudioClip Shield_mp3;
    private Vector3 Direction;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, LifeTime);
    }
    public void launch(Vector3 PlayerPos)
    {
        Direction = (PlayerPos - transform.position).normalized;
    }
    public void launch2(Vector3 DiR)
    {
        Direction = DiR.normalized; 
    }

    void Update()
    {
        transform.position += Direction * speed * Time.deltaTime;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shield") && !reflected)
        {
            AudioSource.PlayClipAtPoint(Shield_mp3, transform.position);
            Reflect(other);
            return;
        }
        Run Plyer = other.GetComponent<Run>();
        if (Plyer != null && !reflected)
        {
            
            Explode(damage);
            return;
        }
        else if (other.CompareTag("Boss") && reflected)
        {
            
            Boss BossScript = other.GetComponent<Boss>();
            BossScript.TakeDamage(damage * 20);
            Explode(damage);
        }
        else if (other.CompareTag("crip") && reflected)
        {
            
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("place"))
        {
            
            Explode(damage);
            
        }


    }
    
    public void Reflect(Collider Shield)
    {
        reflected = true;
        Vector3 ShieldNormal = -Shield.transform.right;
        ShieldNormal = ShieldNormal.normalized;
        

        Vector3 newDirection = Vector3.Reflect(Direction, ShieldNormal);

        Direction = newDirection.normalized;

        //Direction.y = 0;

        //transform.rotation = Quaternion.LookRotation(Direction);
        gameObject.tag = "reflected";
    }


    void Explode(int finalDamage)
    {
        if (onehit)
        {
            
            return;
        }
        onehit = true;
        if (reflected)
        {
            Rotation = Quaternion.Euler(0, 90f, 0);


        }
        AudioSource.PlayClipAtPoint(Explode_mp3, transform.position);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Run Plyer = player.GetComponent<Run>();
        GameObject explode = Instantiate(sprite, transform.position, Rotation);
        Destroy(explode, 1f);
        
        ParticleSystem fire = GetComponent<ParticleSystem>();
        fire.transform.parent = null;
        fire.Stop();
        Destroy(fire.gameObject, 1f);


        if (Plyer != null)
        {
            float Distance = Vector3.Distance(transform.position, player.transform.position);
            if (Distance < 5f)
            {
                
                Plyer.hp -= finalDamage;
                
                Plyer.health.set_health(Plyer.hp);
            }
        
        }
        
        Destroy(gameObject);
    }
}
