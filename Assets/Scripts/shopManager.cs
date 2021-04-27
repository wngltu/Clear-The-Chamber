using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopManager : MonoBehaviour
{
    public GameObject shopMenu;

    public static shopManager Instance;

    public bool open = false;

    private void Start()
    {
        Instance = this;
        shopMenu.SetActive(false);
    }

    public void OpenShop()
    {
        Cursor.lockState = CursorLockMode.None;
        shopMenu.SetActive(true);
        open = true;
    }
    public void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        shopMenu.SetActive(false);
        open = false;
    }

    public void ExtendMags()
    {
        if (playerController.Instance.currentEnergy >= 50)
        {
            gunClass.Instance.IncreaseMag();
            playerController.Instance.LoseEnergy(50);
        }
    }
}
