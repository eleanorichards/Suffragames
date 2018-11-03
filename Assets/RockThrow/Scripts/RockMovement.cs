using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RockThrow;

namespace RockThrow
{
    public class RockMovement : MonoBehaviour
    {
        private CameraFollow camFollow;
        private Rigidbody2D rockRig;
        private ParticleController particles;
        private UIManager uiManager;
		private Bouncables bouncables;
        [Header("Bouncable Strengths")]
        public float policemanBounciness;

        public float studentBounciness;
        public float suffragetteBounciness;
        private bool inSlapZone = false;
        private bool slappedInTime = false;

		private float travelDistance = 0.0f;
		private int stepsTravelled = 1;

        public float minVelocity = -10.0f;
        public float maxVelocity = 10.0f;
        public float MaxRotationSpeed = 1.0f;

        // Use this for initialization
        private void Start()
        {
            camFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
            rockRig = GetComponent<Rigidbody2D>();
            uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
			bouncables = GameObject.Find ("Bouncables").GetComponent<Bouncables> ();
            particles = GameObject.Find("Particles").GetComponent<ParticleController>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (inSlapZone)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    slappedInTime = true;
					Time.timeScale = 1.0f;
                    uiManager.ClearCenterText();
                }
            }
			if (rockRig.transform.position.x / stepsTravelled >= 1000) {
				stepsTravelled++;
				bouncables.SpawnBouncables (rockRig.transform.position.x + 100.0f);
			}
            Mathf.Clamp(rockRig.velocity.y, minVelocity, maxVelocity);
            Mathf.Clamp(rockRig.angularVelocity, 0.0f, MaxRotationSpeed);
            Debug.Log(rockRig.angularVelocity);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            switch (col.collider.tag)
            {
                case "Ground":
                    camFollow.ShakeCamera(0.1f, 0.2f);
                    break;

                default:
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            switch (col.tag)
            {
                case "Policeman":
                    camFollow.ShakeCamera(0.2f, 1.0f);
                    KnockRock(policemanBounciness);
                    break;

                case "Medical Student":
                    camFollow.ShakeCamera(0.2f, 1.0f);
                    KnockRock(studentBounciness);
                    break;

                case "SuffragetteRadius":
                    SuffragetteSlapSlowmo();
                    break;

                case "Suffragette":
                    SuffragetteSlap();
                    break;

                case "manhole":
                //StopRock();

                default:
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            switch (col.tag)
            {
                case "SuffragetteRadius":
                    ResetSlowMo();
                    inSlapZone = false;
                    break;

                default:
                    break;
            }
        }

        private void SuffragetteSlapSlowmo()
        {
            uiManager.SetHitTimerText();
            Time.timeScale = 0.4f;
            inSlapZone = true;
        }

        private void SuffragetteSlap()
        {
            if (slappedInTime)
            {
                KnockRock(suffragetteBounciness * 1.5f);
				camFollow.ShakeCamera(0.9f, 2.0f);

                slappedInTime = false;
            }
            else
            {
                KnockRock(suffragetteBounciness);
				camFollow.ShakeCamera(0.5f, 1.5f);
            }
            ResetSlowMo();
        }

        private void ResetSlowMo()
        {
            Time.timeScale = 1.0f;
            uiManager.ClearCenterText();
            slappedInTime = false;
            inSlapZone = false;
        }

        private void KnockRock(float strength)
        {

            rockRig.AddForce(new Vector2(strength*0.1f, strength) * (Mathf.Abs(rockRig.velocity.y) + rockRig.velocity.x), ForceMode2D.Impulse);
            particles.FireParticles(rockRig.transform.position);

        }
    }
}