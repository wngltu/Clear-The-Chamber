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
        shopMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        open = false;
    }

    public void smgBuy()
    {
        if (playerController.Instance.currentEnergy >= 300)
        {
            weaponSwitch.Instance.wepUnlocked[1] = true;
            playerController.Instance.LoseEnergy(300);
        }
    }
    public void rifleBuy()
    {
        if (playerController.Instance.currentEnergy >= 500)
        {
            weaponSwitch.Instance.wepUnlocked[2] = true;
            playerController.Instance.LoseEnergy(500);
        }
    }
    public void healthRestore()
    {
        if (playerController.Instance.currentEnergy >= 150 && playerController.Instance.currentHealth != playerController.Instance.maxHealth)
        {
            playerController.Instance.currentHealth = playerController.Instance.maxHealth;
            playerController.Instance.UpdateHealth();
            playerController.Instance.LoseEnergy(150);
        }
    }
    public void healthMax()
    {
        if (playerController.Instance.currentEnergy >= 200)
        {
            playerController.Instance.maxHealth += 50;
            playerController.Instance.currentHealth += 50;
            playerController.Instance.UpdateHealthMax();
            playerController.Instance.UpdateHealth();
            playerController.Instance.LoseEnergy(200);
        }
    }
}
