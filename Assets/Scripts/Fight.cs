using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [Header("Weapon & Shield")]
    public GameObject sword;
    public GameObject shield;

    [Header("Combat Settings")]
    public float blockDuration = 1f;

    [Header("Audio")]
    public AudioClip missSwordClip;

    private Run run;
    private float blockEndTime = 0f;

    private void Awake()
    {
        run = GetComponent<Run>();
    }

    private void Update()
    {
        if (run.blocker) return;

        if (Time.time >= blockEndTime && run.isBlocking)
        {
            if (shield != null) shield.SetActive(false);
            run.SetBlockSpeed(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !run.isAttacking && !run.isBlocking)
        {
            if (run.playerAudioSource && missSwordClip)
            {
                run.playerAudioSource.PlayOneShot(missSwordClip);
            }

            run.isAttacking = true;
            run.anim.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !run.isBlocking && !run.isAttacking)
        {
            if (shield != null) shield.SetActive(true);

            run.isAttacking = false;
            run.anim.SetTrigger("Def");
            run.SetBlockSpeed(true);

            blockEndTime = Time.time + blockDuration;
        }
    }

    public void Enable_Attack()
    {
        if (sword != null) sword.SetActive(true);
    }

    public void Disable_Attack()
    {
        if (sword != null) sword.SetActive(false);
        run.isAttacking = false;
    }
}
