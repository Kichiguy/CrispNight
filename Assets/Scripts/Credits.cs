using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsMenu;
    

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }
}
