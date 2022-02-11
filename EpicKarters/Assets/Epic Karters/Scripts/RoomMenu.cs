using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace EpicKarters
{
    public class RoomMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        InputField createInput, joinInput;

       //TO create a new room with the specified options from the Master
        public void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(createInput.text, roomOptions);
        }

        //To join an existing created room
        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }

        //To load Lobby after joining
        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Lobby");
        }
    }

}
