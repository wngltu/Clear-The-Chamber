using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorManager : MonoBehaviour
{
    public GameObject[] doorMenus;
    public GameObject[] doors;

    public static doorManager Instance;

    public bool door1IsOpen = false;
    public bool door2IsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        doorMenus[0].SetActive(false);
    }
    public void Door1Open()
    {
        Cursor.lockState = CursorLockMode.None;
        doorMenus[0].SetActive(true);
        door1IsOpen = true;
    }
    public void CloseDoor1Menu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        doorMenus[0].SetActive(false);
        door1IsOpen = false;
    }
    public void Door1Purchase()
    {
        if (playerController.Instance.currentEnergy >= 100)
        {
            doors[0].SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            playerController.Instance.LoseEnergy(100);
            FindObjectOfType<soundManager>().Pause("Chamber1");
            FindObjectOfType<soundManager>().Play("shop");
            doorMenus[0].SetActive(false);
        }
    }
    public void Door1Emergency()
    {
        if (playerController.Instance.currentEnergy < 100)
        {
            Cursor.lockState = CursorLockMode.Locked;
            doors[0].SetActive(false);
            waveManager.Instance.enemiesLeft = 5;
            waveManager.Instance.spawnsLeft = 5;
            waveManager.Instance.UpdateEnemyCount();
        }
    }
    public void Door2Open()
    {
        Cursor.lockState = CursorLockMode.None;
        doorMenus[1].SetActive(true);
        door2IsOpen = true;
    }
    public void CloseDoor2Menu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        doorMenus[1].SetActive(false);
        door2IsOpen = false;
    }
    public void Door2Purchase()
    {
        if (playerController.Instance.currentEnergy >= 250)
        {
            doors[1].SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            playerController.Instance.LoseEnergy(250);
            FindObjectOfType<soundManager>().Pause("Chamber2");
            FindObjectOfType<soundManager>().Play("shop");
            doorMenus[1].SetActive(false);
        }
    }
    public void Door2Emergency()
    {
        if (playerController.Instance.currentEnergy < 250)
        {
            Cursor.lockState = CursorLockMode.Locked;
            doors[0].SetActive(false);
            waveManager.Instance.enemiesLeft = 5;
            waveManager.Instance.spawnsLeft = 5;
            waveManager.Instance.UpdateEnemyCount();
        }
    }
}
