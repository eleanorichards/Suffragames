using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float camAcceleration = 0.2f;

    public float camSpeed = 1.0f;

    public float camMaxSpeed = 10.0f;
	public float playerMaxSpeed = 50.0f;
	public float playerSpeedIncrease = 0.5f;
    public float camRateOfIncrease = 2.0f;
	public bool underLimit = true;
    private float gameTime = 0.0f;

    private Camera cam;

    private GameObject player;
	private PlayerMovement2D playerMovement;
	float cameraOffset = 25.0f;
    // Use this for initialization

    private void Start()

    {
        cam = this.GetComponent<Camera>();

        player = GameObject.Find("Player");
		playerMovement = player.GetComponent<PlayerMovement2D> ();
	}

    // Update is called once per frame

    private void FixedUpdate()
    {		
		if (cam.WorldToScreenPoint (player.transform.position).x >= Screen.width / 2) 
		{
			IncreaseCamSpeed ();
		}
		//if (underLimit) {
			MoveCamera ();
		//} else {
		//	float playerPos = player.transform.position.x;
		//	transform.position = new Vector3 (playerPos, transform.position.y, transform.position.z);
		//}
    }

	public void MoveCamera()
	{
		float yPos = player.transform.position.y + cameraOffset;
		transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
		
		transform.Translate(camSpeed, 0, 0);
	}

	void IncreaseCamSpeed()
	{
		gameTime += Time.deltaTime;

		if (gameTime >= camRateOfIncrease)
		{
			IncreasePlayerMovementSpeed ();
			if (camSpeed < camMaxSpeed)
			{
				camSpeed += camAcceleration;
			}

			gameTime = 0.0f;
		}

	}

	void IncreasePlayerMovementSpeed()
	{
			if (playerMovement.maxSpeed <= playerMaxSpeed) {
				playerMovement.maxSpeed += playerSpeedIncrease;
				print("player speed increased");
			}

	}
}