using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class sfera : MonoBehaviour
{
    public static event Action Oncollected;







    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag ("Player"))
        {
            Oncollected?.Invoke ();
            Destroy(gameObject);
        }
    }

}
