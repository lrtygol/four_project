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
    public Transform cameraPivot;
    private float VerticalRotation = 0f;
    [Range(0.1f, 9f)][SerializeField] float sensitivity = 0.5f;
    [Range(0f, 90f)][SerializeField] float yLimited;

    Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {

        float MouseX = Input.GetAxis("Mouse X") * sensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * sensitivity;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        transform.Rotate(0, MouseX, 0);
        VerticalRotation = MouseY;
        VerticalRotation = Mathf.Clamp(VerticalRotation, -yLimited, yLimited);
        cameraPivot.localRotation = Quaternion.Euler(VerticalRotation, 0, 0);
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();
        Vector3 moveDerection = (cameraForward * vertical ) + (cameraRight * horizontal );
        moveDerection.y = 0;
        moveDerection.Normalize();




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
                transform.position += moveDerection.normalized * Time.deltaTime * sprint;

            }
            else
            {
                anim.SetBool("Sprint", false);
                anim.SetBool("Walk", true);
                transform.position += moveDerection.normalized * Time.deltaTime * speed;
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
