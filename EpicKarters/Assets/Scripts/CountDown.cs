using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class CountDown : MonoBehaviour
    {

        public int countdownTime;
        public Text countDownText;
        public GameObject kart1, kart2;


        private void Start()
        {
            StartCoroutine(CountDownToStart());


        }

        IEnumerator CountDownToStart()
        {
            while (countdownTime > 0)
            {
                countDownText.text = countdownTime.ToString();

                yield return new WaitForSeconds(1f);

                countdownTime--;
            }

            countDownText.text = "GO!";

            // start game
            kart1.GetComponent<CarDrive>().enabled = true;
            kart2.GetComponent<CarDrive>().enabled = true;

            yield return new WaitForSeconds(0.5f);

            countDownText.gameObject.SetActive(false);
        }


    }

