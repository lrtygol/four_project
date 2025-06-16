using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    public float rayCastDistance = 1.5f;
    public LayerMask ground;
    public Transform eyePos;
    public float height = 1f;
    public float time = 2f;
    Rigidbody rb;
    private bool proc = false;
    void Start()
    {
        rb = GetComponent < Rigidbody >();
    }

    
    void Update()
    {
        if (proc) return;

        RaycastHit hit;
        if (Physics.Raycast(eyePos.position, transform.forward, out hit, rayCastDistance, ground))
        {
            Vector3 normal = hit.normal;
            float angle = Vector3.Angle(normal, -transform.up);
            if (angle >70 && angle <110)
            {
                StartCoroutine(Climbing(hit.point));
            }
        }
    }
    IEnumerator Climbing(Vector3 xPoint)
    {
        proc = true;
        rb.isKinematic = true;
        Vector3 TargetPos = xPoint + height * transform.up;
        float CurTime = 0f;
        while (CurTime < time)
        {
            CurTime += Time.deltaTime;

        }
    }
}
