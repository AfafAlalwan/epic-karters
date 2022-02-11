using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace EpicKarters
{
    public class ItemBox : MonoBehaviourPun
    {
        void Update()
        {
            transform.Rotate(0, 90 * Time.deltaTime * 2, 0); // Animation for the box
        }
    }

}
