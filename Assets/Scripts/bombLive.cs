using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombLive : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 6f;
    }

    
    //void Update()
    //{
    //    
    //    Rigidbody(Vector3.forward * Time.deltaTime);
    //}
}
