using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

namespace EpicKarters
{
    public class Lobby : MonoBehaviour
    { 
        [SerializeField]
        Text waitingLabel, roomName;
        [SerializeField]
        Button startButton;

        private void Start()
        {
            waitingLabel.enabled = true;
            startButton.enabled = false;
            startButton.gameObject.active = false;

            roomName.text = PhotonNetwork.CurrentRoom.Name;
        }

        private void Update()
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                waitingLabel.enabled = false;
                startButton.enabled = true;
                startButton.gameObject.active = true;

            }
            else
            {
                waitingLabel.enabled = true;
                startButton.enabled = false;
                startButton.gameObject.active = false;

            }
        }

        //Starts the game when the players are connected
        public void StartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
                {
                    SceneManager.LoadScene("Game");
                }
            }
        }
    }
}

