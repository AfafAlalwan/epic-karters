using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Photon.Pun;
using Photon.Realtime;


    public class CheckPointSystem : MonoBehaviour
    {
        [SerializeField] Transform kart1, kart2;
        [SerializeField] TMP_Text rank1, rank1Text, rank2, rank2Text;
        [SerializeField] TMP_Text p1LapCountText, p2LapCountText;

        [SerializeField] Transform nextCP;

        float kart1Dist, kart2Dist;
        float firstPlace, secondPlace;
        int player1Position, player2Position;
        int kart1Laps, kart1CPs, kart2Laps, kart2CPs;


    void Update()
        {
            CheckRank();
            rankText();
        }


        void CheckRank()
        {
               
            kart1Laps = kart1.GetComponent<CheckPoint>().currentLap;
            kart1CPs = kart1.GetComponent<CheckPoint>().currentCP;
            
            kart2Laps = kart2.GetComponent<CheckPoint>().currentLap;
            kart2CPs = kart2.GetComponent<CheckPoint>().currentCP;

            if (kart1Laps > kart2Laps ||
                (kart1Laps == kart2Laps && kart1CPs > kart2CPs))
            {
                player1Position = 1;
                player2Position = 2;
            }
            else if (kart1Laps == kart2Laps && kart1CPs == kart2CPs)
            {
                nextCP = kart1.GetComponent<CheckPoint>().currentCPTransform;
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
        void rankText()
        {
            if (kart1Laps != 3)
                p1LapCountText.text = $"{kart1Laps + 1}/3";
            if (kart2Laps != 3)
                p2LapCountText.text = $"{kart2Laps + 1}/3";



            rank1.text = player1Position.ToString();
            rank2.text = player2Position.ToString();

            if (player1Position == 1)
            {
                rank1Text.text = "st";
                rank2Text.text = "nd";
            }
            else
            {
                rank1Text.text = "nd";
                rank2Text.text = "st";

            }

        }
    }


