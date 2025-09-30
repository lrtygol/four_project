using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int speed = 3;
    public Transform player;
    private float distance;
    private Renderer GhostRenderer;
    private Coroutine fadeCor;
    public Material InvisMat;
   
    
    public float Duration = 2f;
    
    void Start()
    {
        GhostRenderer = GetComponent<Renderer>();
        
    }
    public void StartTransp()
    {
        if (fadeCor != null)
        {
            StopCoroutine(fadeCor);
        }
        fadeCor = StartCoroutine(fading(Duration));
    }


    private IEnumerator fading(float Duration)
    {
        float StartTime = Time.time;
        
        speed = 0;
        while (Time.time < StartTime + Duration)
        {

            float t = Time.time - StartTime / Duration;
            float newT = 1f - t;
            Color currentColor = InvisMat.color;
            InvisMat.color = new Color(currentColor.r, currentColor.g, currentColor.b, newT);
            yield return null;
        }
        Destroy(gameObject);
        fadeCor = null;

    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= 8)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation *= Quaternion.Euler(-90, 0, 0);
            transform.rotation = targetRotation;
            
            transform.position += direction * speed * Time.deltaTime;
        }
        

    }
}
