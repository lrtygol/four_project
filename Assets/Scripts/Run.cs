using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    public float Fjump = 200f;
    public int speed = 4;
    public int rot = 110;
    public int sprint = 10;
    public bool jump = false;
    public float horizontal;
    public float vertical;
    public Animator anim;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 moveDerection = new Vector3(horizontal, 0 , vertical);
        if (Input.GetKey(KeyCode.Space)&& jump == false)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector3(0, Fjump, 0), ForceMode.Impulse);
            jump = true;
        }
        if (moveDerection.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Sprint", true);
                anim.SetBool("Walk", false);
                transform.Translate(Vector3.forward * Time.deltaTime * vertical * sprint);

            }
            else
            {
                anim.SetBool("Sprint", false);
                anim.SetBool("Walk", true);
                transform.Translate(Vector3.forward * Time.deltaTime * vertical * speed);
            }

        }
        else
        {
            anim.SetBool("Sprint", false);
            anim.SetBool("Walk",  false);
        }
        
        
        
        
        transform.Rotate(Vector3.up * Time.deltaTime* horizontal * rot);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("kill"))
        {
            transform.position = new Vector3(149, 151, -41);
        }

        if (collision.gameObject.CompareTag("place"))
        {
            jump = false;
        }
    } 
}
