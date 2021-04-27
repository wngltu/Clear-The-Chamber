using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doorManager : MonoBehaviour
{
    public GameObject door1Menu;
    public GameObject door1;

    public static doorManager Instance;

    public bool door1IsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        door1Menu.SetActive(false);
    }
    public void Door1Open()
    {
        Cursor.lockState = CursorLockMode.None;
        door1Menu.SetActive(true);
        door1IsOpen = true;
    }
    public void CloseDoor1Menu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        door1Menu.SetActive(false);
        door1IsOpen = false;
    }
    public void Door1Purchase()
    {
        if (playerController.Instance.currentEnergy >= 100)
        {
            door1.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            playerController.Instance.LoseEnergy(100);
            door1Menu.SetActive(false);
        }
    }
}
