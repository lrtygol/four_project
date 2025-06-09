using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float speed = 70f;
    
    void Update()
    {
        transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
    }
}
