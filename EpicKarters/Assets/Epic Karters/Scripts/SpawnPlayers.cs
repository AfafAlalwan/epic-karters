using UnityEngine;
using Photon.Pun;

namespace EpicKarters
{
    public class SpawnPlayers : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] Vector3[] startingPositions;

        private void Start()
        {
          //When players enter a racing room instantiate their networked objects
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(player.name, startingPositions[0], Quaternion.identity);
            else
                PhotonNetwork.Instantiate(player.name, startingPositions[1], Quaternion.identity);

        }
    }
}

