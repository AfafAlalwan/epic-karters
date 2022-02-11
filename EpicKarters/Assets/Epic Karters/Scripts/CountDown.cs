using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EpicKarters {
    public class CountDown : MonoBehaviour
    {
        [SerializeField] int countdownTime;
        [SerializeField] Text countDownText;
        [SerializeField] GameObject startPoint;
        GameObject[] karts;

        private void Start()
        {
            StartCoroutine(CountDownToStart());
        }

        // When players enter the room this will be passed as a parameter of StartCoroutine method
        //To countdown for the race to begin and enables karts movement in KartController script
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
            startPoint.SetActive(false);
            karts = GameObject.FindGameObjectsWithTag("Kart");
            foreach (GameObject kart in karts)
            {
               kart.GetComponent<KartController>().enabled = true;   
            }
            yield return new WaitForSeconds(0.5f);

            countDownText.gameObject.SetActive(false);
        }
    }
}
