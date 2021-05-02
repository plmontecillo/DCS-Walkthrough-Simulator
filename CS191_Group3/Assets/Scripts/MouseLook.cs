//PROGRAMMER: Lyzer Merck B. Bautista
//This script is used to change the perspective of the player as the mouse moves
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //mouse sensitivity
    public float mouseSensivity = 100f;

    // get reference for rotation
    public Transform playerBody;
    float xRotation = 0f;

    //variable to disable this script
    private bool disableControl;
    
    public void Awake()
    {
        //get reference to the popup message
        var popup = FindObjectOfType<PopupHandler>();
        var bulletinPopup = FindObjectOfType<BulletinPopup>();
        if (popup)
        {
            // add event listener
            popup.OnPopupChange += OnPopupAction;
        }
        if (bulletinPopup)
        {
            bulletinPopup.OnPopupChange += OnPopupAction;
        }
    }

    //update disable control status whether popup message is shown/hidden
    private void OnPopupAction(bool active)
    {
        disableControl = active;
    }


    // Update is called once per frame
    void Update()
    {
        //disable control when popup message is active
        if (disableControl) return;

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