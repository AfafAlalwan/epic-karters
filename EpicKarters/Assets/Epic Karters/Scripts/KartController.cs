using UnityEngine;
using Photon.Pun;
using TMPro;

namespace EpicKarters
{
    public class KartController : MonoBehaviour, IPunObservable
    {
        [SerializeField] bool grounded;
        [SerializeField] float turnSpeed, coinSpeed, gravityMultiplier;
        [SerializeField] KeyCode forward, backward, right, left;
        [SerializeField] TMP_Text coinsCountText;
        [SerializeField] ParticleSystem coinsBurst;

        public float speed;
        public int coinsCount;
        Rigidbody rb;
        PhotonView view;
        public bool takeDamage = false;
  

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            coinsCountText = GameObject.Find("Coins Count").GetComponent<TMP_Text>();
            view = this.GetComponent<PhotonView>();
            rb = this.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (view.IsMine)
            {
                Movement();

                if (takeDamage)
                    TakeDamage();

                if (speed <= 50)
                    speed = 50;
              
                if (coinsCount <= 0)
                    coinsCount = 0;
            }
        }


        private void OnCollisionStay(Collision collision)
        {
            grounded = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            grounded = false;
        }

        void OnTriggerEnter(Collider other)
        {
            if (view.IsMine)
            {
                if (other.gameObject.CompareTag("Coins")) //When triggered by coins add to coins as long as it is less than 10
                {
                        if (coinsCount != 10)
                        {
                            coinsCount++;
                            coinsCountText.text = coinsCount.ToString();
                        }

                        if (coinSpeed < 50)
                            coinSpeed = coinSpeed + 5;
                }

                if (other.gameObject.CompareTag("Grass")) //when triggered by Grass slower the speed down
                {
                    speed -= 120f;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (view.IsMine)
            {
                if (other.gameObject.CompareTag("Grass")) //when leaving Grass, give back the speed that was conducted 
                {
                    speed += 20f;
                }
            }
        }
    
        //This method is called for Kart's movement
        void Movement()
        {
            if (Input.GetKey(forward))
            {
                Vector3 forceToAdd = transform.forward;
                Vector3 coinToAdd = transform.forward;
                Vector3 grassToAdd = -transform.forward;
                forceToAdd.y = 0;
                rb.AddForce((forceToAdd * (speed + coinSpeed)));

            }
            else if (Input.GetKey(backward))
            {
                Vector3 forceToAdd = -transform.forward;
                forceToAdd.y = 0;
                rb.AddForce(forceToAdd * (speed + coinSpeed));
            }

            Vector3 localVelo = transform.InverseTransformDirection(rb.velocity);
            localVelo = new Vector3(0, localVelo.y, localVelo.z);
            rb.velocity = new Vector3(transform.TransformDirection(localVelo).x, rb.velocity.y, transform.TransformDirection(localVelo).z);

            if (Input.GetKey(right) && grounded)
            {
                rb.AddTorque(Vector3.up * turnSpeed * 10);
            }
            else if (Input.GetKey(left) && grounded)
            {
                rb.AddTorque(-Vector3.up * turnSpeed * 10);
            }

            rb.AddForce(Vector3.down * gravityMultiplier);
        }

        //This method is called when the player has coins and is colliding with dangerous game objects to take damage
         void TakeDamage()
         {
            if (view.IsMine)
            {
                if (coinsCount <= 0)
                    return;
                else
                {
                    view.RPC("TakeDamageRPC", RpcTarget.All);

                    speed -= 10;
                    coinsCount -= 3;
                    coinsCountText.text = coinsCount.ToString();

                    takeDamage = false;
                }
            }
        }

        //Show damage on this kart to all players
        [PunRPC]
        void TakeDamageRPC()
        {
            coinsBurst.Play();
        }

        //Send and recieve players movement
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(this.speed);
                stream.SendNext(this.takeDamage);
            }

            else if (stream.IsReading)
            {
                speed = (int)stream.ReceiveNext();
                takeDamage = (bool)stream.ReceiveNext();
            }
        }
    }

}
