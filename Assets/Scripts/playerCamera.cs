using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    public float mouseSens = 200;

    public Transform playerBody;

    float xrot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens *Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens *Time.deltaTime;

        xrot -= mouseY;
        xrot = Mathf.Clamp(xrot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xrot, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
