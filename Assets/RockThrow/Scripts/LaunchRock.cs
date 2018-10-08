using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RockThrow
{
    public class LaunchRock : MonoBehaviour
    {
        public float rockGrav = 1.0f;
		public float xLaunchMultiplier;
		public float yLaunchMultiplier;

		[HideInInspector]
        public bool countDownStarted = false;
		[HideInInspector]
        public bool countdownFinished = false;

		private float preparationTime = 3.0f;
		private float currentTime = 0.0f;
        private Vector3 startPos;
        private Rigidbody2D rock;
        private int noOfPresses = 0;
        private CameraFollow camScript;
        private UIManager uiManager;
		private Speedometer speedometer;

        // Use this for initialization
        private void Start()
        {
            rock = GetComponent<Rigidbody2D>();
            rock.gravityScale = 0;
            startPos = transform.position;
            camScript = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
            uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
			speedometer = GameObject.Find ("speedometer").GetComponent<Speedometer> ();
        }

        // Update is called once per frame
        private void Update()
        {
            TakeInput();
            //Countdown Started
            if (countDownStarted && currentTime < preparationTime)
            {
                uiManager.StartCountdown();
                currentTime += Time.deltaTime;
            }
            //Countdown finished
            else if (currentTime >= preparationTime && countdownFinished == false)
            {
                countdownFinished = true;
                rock.gravityScale = rockGrav;
                ThrowRock();
            }
            //Rock stopped moving
            if (countdownFinished && rock.velocity.x <= 0.1f && rock.velocity.y <= 0.1f)
            {
                StartCoroutine(CheckForStationary());
            }
        }

        private void TakeInput()
        {
            if (Input.GetButtonDown("Jump") && !countDownStarted)
            {
                print("starting");
                countDownStarted = true;
            }
            else if (Input.GetButtonDown("Jump") && countDownStarted && !countdownFinished)
            {
                noOfPresses++;
				speedometer.IncreaseTaps ();
            }
        }

        private void ThrowRock()
        {
            print(noOfPresses);
            rock.AddForce(new Vector2(noOfPresses*xLaunchMultiplier, noOfPresses*yLaunchMultiplier), ForceMode2D.Impulse);
			rock.AddForceAtPosition (new Vector3 (0, 1, 0), new Vector3 (-0.2f, 0, 0)); 
        }

        private void ResetGame()
        {
            rock.transform.position = startPos;
            countDownStarted = false;
            countdownFinished = false;
            uiManager.Reset();
            currentTime = 0.0f;
            noOfPresses = 0;
            rock.gravityScale = 0;
        }

        private IEnumerator CheckForStationary()
        {
            yield return new WaitForSeconds(2.0f);
            if (countdownFinished && rock.velocity.x <= 0.1f && rock.velocity.y <= 0.1f)
            {
                ResetGame();
                camScript.ResetCamera();
                uiManager.Reset();
				speedometer.ResetSpeedo ();
                StopAllCoroutines();
            }
        }
    }
}