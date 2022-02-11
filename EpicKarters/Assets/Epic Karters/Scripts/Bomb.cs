using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace EpicKarters
{
    public class Bomb : MonoBehaviourPun
    {
        PhotonView view;
        [SerializeField] ParticleSystem explisionEffect;

        private void Awake()
        {
            view = PhotonView.Get(this);
        }

        //When triggered by Kart game object, give it damage and explode
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Kart"))
            {
                other.gameObject.GetComponent<KartController>().takeDamage = true;

                view.RPC("Explode", RpcTarget.All);
            }
        }

        [PunRPC] //Using PunRCP to show explosion for all players
        void Explode()
        {
            StartCoroutine(ExplodeRPC());
        }

        IEnumerator ExplodeRPC()
        {
            PhotonNetwork.Instantiate(explisionEffect.name, this.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(1f);

            if (PhotonNetwork.IsMasterClient) //Only MasterClient can destroy Scene objects 
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
            else
            {
                this.photonView.RPC("DestroyBomb", RpcTarget.MasterClient, this.photonView.ViewID);
            }

        }

        [PunRPC]
        void DestroyBomb(int viewId)
        {
            PhotonNetwork.Destroy(PhotonView.Find(viewId).gameObject);
        }
    }
}

