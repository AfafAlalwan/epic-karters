using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Photon.Pun;
using Photon.Realtime;

namespace EpicKarters
{
    public class LapSystem : MonoBehaviour
    {

        [SerializeField] Transform kart1, kart2;
        [SerializeField] Transform nextCP;
        [SerializeField] TMP_Text rank1, rank1Text, p1LapCountText;
        float kart1Dist, kart2Dist;
        float firstPlace, secondPlace;
        int player1Position, player2Position;
        int kart1Laps, kart1CPs, kart2Laps, kart2CPs;
        public GameObject finishLine;
        public GameObject lapComlpete;


        private void Start()
        {
            assignKarts();
            nextCP = GameObject.Find("cp7").transform; //Assign nextCP to the last check point before the finish line

        }

        void Update()
        {
            CheckRank();
            rankText();
            LapSound();
        }

        //This method is called to check each kart's rank by comparing the distination 
        //between the karts to determine which kart is taking the lead
        void CheckRank()
        {
            if (kart1 != null && kart2 != null)
            {

                kart1Laps = kart1.GetComponent<CheckPoint1>().currentLap;
                kart1CPs = kart1.GetComponent<CheckPoint1>().currentCP;

                kart2Laps = kart2.GetComponent<CheckPoint1>().currentLap;
                kart2CPs = kart2.GetComponent<CheckPoint1>().currentCP;

                if (kart1Laps > kart2Laps ||
                    (kart1Laps == kart2Laps && kart1CPs > kart2CPs))
                {
                    player1Position = 1;
                    player2Position = 2;
                }
                else if (kart1Laps == kart2Laps && kart1CPs == kart2CPs)
                {
                    nextCP = kart1.GetComponent<CheckPoint1>().currentCPTransform;
                    kart1Dist = Vector3.Distance(nextCP.position, kart1.position);
                    kart2Dist = Vector3.Distance(nextCP.position, kart2.position);

                    secondPlace = Mathf.Min(kart1Dist, kart2Dist);
                    firstPlace = Mathf.Max(kart1Dist, kart2Dist);

                    if (kart1Dist == firstPlace)
                    {
                        player1Position = 1;
                        player2Position = 2;
                    }
                    else
                    {
                        player1Position = 2;
                        player2Position = 1;
                    }
                }
                else if (kart1Laps < kart2Laps || kart1CPs < kart2CPs)
                {
                    player1Position = 2;
                    player2Position = 1;
                }
            }
            else  // if karts were null find them and assing them
            {
                assignKarts();
            }
        }

        //this method gets called to update the rank text UI
        public void rankText()
        {
            if (kart1Laps != 3)
                p1LapCountText.text = $"{kart1Laps + 1}/3";

            rank1.text = player1Position.ToString();

            if (player1Position == 1)
            {
                rank1Text.text = "st";
            }
            else
            {
                rank1Text.text = "nd";
            }
        }

        //This method gets called to find the karts when connected to the room and assign them 
        void assignKarts()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Kart");
            foreach (GameObject player in players)
            {
                if (PhotonView.Get(player).IsMine)
                {
                    this.kart1 = player.transform;
                }
                if (!(PhotonView.Get(player).IsMine))
                {
                    this.kart2 = player.transform;
                } 
            }

        }

        //This method gets called to know which sound to play
        void LapSound()
        {
            if (kart1Laps == 3)
            {
                finishLine.SetActive(true);
            }
            if (kart1Laps == 2)
            {
                lapComlpete.SetActive(true);
            }
            if (kart1Laps == 1)
            {
                lapComlpete.SetActive(true);
            }

            if (kart2Laps == 3)
            {
                finishLine.SetActive(true);
            }
            if (kart2Laps == 2)
            {
                lapComlpete.SetActive(true);
            }
            if (kart2Laps == 1)
            {
                lapComlpete.SetActive(true);
            }
        }

    }
}

