using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;

namespace EpicKarters
{
    public class CustomObservedComponent : MonoBehaviourPun , IPunObservable
    {

        Vector3 _networkPosition;
        Quaternion _networkRotation;
        [SerializeField] Rigidbody _rb;

        //Custom coded script instead of Photon Transform view script 
        //Which fixed some laggins in Transform's data
   
        public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(this._rb.position);
                stream.SendNext(this._rb.rotation);
                stream.SendNext(this._rb.velocity);
            }
            else if(stream.IsReading)
            {
                _networkPosition = (Vector3)stream.ReceiveNext();
                _networkRotation = (Quaternion)stream.ReceiveNext();
                _rb.velocity = (Vector3)stream.ReceiveNext();

                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
                _networkPosition += (this._rb.velocity * lag);
            }
        }

        private void FixedUpdate()
        {
            if (!photonView.IsMine)
            {
                _rb.position = Vector3.MoveTowards(_rb.position, _networkPosition, Time.fixedDeltaTime);
                _rb.rotation = Quaternion.RotateTowards(_rb.rotation, _networkRotation, Time.fixedDeltaTime * 100.0f);
            }
        }
    }

}
