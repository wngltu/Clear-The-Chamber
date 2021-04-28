using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalRotate : MonoBehaviour
{
    public static crystalRotate Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 150 * Time.deltaTime, 0);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
