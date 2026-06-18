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

    public GameObject TVOROG;

    public Transform attackPoint;
    public float attackCD = 4f;
    private float nextAttackTime;
    public Slider Boss_slider;
    public int Phase = 1;

    public bool HealSpawn = false;
    private float nextHealthSpawn;
    public float HealthSpawnCD = 5f;


    public GameObject PartsCenter;
    public GameObject parts;
    public GameObject BossPlate;


    public float Distance_Meteor = 30f;
    public float Spacing = 2f;
    public int Meteors = 7;
    public Transform attackLine;

    public float MeteorCD = 1f;
    private float nextMeteorTime;

    public GameObject TVOROgHide;
    public GameObject GhostHide;
    public GameObject crips;
    public BoxCollider[] SpawnArea;
    public GameObject ProPrefabJectile;
    public BoxCollider[] MeteorArea;

    void Start()
    {
        currethp = 199;
        nextAttackTime = Time.time + attackCD;
        Crips.Dist_e = 20f;


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

        if (Time.time >= nextAttackTime && Phase != 5)
        {
            
            attack();
            Debug.Log(currethp);
            nextAttackTime = Time.time + attackCD;
        }
        if (Phase == 3 &&  Time.time >= nextMeteorTime)
        {
            meteors();
            nextMeteorTime = Time.time + MeteorCD;
        }
        if (Phase == 4 && Time.time >= nextMeteorTime)
        {
            meteors();
            nextMeteorTime = Time.time + MeteorCD/3;
            
        }
        if (Time.time >= nextAttackTime && Phase == 5)
        {
            MeteorLine();
            nextAttackTime = Time.time + attackCD;

        }
        if (Phase == 5 && Time.time >= nextMeteorTime)
        {
            meteors();
            nextMeteorTime = Time.time + MeteorCD / 4;
            
            
        }


        if (Time.time >= nextHealthSpawn && HealSpawn)
        {
            nextHealthSpawn = Time.time + HealthSpawnCD;
            Debug.Log("есть{Phase}");

            if (Phase == 5)
            {
                
                int randomIndex = Random.Range(0, PartsCenter.transform.childCount);
                Transform randomPart = PartsCenter.transform.GetChild(randomIndex);
                Vector3 randomPos = randomPart.position;
                randomPos.y += 1f;
                GameObject spawning = Instantiate(TVOROG, randomPos, Quaternion.Euler(0, 0, 0), TVOROgHide.transform);
            }
            else 
            {
                BoxCollider randomPart = SpawnArea[Random.Range(0, SpawnArea.Length)];
                Bounds A = randomPart.bounds;
                Vector3 RandomPos = new Vector3(
                    Random.Range(A.min.x, A.max.x),
                    A.max.y + 7f,
                    Random.Range(A.min.z, A.max.z)
                    );
                GameObject spawning = Instantiate(TVOROG, RandomPos, Quaternion.Euler(0, 0, 0), TVOROgHide.transform);
            }


        }
        
    }

    void ChangePhase()
    {
        if (currethp <= 800 && currethp >= 600 && Phase == 1)
        {
            Phase = 2;
            for (int i = 0; i < 10; i++)
            {
                BoxCollider randomPart = SpawnArea[Random.Range(0, SpawnArea.Length)];
                
                Bounds B = randomPart.bounds;
                Vector3 RandomPos = new Vector3(
                    Random.Range(B.min.x, B.max.x),
                    B.min.y,
                    Random.Range(B.min.z, B.max.z)
                );
                GameObject spawning = Instantiate(crips, RandomPos, Quaternion.Euler(-90, 0, 0), GhostHide.transform);
                
            }
        }
        if (currethp <= 600 && currethp >= 400 && Phase == 2)
        {
            Phase = 3;
            HealSpawn = true;

            for (int i = 0; i < 20; i++)
            {
                BoxCollider randomPart = SpawnArea[Random.Range(0, SpawnArea.Length)];
                Bounds A = randomPart.bounds;
                Vector3 RandomPos = new Vector3(
                    Random.Range(A.min.x, A.max.x),
                    A.min.y,
                    Random.Range(A.min.z, A.max.z)
                    );
                GameObject spawning = Instantiate(crips, RandomPos, Quaternion.Euler(-90, 0, 0), GhostHide.transform);

            }
            
        }
        if (currethp <= 400 && currethp >= 200 && Phase == 3)
        {
            Phase = 4;
            HealSpawn = true;

            for (int i = 0; i < 40; i++)
            {
                BoxCollider randomPart = SpawnArea[Random.Range(0, SpawnArea.Length)];
                Bounds A = randomPart.bounds;
                Vector3 RandomPos = new Vector3(
                    Random.Range(A.min.x, A.max.x),
                    A.min.y,
                    Random.Range(A.min.z, A.max.z)
                    );
                GameObject spawning = Instantiate(crips, RandomPos, Quaternion.Euler(-90, 0, 0), GhostHide.transform);

            }

        }
        if (currethp <= 200 )
        {
            Phase = 5;
            Crips.Dist_e = 1000f;
            HealSpawn = true;

            SpawnParts();
            Destroy(GhostHide);
            Destroy(TVOROgHide);
            GhostHide = new GameObject("GhostHide");
            TVOROgHide = new GameObject("TVOROgHide");
            transform.position = PartsCenter.transform.position;
            Plocation.root.position = new Vector3(255, 191, 24);
            BossPlate.SetActive(false);
            for (int i = 0; i < 40; i++)
            {
                BoxCollider randomPart = SpawnArea[Random.Range(0, SpawnArea.Length)];
                Bounds A = randomPart.bounds;
                Vector3 RandomPos = new Vector3(
                    Random.Range(A.min.x, A.max.x),
                    A.min.y,
                    Random.Range(A.min.z, A.max.z)
                    );
                GameObject spawning = Instantiate(crips, RandomPos, Quaternion.Euler(-90, 0, 0), GhostHide.transform);

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

    void meteors()
    {
        BoxCollider randomPart = MeteorArea[Random.Range(0, MeteorArea.Length)];
        Bounds C = randomPart.bounds;
        Vector3 RandomPos = new Vector3(
            Random.Range(C.min.x, C.max.x),
            C.min.y,
            Random.Range(C.min.z, C.max.z)
            );
        GameObject Meteor = Instantiate(ProPrefabJectile, RandomPos, randomPart.transform.rotation);
        ProPreJectile projectScript = Meteor.GetComponent<ProPreJectile>();
        projectScript.launch(RandomPos + randomPart.transform.up );

    }

    //void SpawnParts()
    //{
    //    Vector3 Center = SpawnArea[0].bounds.center;
    //    float Start_X = Center.x - (150 / 2) + (15f / 2f);
    //    float Start_Z = Center.z - (150 / 2) + (15f / 2f);
    //    for (int i = 0; i < 10; i++)
    //    {
    //        for (int j = 0; j < 10; j++)
    //        {
    //            float Pos_X = Start_X + (i * 15);
    //            float Pos_Z = Start_Z + (j * 15);
    //            Vector3 SpawnPos = new Vector3(Pos_X, 190, Pos_Z);
    //            Instantiate(parts, SpawnPos, Quaternion.identity);
    //        }
    //    }
    //}

    void SpawnParts()
    {
        Vector3 Center = SpawnArea[0].bounds.center;
        float inner_Radius = 35f;
        int inner_Platforms = 10;
        float outter_Radius = 60f;
        int outter_Platforms = 18;


        for (int i = 0; i < inner_Platforms; i++)
        {
            float angle = i * (2 * Mathf.PI / inner_Platforms);
            SpawnSinglePart(Center, angle, inner_Radius);
        }
        for (int i = 0; i < outter_Platforms; i++)
        {
            float angle = i * (2 * Mathf.PI / outter_Platforms);
            SpawnSinglePart(Center, angle, outter_Radius);
        }
    }

    void SpawnSinglePart(Vector3 Center, float angle, float radius)
    {
        float Pos_X = Center.x + Mathf.Cos(angle) * radius;
        float Pos_Z = Center.z + Mathf.Sin(angle) * radius;
        Vector3 SpawnPos = new Vector3(Pos_X, 190, Pos_Z);
        Vector3 Direction_Center = Center - SpawnPos;
        Direction_Center.y = 0;
        Quaternion rotation = Quaternion.LookRotation(Direction_Center);
        Instantiate(parts, SpawnPos, rotation, PartsCenter.transform);
    }


    void MeteorLine()
    {
        Vector3 ForwardDirection = attackLine.forward;
        Vector3 CenterPoint = attackLine.position;
        Vector3 StartPoint = CenterPoint - attackLine.right * (16 / 2);
        for (int i = 0; i < Meteors; i++)
        {
            Vector3 SpawnPos = StartPoint + attackLine.right * (i * Spacing);
            GameObject Meteor = Instantiate(ProPrefabJectile, SpawnPos, attackLine.rotation);
            ProPreJectile projectScript = Meteor.GetComponent<ProPreJectile>();
            projectScript.launch(SpawnPos + attackLine.up);
        }
        
    }
}
