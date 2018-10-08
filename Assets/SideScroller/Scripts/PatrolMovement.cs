using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    private enum MoveDirection
    {
        Forward,
        Backwards
    }

    public enum MovementType
    {
        ReverseDir,
        Looped,
        Random
    }

    public Transform[] patrolPoints;
    public float patrolSpeed = 5.0f;
    public bool shouldRest;
    public float restTime = 2.0f;

    private int currentPoint = 0;
    private float targetDistance = 0.5f;
    private bool resting = false;
    private MoveDirection moveDirection = MoveDirection.Forward;
    public MovementType movementType;

    // Use this for initialization
    private void Start()
    {
        float seed = Time.deltaTime;
        transform.position = patrolPoints[0].position;
    }

    // Update is called once per frame
    private void Update()
    {
        float step = patrolSpeed * Time.deltaTime;
        switch (movementType)
        {
            case MovementType.ReverseDir:
                ReverseDirection();
                break;

            case MovementType.Looped:
                LoopedMovement();
                break;

            case MovementType.Random:
                RandomMovement();
                break;

            default:
                break;
        }
        if (!resting)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, step);
        }
    }

    private void ReverseDirection()
    {
        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < targetDistance)
        {
            if (shouldRest)
            {
                StartCoroutine(RestForSecs());
            }
            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    currentPoint++;
                    if (currentPoint >= patrolPoints.Length)
                    {
                        currentPoint -= 1;
                        moveDirection = MoveDirection.Backwards;
                    }
                    break;

                case MoveDirection.Backwards:
                    currentPoint--;
                    if (currentPoint < 0)
                    {
                        currentPoint = 1;
                        moveDirection = MoveDirection.Forward;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    private void LoopedMovement()
    {
        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < targetDistance)
        {
            if (shouldRest)
            {
                StartCoroutine(RestForSecs());
            }

            currentPoint++;
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
        }
    }

    private void RandomMovement()
    {
        if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < targetDistance)
        {
            if (shouldRest)
            {
                StartCoroutine(RestForSecs());
            }
            currentPoint = Random.Range(0, patrolPoints.Length);
        }
    }

    private IEnumerator RestForSecs()
    {
        resting = true;
        yield return new WaitForSeconds(restTime);
        resting = false;
    }
}