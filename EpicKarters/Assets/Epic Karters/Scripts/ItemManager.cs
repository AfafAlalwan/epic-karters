using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;

namespace EpicKarters
{
    public class ItemManager : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] GameObject[] itemGameObjects; //holds game objects of the items
        public Sprite[] itemsSprites;                 //holds the sprites of the game object
        Image yourSprite;                             //displays the current owned item
        [SerializeField] Animator itemsPanelAnm;
        [SerializeField] GameObject bomb;            //bomb prefab for instantiatiion
        [SerializeField] Vector3 bombPos;            //pos of instantiated bomb
        [SerializeField] ParticleSystem boost;

        PhotonView view;
        bool hasItem = false;
        int itemIndex;
        bool display = true;
        GameObject itemBox, item;

        private void Awake()
        {
            itemsPanelAnm = GameObject.Find("Item's Background").GetComponent<Animator>();
            yourSprite = GameObject.Find("yourSprite").GetComponent<Image>();
            view = PhotonView.Get(this);

        }

        private void Update()
        {
            if (view.IsMine)
            {
                if (hasItem)
                {
                    useItem();
                }

                if(item == itemGameObjects[0])
                {
                    if (item.activeInHierarchy)
                    {
                        StartCoroutine(SpikesTime());
                    }
                }

                if(item == itemGameObjects[1])
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        HideObject();
                        bombPos = this.transform.position - (transform.forward * 3f);

                        
                        if (PhotonNetwork.IsMasterClient) //Only MasterClient can instantiage sceneObjects
                        {
                            PhotonNetwork.Instantiate(bomb.name, bombPos, Quaternion.identity);
                        }
                        else
                        {
                            view.RPC("CreateBomb", RpcTarget.MasterClient);
                        }
                    }
                }

                if (item == itemGameObjects[2])
                {
                    if (item.activeInHierarchy)
                    {
                        StartCoroutine(SpeedUp());

                    }
                }
            }
        }

        [PunRPC]
        void CreateBomb()
        {
            PhotonNetwork.Instantiate(bomb.name, bombPos, Quaternion.identity);
        }

        private IEnumerator OnTriggerEnter(Collider other)
        {
            if (view.IsMine)
            {   //When triggered by ItemBox disactivate the itembox for 3 seconds
                if(other.gameObject.tag == "ItemBox")
                {
                    display = false;
                    other.gameObject.SetActive(false);
                   
                    StartCoroutine(getItem());

                    yield return new WaitForSeconds(3f);

                    display = true;
                    other.gameObject.SetActive(true);                    

                }
            }
        }

        //This method is runned When the player doesn't have an item and collides with an itemBox
        //to get a random item from items list
        IEnumerator getItem()
        {
            if (view.IsMine)
            {
                if (!hasItem && !itemGameObjects[itemIndex].activeInHierarchy)
                {
                    itemIndex = Random.Range(0, itemGameObjects.Length);
                    yourSprite.sprite = itemsSprites[itemIndex];
                    itemsPanelAnm.SetBool("hasItem", true);
                    hasItem = true;
                    item = itemGameObjects[itemIndex];

                    yield return new WaitForSeconds(4f);
                }
            }
           
        }

        //when the player has an item this method is called to use the item 
        void useItem()
        {
            if (view.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    itemsPanelAnm.SetBool("hasItem", false);

                    view.RPC("ItemDisplayRPC", RpcTarget.All, itemIndex);
                    hasItem = false;
                }
            }
        }

        [PunRPC] //To display the item to all players
        void ItemDisplayRPC(int index)
        {
            itemGameObjects[index].SetActive(true);
        }

        void HideObject()
        {
            view.RPC("HideObjectRPC", RpcTarget.All, itemIndex);
        }

        [PunRPC] //To hide the item from all players
        void HideObjectRPC(int index)
        {
            itemGameObjects[index].SetActive(false);
        }

        //To speed up when a mushroom's item is used
        IEnumerator SpeedUp()
        {
            this.gameObject.GetComponent<KartController>().speed += 1;
            boost.Play();

            yield return new WaitForSeconds(1.5f);

            HideObject();
            this.gameObject.GetComponent<KartController>().speed -= 1;
        }

        //To disable spikes game object after 10 seconds from using it
        IEnumerator SpikesTime()
        {
            yield return new WaitForSeconds(10f);
            HideObject();
        }

        //To send and reviece players actions 
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(this.hasItem);
                stream.SendNext(this.display);
                stream.SendNext(this.itemIndex);
            }

            else if (stream.IsReading)
            {
                hasItem = (bool)stream.ReceiveNext();
                display = (bool)stream.ReceiveNext();
                itemIndex = (int)stream.ReceiveNext();
            }
        }

    }

}
