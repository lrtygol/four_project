using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int speed = 3;
    public Transform player;
    private float distance;
    private Renderer GhostRenderer;
    private bool invisible = false;
    private Color OriginalCol;
    private float StartTime;
    public float Duration = 2f;
    
    void Start()
    {
        GhostRenderer = GetComponent<Renderer>();
        if (GhostRenderer != null)
        {
            OriginalCol = GhostRenderer.material.color;
        }
    }
    public void StartTransp()
    {
        if (!invisible)
        {
            invisible = true;
            StartTime = Time.time;
        }
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
        if (invisible) 
        {
            float elapsedTime = Time.time - StartTime;
            if (elapsedTime <Duration)
            {
                float t = elapsedTime / Duration;
                float newT = 1f - t;
                Color Col = new Color(OriginalCol.r, OriginalCol.g, OriginalCol.b, newAlpha);
                GhostRenderer.material.Color = Col;
            }
        }
    }
}
