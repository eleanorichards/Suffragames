using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public LayerMask GroundMask;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer rend;
	private Camera cam;
	private CameraFollow camScript;
    [Range(0.5f, 20.0f)]
    public float fallMultiplier = 2.5f;

    public float maxSpeed = 15.0f;

    [Range(0.5f, 200.0f)]
    public float jumpVelocity = 5.0f;

    [Range(0.1f, 20.0f)]
    public float walkMultiplier = 5.0f;

    [SerializeField]
    private Vector2 moveDirection;

    //private bool isGrounded;

    //AUDIO
    private AudioSource audioSource;

    //PHYSICS
    public bool isGrounded = false;

    public bool onLadder = false;

    /// <summary>
    /// 0 - jump
    /// 1 - land
    /// 2 - Bump (enemy)
    /// </summary>
    public AudioClip[] audioClips;

    // Use this for initialization
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
		cam = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		camScript = cam.GetComponent<CameraFollow> ();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
		TakeInput ();
		ApplyMotion ();
		PlayerOverLimit ();
    }

	private void PlayerOverLimit()
	{
		if (cam.WorldToScreenPoint (transform.position).x >= Screen.width * 0.75f) {
			//cam.transform.SetParent (this.transform);
			//find a way to make camera follow plyer but still move ehwn not
			//cam.transform.position.x = transform.position.x;
			camScript.underLimit = false;
			print ("true");
		} else if(cam.WorldToScreenPoint (transform.position).x < Screen.width * 0.75f && camScript.underLimit == false) {
			//cam.transform.SetParent (null);
			camScript.underLimit = true;
			print ("false");
		}
	}

    private void ApplyMotion()
    {
        //slam down jumpoads
        if (rig.velocity.y < 0)
        {
            rig.AddForce(Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime, ForceMode2D.Impulse);
        }
        else if (rig.velocity.y > 0)
        {
            rig.AddForce(Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime, ForceMode2D.Impulse);
        }

        //Horizontal movement //cap at max speed
        if (rig.velocity.x < maxSpeed && rig.velocity.x > -maxSpeed)
        {
            rig.AddForce(moveDirection * walkMultiplier, ForceMode2D.Impulse);
        }
    }

    private void TakeInput()
    {
        //horizontal movement
        //Jump Input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rig.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        }

        if (!onLadder)
        {
            moveDirection = new Vector2((Input.GetAxis("Horizontal")), 0);
        }
        else
        {
            moveDirection = new Vector2((Input.GetAxis("Horizontal")), Input.GetAxis("Vertical"));
        }
    }
}