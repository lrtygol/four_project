using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public int Maxhp = 1000;
    private int currethp;
    public Transform Plocation;
    public GameObject ProPrefabJectile;
    public Transform attackPoint;
    public float attackCD = 4f;
    private float nextAttackTime;
    public Slider Boss_slider;
    public int Phase = 1;
    
    public GameObject crips;
    public BoxCollider SpawnArea;
    

    void Start()
    {
        currethp = Maxhp;
        nextAttackTime = Time.time + attackCD;
    }

    public void TakeDamage(int damage)
    {
        currethp -= damage;
        Boss_slider.value = currethp;
        Debug.Log(currethp);

        ChangePhase();

    }

    void Update()
    {
        LookAtP();

        if (Time.time >= nextAttackTime)
        {
            attack();
            Debug.Log(currethp);
            nextAttackTime = Time.time + attackCD;
        }
    }

    void ChangePhase()
    {
        if (currethp <= 800 && currethp >= 600 && Phase == 1)
        {
            Phase = 2;
            for (int i = 0; i < 10; i++)
            {
                
                Bounds B = SpawnArea.bounds;
                Vector3 RandomPos = new Vector3(
                    Random.Range(B.min.x, B.max.x),
                    B.min.y,
                    Random.Range(B.min.z, B.max.z)
                );
                GameObject spawning = Instantiate(crips, RandomPos, Quaternion.Euler(-90, 0, 0));
                
            }
        }
        if (currethp <= 600 && currethp >= 400 && Phase == 2)
        {
            Phase = 3;
            for (int i = 0; i < 10; i++)
            {
                Bounds A = SpawnArea.bounds;
                Vector3 RandomPos = new Vector3(
                    Random.Range(A.min.x, A.max.x),
                    A.min.y,
                    Random.Range(A.min.z, A.max.z)
                    );
                GameObject spawning = Instantiate(crips, RandomPos, Quaternion.Euler(-90, 0, 0));
            }

        }
    }
    
    //IEnumerator up (Transform CripPos, Vector3 TargetPos)
    //{
        
    //}

    void LookAtP()
    {
        Vector3 direction = (Plocation.position - transform.position).normalized;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation *= Quaternion.Euler(-90, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 0.5f);
    }

    void attack()
    {

        GameObject projectile = Instantiate(ProPrefabJectile, attackPoint.position, transform.rotation);
        ProPreJectile projectScript = projectile.GetComponent<ProPreJectile>();
        projectScript.launch(Plocation.position);

    }
}
