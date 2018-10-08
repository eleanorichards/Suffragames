using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollision : MonoBehaviour
{
    private PlayerMovement2D playerMovement;

    // Use this for initialization
    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement2D>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            playerMovement.onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            playerMovement.onLadder = false;
        }
    }
}