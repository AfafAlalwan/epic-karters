using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace EpicKarters
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        //When starting a new game this script runs on the loading scene
        //To connect players to the server
 
        void Start()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.OfflineMode = false;
                PhotonNetwork.GameVersion = "0.0.0";
                PhotonNetwork.ConnectUsingSettings();

            }
            else
            {
                Debug.Log("We are already connected");
            }
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            SceneManager.LoadScene("Rooms");
        }
    }
}

