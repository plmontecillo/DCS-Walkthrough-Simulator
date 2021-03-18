using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
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

    public string currentScene;
    public Vector3 pos;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(currentScene + "x"))
        {
            pos = new Vector3(PlayerPrefs.GetFloat(currentScene + "x"),
            PlayerPrefs.GetFloat(currentScene + "y"),
            PlayerPrefs.GetFloat(currentScene + "z"));

            transform.position = pos;
            Debug.Log(transform.position);
        }
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
