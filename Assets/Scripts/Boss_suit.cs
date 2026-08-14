using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Boss_suit : MonoBehaviour
{

    public Transform Plocation;

    void Start()
    {


    }
    void Update()
    {
        LookAtP();
    }



    void LookAtP()
    {
        Vector3 direction = (Plocation.position - transform.position).normalized;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation *= Quaternion.Euler(-90, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 0.5f);
    }

}