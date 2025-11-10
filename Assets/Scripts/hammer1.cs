using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer1 : MonoBehaviour
{
    public float F = 1000f;
    




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("yes");
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            Vector3 direction = transform.forward;
            rb.AddForce(direction * F, ForceMode.Impulse);
        }
    }
}
