using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject startMenu;

    public GameObject startMenuFirstButton, startMenuClosedButton;

    public void StartMenuOpen()
    {
        startMenu.SetActive(true);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(startMenuFirstButton);
    }

    public void StartMenuClosed()
    {
        startMenu.SetActive(false);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(startMenuClosedButton);
    }
}
