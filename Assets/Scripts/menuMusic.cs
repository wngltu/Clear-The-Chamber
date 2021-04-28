using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<soundManager>().Play("menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
