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

    void Awake()
    {
        //get the name of the current scene
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Coroutine is used since transforming the position of the Player Game Object on Start() does not work since it is executed before the first frame is rendered.
        //We need to delay the transform.position

        //From inside to outside
        if (GameManager.Instance.prevScene == "DCS_Inside" && currentScene == "DCS_Outside")
        {
            //set the corresponding position (HARD CODED)
            StartCoroutine(DelayMovePlayer(new Vector3(145.3551f, 27.35716f, 153.4451f)));
        }
        //From outside to inside
        else if (GameManager.Instance.prevScene == "DCS_Outside" && currentScene == "DCS_Inside")
        {
            //set the corresponding position (HARD CODED)
            StartCoroutine(DelayMovePlayer(new Vector3(-4.512936f, 1.980001f, -17.82328f)));                
        }
        //for debugging
        Debug.Log(transform.position);
    }

    //function to delay transform.position
    private IEnumerator DelayMovePlayer(Vector3 pos)
    {
        yield return null;
        transform.position = pos;
        //For debugging
        Debug.Log("Singleton read.");
    }


    // Update is called once per frame
    void Update()
    {
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
