using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianMovement : MonoBehaviour {
    private int seed = 0;
    private float xSpeed;
    public float minSpeed = 1.0f;
    public float maxSpeed = 2.0f;

	// Use this for initialization
	void Start ()
    {
        xSpeed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(new Vector3(xSpeed, 0.0f, 0.0f));
    }
}
