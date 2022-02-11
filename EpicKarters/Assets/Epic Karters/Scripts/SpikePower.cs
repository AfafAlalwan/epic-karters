
using UnityEngine;
using Photon.Pun;

namespace EpicKarters
{
    public class SpikePower : MonoBehaviourPun
    {
        //To give damage to the Kart which was triggered
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Kart") && PhotonView.Get(other) != PhotonView.Get(this))
            {
                other.gameObject.GetComponent<KartController>().takeDamage = true;
            }
        }
    }

}
