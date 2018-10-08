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
        private Text hiScoreText;
        private int countdownTime;
        private bool countdownClocking = false;

        public int[] hiscores;
        private string hiScoreString = "";
        // Use this for initialization
        private void Start()
        {
            launchRock = GameObject.Find("Rock").GetComponent<LaunchRock>();
            rockMovement = GameObject.Find("Rock").GetComponent<RockMovement>();
            player = GameObject.Find("Player");
            distanceTravelled = GameObject.Find("DistanceTravelled").GetComponent<Text>();
            centerTitleText = GameObject.Find("TitleText").GetComponent<Text>();
            centerHeaderText = GameObject.Find("HeaderText").GetComponent<Text>();
            hiScoreText = GameObject.Find("HiScoreText").GetComponent<Text>();
            Reset();
        }

        // Update is called once per frame
        private void Update()
        {
            if (launchRock.countdownFinished)
            {
                UpdateScore();
            }
        }

        public void Reset()
        {
            StopCoroutine(UpdateCountdown());
            countdownTime = 3;
            countdownClocking = false;
            centerTitleText.text = "Ready?";
            centerHeaderText.text = "Press Space To Begin";
            
            for(int i = 0; i < 5; i++)
            {
                hiScoreString.Insert(0, "hello");

            }
            hiScoreText.text = hiScoreString;

        }

        public void UpdateScore()
        {
            distanceTravelled.text = (((int)(rockMovement.transform.position.x - player.transform.position.x) + 12) / 10).ToString() + " m";
        }

        public void StartCountdown()
        {
            if (!countdownClocking)
            {
                StartCoroutine(UpdateCountdown());
            }
        }

        private IEnumerator UpdateCountdown()
        {
            countdownClocking = true;
            while (launchRock.countDownStarted && !launchRock.countdownFinished)
            {
                centerHeaderText.text = "";
                centerTitleText.text = countdownTime.ToString();
                yield return new WaitForSeconds(1.0f);
                countdownTime--;
            }

            if (launchRock.countdownFinished)
            {
                ClearCenterText();
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

        public void SetEndMessage()
        {
            centerTitleText.text = "You made it " + (((int)(rockMovement.transform.position.x - player.transform.position.x) + 12) / 10).ToString() + " m";
            centerHeaderText.text = "";
        }
    }
}