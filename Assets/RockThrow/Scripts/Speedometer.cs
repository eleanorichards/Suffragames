using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour {

	private float zRotation = 0.0f;
	private float startRotation = 0.0f;
	private GameObject pointer;
	private float maxRotation = -70.0f;
	private float smooth = 0.1f;

	// Use this for initialization
	void Start () {
		pointer = GameObject.Find ("Speedometer Pointer");
		zRotation = pointer.transform.rotation.eulerAngles.z;
		startRotation = pointer.transform.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncreaseTaps()
	{
		if (pointer.transform.rotation.z > maxRotation) 
		{
			
			zRotation -= 10.0f;
			Quaternion target = Quaternion.Euler(0, 0, zRotation);

			pointer.transform.rotation = target;
		}
	}

	public void ResetSpeedo()
	{
		Quaternion target = Quaternion.Euler(0, 0, startRotation);
		pointer.transform.rotation = target;
		zRotation = pointer.transform.rotation.eulerAngles.z;
	}
}
