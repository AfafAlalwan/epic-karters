using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField] GameObject kart;
    void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            Invoke("HideObject", 15);
        }
    }

    void HideObject()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            kart.GetComponent<CarDrive>().speed += 5;
            other.gameObject.GetComponent<CarDrive>().speed -= 10;
            other.gameObject.GetComponent<CarDrive>().coinsCount -= 3;

            //TODO: show animation of coins dropping
        }
    }
   
}
