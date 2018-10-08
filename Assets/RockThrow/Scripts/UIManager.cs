using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RockThrow;

namespace RockThrow
{
	public class UIManager : MonoBehaviour 
	{
		private LaunchRock launchRock;
		private RockMovement rockMovement;
		private GameObject player;
		private Text distanceTravelled;
		private Text centerTitleText;
		private Text centerHeaderText;
		private int countdownTime;
		private bool countdownClocking = false;

		// Use this for initialization
		void Start () 
		{
			launchRock = GameObject.Find ("Rock").GetComponent<LaunchRock> ();
			rockMovement = GameObject.Find ("Rock").GetComponent<RockMovement> ();
			player = GameObject.Find ("Player");
			distanceTravelled = GameObject.Find ("DistanceTravelled").GetComponent<Text> ();
			centerTitleText = GameObject.Find ("TitleText").GetComponent<Text> ();
			centerHeaderText = GameObject.Find ("HeaderText").GetComponent<Text> ();
			Reset ();
		}
		
		// Update is called once per frame
		void Update () 
		{
			if (launchRock.countdownFinished) {
				UpdateScore ();
			}
		}

		public void Reset()
		{
			StopCoroutine (UpdateCountdown ());
			countdownTime = 3;
			countdownClocking = false;
			centerTitleText.text = "Ready?";
			centerHeaderText.text = "Press Space To Begin";
		}

		public void UpdateScore()
		{
			distanceTravelled.text = (((int)(rockMovement.transform.position.x - player.transform.position.x) + 12)/10).ToString() + " m";
		}

		public void StartCountdown()
		{
			if (!countdownClocking) 
			{
				StartCoroutine (UpdateCountdown ());
			}
		}

		private IEnumerator UpdateCountdown()
		{
			countdownClocking = true;
			while (launchRock.countDownStarted && !launchRock.countdownFinished) 
			{
				centerHeaderText.text = "";
				centerTitleText.text = countdownTime.ToString ();
				yield return new WaitForSeconds(1.0f);
				countdownTime--;
			}

			if (launchRock.countdownFinished) {
				ClearCenterText ();
			}

		}

		public void SetHitTimerText()
		{
			centerTitleText.text = "Space to protest!";
		}

		public void ClearCenterText()
		{
			centerTitleText.text = "";
			centerHeaderText.text = "";
		}

	}
}