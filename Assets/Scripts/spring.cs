using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spring : MonoBehaviour
{
    public float F = 500f;
    private Animator push;







    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            Vector3 direction = transform.up;
            rb.AddForce(direction * F, ForceMode.Impulse);
            push = GetComponent<Animator>();
            push.SetTrigger("Input");
        }
    }

    

}
