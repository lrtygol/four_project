using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createSpike : MonoBehaviour
{
    public GameObject Spike;
    private float Timer;
    private float interval = 4f;

    void Update()
    {
        Timer += Time.deltaTime;
        if ( Timer > interval )
        {
            GameObject newSpike = Instantiate( Spike, transform.position, Quaternion.identity );
            
            
            Timer = 0f;
        }
        
    }
}
