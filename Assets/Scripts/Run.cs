using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Run : MonoBehaviour
{
    public float speed = 4f;
    public float sprint = 10f;
    public float jumpForce = 200f;
    public Transform cameraPivot;

    [Range(0.1f, 9f)][SerializeField] private float sensitivity = 0.5f;
    [Range(0f, 90f)][SerializeField] private float yLimited = 80f;

    public health health;
    public int maxHp = 100;
    public int hp = 100;
    public float damageCD = 0.5f;
    private float nextDmgTime;

    public AudioSource playerAudioSource;
    public AudioSource walkAudioSource;
    public AudioClip jumpClip;
    public AudioClip damageClip;

    public VideoPlayer videoPlayer;

    public Animator anim;

    public bool blocker = false;
    public bool isAttacking = false;
    public bool isBlocking = false;

    private Rigidbody rb;
    private float verticalRotation = 0f;
    private bool isGrounded = false;
    private float currentSpeed;
    private float currentSprint;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetSpeed();

        hp = maxHp;
        if (health != null)
        {
            health.set_health(hp);
        }
    }

    private void Update()
    {
        if (blocker)
        {
            return;
        }

        HandleCameraRotation();
        HandleMovement();
        HandleJump();
    }

    private void HandleCameraRotation()
    {
        if (blocker != true)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            transform.Rotate(0, mouseX, 0);
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -yLimited, yLimited);



            if (cameraPivot != null)
            {
                cameraPivot.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            }
        }
    }

    private void HandleMovement()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Camera.main == null)
        {
            return;
        }

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * vertical) + (cameraRight * horizontal);
        moveDirection.y = 0;
        moveDirection.Normalize();

        if (moveDirection.magnitude > 0.1f)
        {
            if (walkAudioSource != null)
            {
                if (!walkAudioSource.isPlaying && isGrounded)
                {
                    walkAudioSource.Play();
                }
            }

            if (Input.GetKey(KeyCode.LeftShift) && !isBlocking)
            {
                if (anim != null)
                {
                    anim.SetBool("Sprint", true);
                    anim.SetBool("Walk", false);
                }
                transform.position += moveDirection * (Time.deltaTime * currentSprint);

                if (walkAudioSource != null)
                {
                    walkAudioSource.pitch = 1.3f;
                }
            }
            else
            {
                if (anim != null)
                {
                    anim.SetBool("Sprint", false);
                    anim.SetBool("Walk", true);
                }
                transform.position += moveDirection * (Time.deltaTime * currentSpeed);

                if (walkAudioSource != null)
                {
                    walkAudioSource.pitch = 0.8f;
                }
            }
        }
        else
        {
            if (walkAudioSource != null)
            {
                walkAudioSource.Stop();
            }

            if (anim != null)
            {
                anim.SetBool("Sprint", false);
                anim.SetBool("Walk", false);
            }
        }

    }


    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (anim != null)
            {
                anim.SetTrigger("Jump");
            }

            if (rb != null)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            isGrounded = false;

            if (walkAudioSource != null)
            {
                walkAudioSource.Stop();
            }

            if (playerAudioSource != null && jumpClip != null)
            {
                playerAudioSource.PlayOneShot(jumpClip);
            }
        }
    }

    public void SetBlockSpeed(bool blocking)
    {
        isBlocking = blocking;

        if (blocking)
        {
            currentSpeed = 2f;
            currentSprint = 0f;
        }
        else
        {
            currentSpeed = speed;
            currentSprint = sprint;
        }
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
        currentSprint = sprint;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0;
        }

        if (playerAudioSource != null && damageClip != null)
        {
            playerAudioSource.PlayOneShot(damageClip);
        }

        if (health != null)
        {
            health.set_health(hp);
        }
    }

    public void Heal(int amount)
    {
        if (hp >= maxHp)
        {
            return;
        }

        hp += amount;
        if (hp > maxHp)
        {
            hp = maxHp;
        }

        if (health != null)
        {
            health.set_health(hp);
        }
    }

    public void Respawn(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        hp = maxHp;

        if (health != null)
        {
            health.set_health(hp);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("crit_damage"))
        {
            Ghost transparency = other.GetComponent<Ghost>();
            if (transparency != null)
            {
                transparency.StartTransp();
            }

            if (Time.time > nextDmgTime)
            {
                TakeDamage(20);
                nextDmgTime = Time.time + damageCD;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heal"))
        {
            if (hp < maxHp)
            {
                Heal(20);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("place") || collision.gameObject.CompareTag("moving"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("moving"))
        {
            transform.parent = collision.transform;
        }

        if (collision.gameObject.CompareTag("kill"))
        {
            TakeDamage(100);
        }

        if (collision.gameObject.CompareTag("tp_boss") && videoPlayer != null)
        {
            videoPlayer.Play();
            blocker = true;
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("moving"))
        {
            transform.parent = null;
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        transform.position = new Vector3(381f, -1090f, 22f);
        blocker = false;
        vp.Stop();
        vp.loopPointReached -= OnVideoEnd;
    }
}
