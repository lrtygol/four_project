using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int Maxhp = 1000;
    private int currethp;
    public Transform Plocation;
    public GameObject ProPrefabJectile;
    public Transform attackPoint;
    public float attackCD = 4f;
    private float nextAttackTime;

    void Start()
    {
        currethp = Maxhp;
        nextAttackTime = Time.time + attackCD;
    }

    public void TakeDamage(int damage)
    {
        currethp -= damage;
        Debug.Log(currethp);
    }

    void Update()
    {
        LookAtP();
        if (Time.time >= nextAttackTime)
        {
            attack();
            nextAttackTime = Time.time + attackCD;
        }
    }


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
