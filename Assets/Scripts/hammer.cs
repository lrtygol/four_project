using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer : MonoBehaviour
{
    public float F = 1000f;
    




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            Vector3 direction = (other.transform.position - transform.position).normalized;
            Vector3 Vector = new Vector3(0, 0, direction.z);
            Debug.Log(Vector);
            rb.AddForce(Vector * F, ForceMode.Impulse);
        }
    }
}
