using UnityEngine;
using System;
using Photon.Pun;

namespace EpicKarters
{
    public class CheckPoint1 : MonoBehaviour
    {
        public int currentCP;
        public int currentLap;
        public int numberOfCheckPoints;
        private int totalLap = 3;
        private string _name = "";
        public GameObject startPoint;
        public Transform currentCPTransform;
        public GameObject Kart;
        PhotonView view;
        [SerializeField] Animator FinishAnm;


        private void Awake()
        {
            view = this.GetComponent<PhotonView>();
            Kart = this.gameObject;
            startPoint = GameObject.FindGameObjectWithTag("StartPoint");
            currentCPTransform = GameObject.Find("cp7").transform; //last cp
            FinishAnm = GameObject.Find("Finish Background").GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (view.IsMine)
            {
                //Calculates the checkpoints number 
                if (other.tag == "CheckPoint")
                {
                    currentCPTransform = other.transform;
                    _name = other.transform.name.Substring(2);
                    currentCP = Int32.Parse(_name);

                    if (currentCP == numberOfCheckPoints)
                    {
                        startPoint.SetActive(true);
                    }
                }
                //when last checkpoint is triggered the startpoint will be enabled
                // and when the startpoint is triggered it adds up to the lap's count
                if (other.tag == "StartPoint")
                {
                    currentLap++;
                    currentCP = 0;

                    if (currentLap == totalLap) //Player finishes the race
                    {
                        Kart.GetComponent<KartController>().enabled = false;
                        FinishAnm.SetTrigger("Finish");
                        // TODO: winning scene
                        // TODO: end Game
                    }

                    startPoint.SetActive(false);
                }

                if(other.tag == "Boundaries") // when the kart falls out of the map
                {
                    Kart.transform.position = currentCPTransform.position;
                }
            }
        }
    }
}
