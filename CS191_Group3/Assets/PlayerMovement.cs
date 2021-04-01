//Programmer: Lyzer Merck B. Bautista
//This script is responsible for the following:
//      (1): Initial position of the player when a scene is newly loaded
//      (2): Views of the Player depending on mouse inputs
//      (3): Movement of the Player(walk and jump) based on WASD and SPACE


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    //accepts the controller of the Player Game Object
    public CharacterController controller;

    //Velocity Variables
    Vector3 velocity;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    // Ground Check Variables
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    // Variables for initial position
    public string prevScene;
    public string currentScene;
    public Vector3 pos;

    private bool disableControl;

    void Awake()
    {
        //get the name of the current scene
        currentScene = SceneManager.GetActiveScene().name;

        var popup = FindObjectOfType<PopupHandler>();
        if (popup)
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
        //Coroutine is used since transforming the position of the Player Game Object on Start() does not work since it is executed before the first frame is rendered.
        //We need to delay the transform.position

        //From inside to outside
        if (GameManager.Instance.prevScene == "DCS_Inside" && currentScene == "DCS_Outside")
        {
            //set the corresponding position and rotation (HARD CODED)
            StartCoroutine(DelayMovePlayer(new Vector3(124.054f, 26.9026f, 135.874f), new Vector3(0f,338.008f,0f)));
        }
        //From outside to inside
        else if (GameManager.Instance.prevScene == "DCS_Outside" && currentScene == "DCS_Inside")
        {
            //set the corresponding position and rotation (HARD CODED)
            StartCoroutine(DelayMovePlayer(new Vector3(2.79572f, 0.32000f, -19.1289f), new Vector3(0f,178.509f,0f)));                
        }
        //for debugging
        Debug.Log(transform.position);
    }

    //function to delay transform.position
    private IEnumerator DelayMovePlayer(Vector3 pos, Vector3 rot = default(Vector3))
    {
        yield return null;
        transform.position = pos;
        if(rot != default(Vector3))
        {
            transform.rotation = Quaternion.Euler(rot);
        }
        //For debugging
        Debug.Log("Singleton read.");
    }


    // Update is called once per frame
    void Update()
    {
        if (disableControl) return;

        //Check if Player touches the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            //reset velocity
            velocity.y = -2f;                                   
        }

        // Get Movement Inputs (WASD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Apply Movement
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Get Space Bar Input and Check if isGrounded
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //Apply Jump
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
