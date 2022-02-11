using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;


public class Kart : MonoBehaviour
{
    public GameObject[] itemGameObjects;
    public Sprite[] itemSprites;
    public Image yourSprite;

    [SerializeField] Animator itemsPanelANM;

    bool hasItem = false;
    int itemIndex = 0;
    

    void Update()
    {
        
        if (hasItem)
        {
            useItem();
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ItemBox")
        {
            other.gameObject.SetActive(false);

            StartCoroutine(getItem());

      
            yield return new WaitForSeconds(1f);
            other.gameObject.SetActive(true);

        }
    }

    IEnumerator getItem()
    {

        if (!hasItem)
        {
            itemIndex = Random.Range(0, itemGameObjects.Length);
            yourSprite.sprite = itemSprites[itemIndex];
            itemsPanelANM.SetBool("hasItem", true);
            hasItem = true;

            yield return new WaitForSeconds(4f);

            
        }
    }
    void useItem()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            hasItem = false;
            itemsPanelANM.SetBool("hasItem", false);

            itemGameObjects[itemIndex].SetActive(true);


        }
    }
}
