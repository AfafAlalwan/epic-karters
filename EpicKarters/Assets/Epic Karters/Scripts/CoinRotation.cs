using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EpicKarters
{
    public class CoinRotation : MonoBehaviour
    {
        void Update()
        {
            transform.Rotate(0, 90 * Time.deltaTime * 2, 0); //Animation for coins
        }

        // When colliding with the kart disable for 10 seconds 
        private IEnumerator OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Kart"))
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;

                yield return new WaitForSeconds(10f);

                this.gameObject.GetComponent<BoxCollider>().enabled = true;
                this.gameObject.GetComponent<MeshRenderer>().enabled = true;

            }
        }
    }
}

