using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    public float Fjump = 200f;
    public float F = 500f;
    public float S = 1000f;
    public int speed = 4;
    public int sprint = 10;
    public GameObject DieScreen;
    

    public bool jump = false;

    public float horizontal;
    public float vertical;
    private float VerticalRotation = 0f;

    public Animator anim;

    public Transform cameraPivot;

    [Range(0.1f, 9f)][SerializeField] float sensitivity = 0.5f;
    [Range(0f, 90f)][SerializeField] float yLimited;

    Rigidbody rb;

    void Start()
    {
        

        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {

        float MouseX = Input.GetAxis("Mouse X") * sensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * sensitivity;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Rotate(0, MouseX, 0);
        VerticalRotation -= MouseY;
        VerticalRotation = Mathf.Clamp(VerticalRotation, -yLimited, yLimited);

        cameraPivot.localRotation = Quaternion.Euler(VerticalRotation, 0, 0);

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDerection = (cameraForward * vertical) + (cameraRight * horizontal);
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
        
        
        
        
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("kill"))
        {
            Cursor.lockState = CursorLockMode.None;
            DieScreen.SetActive(true);
            transform.position = new Vector3(149, 151, -41);
        }
        
        //if (collision.gameObject.CompareTag("Boss"))
        //{
        //    transform.position = new Vector3(149, 151, -41);
        //}
        if (collision.gameObject.CompareTag("place") || collision.gameObject.CompareTag("moving"))
        {

            jump = false;
            
        }

        if (collision.gameObject.CompareTag("tp_boss"))
        {
            transform.position = new Vector3(381, -1090, 22);
        }

        //if ((collision.gameObject.CompareTag("hammer")))
        //{
        //    Debug.Log("да");
        //    rb.AddForce(new Vector3(0, 0, S), ForceMode.Impulse);
        //}


        //if ((collision.gameObject.CompareTag("spring")))
        //{
            
        //    rb.AddForce(new Vector3(0, F, 0), ForceMode.Impulse);
        //    jump = true;
        //}

        if (collision.gameObject.CompareTag("moving"))
        {

            transform.parent = collision.transform;
        }
    } 
    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("moving"))
        {
            transform.parent = null;
        }

    }

}
