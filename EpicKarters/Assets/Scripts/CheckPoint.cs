using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Animations;


public class CheckPoint : MonoBehaviour
{
    public  int currentCP;
    public  int currentLap;
    public int numberOfCheckPoints;
    private int totalLap = 3;
    private string _name = "";

    public GameObject startPoint;
    public Transform currentCPTransform;
    public GameObject kart;
    [SerializeField] Animator FinishAnm;


    private void Start()
    {
        startPoint.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            currentCPTransform = other.transform;
            _name = other.transform.name.Substring(2);
            currentCP = Int32.Parse(_name);

            if(currentCP == numberOfCheckPoints)
            {
                startPoint.SetActive(true);
            }
        }

        if(other.tag == "StartPoint")
        {
            currentLap++;
            currentCP = 0;
            // TODO: lap animation

            if(currentLap == totalLap)
            {
                // he wins

                kart.GetComponent<CarDrive>().enabled = false;
                FinishAnm.SetTrigger("Finish");
                // TODO: winning scene
                // TODO: end Game
            }

            startPoint.SetActive(false);
        }
    }
}
