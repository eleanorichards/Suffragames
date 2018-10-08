using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollision : MonoBehaviour
{
    private PlayerMovement2D playerMovement;

    // public bool isGrounded;

    // Use this for initialization
    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement2D>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            //isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            playerMovement.isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") && playerMovement.isGrounded == false)
        {
            playerMovement.isGrounded = true;
            print("Grounded");
        }
    }
}