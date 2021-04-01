using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    
    public float mouseSensivity = 100f;

    public Transform playerBody;
    float xRotation = 0f;

    private bool disableControl;
    
    public void Awake()
    {
        var popup = FindObjectOfType<PopupHandler>();
        if(popup)
        {
            popup.OnPopupChange += OnPopupAction;
            Debug.Log("popup handler exists");
        }
    }

    private void OnPopupAction(bool active)
    {
        disableControl = active;
        Debug.Log(disableControl + "disablecontrol updated");
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (disableControl) return;
        //Debug.Log(disableControl);
        // Get Mouse Inputs (Mouse Movement)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        // Restrict Up/Down Rotation to 180 Degrees Max
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate the Camera (if up/down)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Rotate the Player Body (if left/right)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}