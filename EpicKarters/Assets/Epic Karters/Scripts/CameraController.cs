using UnityEngine;
using Photon.Pun;

namespace EpicKarters
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        float smoothing, rotSmoothing;
        PhotonView view;
        Transform cameraTransform, playerTransform;
        bool isFollowing;
        

        private void Awake()
        {
            //Find the player which the camera should follow
            view = this.GetComponent<PhotonView>();
            if (view.IsMine) 
            {
                cameraTransform = GameObject.FindWithTag("CameraPivot").transform;
                playerTransform = this.GetComponent<Transform>();
                isFollowing = true;
            }
        }

        private void FixedUpdate()
        {
            if (isFollowing)
            {
                Follow();
            }
            
        }

        //This method gets called to let the camera follow the player
        void Follow()
        {
            if(cameraTransform != null)
            {
                cameraTransform.position = Vector3.Lerp(cameraTransform.position, playerTransform.position, smoothing);
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, playerTransform.rotation, rotSmoothing);
                cameraTransform.rotation = Quaternion.Euler(new Vector3(0, cameraTransform.rotation.eulerAngles.y, 0));
            }
            else
            {
                Debug.LogError("cameraTransform ref is missing");
            }
          
        }
    }
}
