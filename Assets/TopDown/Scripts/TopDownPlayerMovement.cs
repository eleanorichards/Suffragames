using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerMovement : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer rend;

    [Range(0.5f, 15.0f)]
    public float fallMultiplier = 2.5f;

    public float maxSpeed = 0.0f;

    [Range(0.5f, 15.0f)]
    public float jumpVelocity = 5.0f;

    [Range(0.1f, 2.0f)]
    public float walkMultiplier = 5.0f;

    [SerializeField]
    private Vector2 moveDirection;

    private bool isGrounded;

    private Transform topLeft;
    private Transform bottomRight;

//    private bool player_fired = false;

    private float rotAngle;

    // Use this for initialization
    //AUDIO
    private AudioSource audioSource;

    /// <summary>
    /// 0 - jump
    /// 1 - land
    /// 2 - Bump (enemy)
    /// </summary>
    public AudioClip[] audioClips;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        TakeInput();
        ApplyMotion();
    }

    private void ApplyMotion()
    {
        //Horizontal movement //cap at max speed
        if (rig.velocity.x < maxSpeed && rig.velocity.x > -maxSpeed && rig.velocity.y < maxSpeed && rig.velocity.y > -maxSpeed)
        {
            rig.AddForce(moveDirection * walkMultiplier, ForceMode2D.Impulse);
        }
        if (moveDirection != Vector2.zero)
        {
            rotAngle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.AngleAxis(rotAngle, Vector3.forward);
    }

    private void TakeInput()
    {
        //horizontal movement

        moveDirection = new Vector2((Input.GetAxis("Horizontal")), (Input.GetAxis("Vertical")));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            audioSource.PlayOneShot(audioClips[0]);
        }
    }
}