using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockThrow;

namespace RockThrow
{
    public class CameraFollow : MonoBehaviour
    {
        private Vector2 velocity;
        public float smoothTime;
		public float minCamPos = 0.0f;
        private Vector3 startPos;
        private GameObject rock;
        private LaunchRock launchRock;
        private float shakeTime;
        private float shakeStrength;

        // Use this for initialization
        private void Start()
        {
            rock = GameObject.Find("Rock");
            launchRock = rock.GetComponent<LaunchRock>();
            startPos = transform.position;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            SmoothFollow();
            CameraShake();
            if (Input.GetButtonDown("Fire1"))
            {
                ShakeCamera(0.5f, 2.0f);
            }
        }

        private void SmoothFollow()
        {
            if (launchRock.countdownFinished)
            {
                Vector2 pos = transform.position;
                Vector2 rockPos = rock.transform.position;
				if (rockPos.y <= minCamPos) {
					rockPos.y = minCamPos;
				}
                transform.position = new Vector3(Mathf.SmoothDamp(pos.x, rockPos.x, ref velocity.x, smoothTime),
                    Mathf.SmoothDamp(pos.y, rockPos.y, ref velocity.y, smoothTime), startPos.z);
            }
        }

        public void ShakeCamera(float time, float strength)
        {
            shakeTime = time;
            shakeStrength = strength;
        }

        private void CameraShake()
        {
            if (shakeTime > 0)
            {
                Vector2 shakePos = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * shakeStrength;
                transform.position = new Vector3(shakePos.x, shakePos.y, startPos.z);
                shakeTime -= Time.deltaTime;
            }
        }

        public void ResetCamera()
        {
            transform.position = startPos;
        }
    }
}