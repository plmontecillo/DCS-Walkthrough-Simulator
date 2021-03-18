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

    public string prevScene;
    public string currentScene;
    public Vector3 pos;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Start is called before the first frame update
    void Start()
    {
        //set initial position (NOT WORKING)
        if (GameManager.Instance.prevScene == "DCS_Inside" && currentScene == "DCS_Outside")
        {
            StartCoroutine(DelayMovePlayer(new Vector3(145.3551f, 27.35716f, 153.4451f)));
        }
        else if (GameManager.Instance.prevScene == "DCS_Outside" && currentScene == "DCS_Inside")
        {
            StartCoroutine(DelayMovePlayer(new Vector3(-4.512936f, 1.980001f, -17.82328f)));                
        }
        Debug.Log(transform.position);
    }

    private IEnumerator DelayMovePlayer(Vector3 pos)
    {
        yield return null;
        transform.position = pos;
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
