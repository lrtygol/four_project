using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeLive : MonoBehaviour
{

    private Rigidbody SpikeRb;
    private float F = 10f;
    public Vector3 direction;


    void Start()
    {
        
        SpikeRb = GetComponent<Rigidbody>();
        SpikeRb.AddForce(direction * F * 50, ForceMode.Force);
        Destroy(gameObject, 12f);

    }

    
    void FixedUpdate()
    {
        SpikeRb.AddForce(direction * F, ForceMode.Force);

    }
}
