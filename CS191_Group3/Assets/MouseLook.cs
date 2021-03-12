using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSensivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    float yRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        yRotation -= mouseX;
        yRotation = Mathf.Clamp(-90f, 0f, yRotation);

        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);


        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        playerBody.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.right * mouseY);
    }
}