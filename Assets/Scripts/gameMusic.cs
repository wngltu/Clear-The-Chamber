using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMusic : MonoBehaviour
{
    public static gameMusic Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        FindObjectOfType<soundManager>().Play("Chamber1");
    }
    public void Shop()
    {
        FindObjectOfType<soundManager>().Pause("Chamber1");
        FindObjectOfType<soundManager>().Pause("Chamber2");
    }
    public void Chamber2()
    {
        FindObjectOfType<soundManager>().Pause("shop");
        FindObjectOfType<soundManager>().Play("Chamber2");
    }
    public void Chamber3()
    {
        FindObjectOfType<soundManager>().Pause("shop");
        FindObjectOfType<soundManager>().Pause("Chamber3");
    }
}
