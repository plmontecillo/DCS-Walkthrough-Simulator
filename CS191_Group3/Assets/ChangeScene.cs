using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //Text in 2D
    public GameObject enterText;
    //name of the scene to be loaded
    public string levelToLoad;
    //name of the current scene
    public string currentScene;
    //player game object
    private GameObject playerObj = null;


    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Hide 2D text at the beginning
        enterText.SetActive(false);

    }

    //check what collides on the collider
    void OnTriggerStay(Collider plyr)
    {
        //if tag is Player
        if(plyr.gameObject.tag == "Player")
        {
            //show 2D text
            enterText.SetActive(true);
            if(Input.GetButton("Use"))
            {

                //Get player's position
                if (playerObj == null)
                {
                    playerObj = GameObject.FindGameObjectWithTag("Player");
                }
                Vector3 playerPosition = playerObj.transform.position;
                //Save player's position
                PlayerPrefs.SetFloat(currentScene + "x", playerPosition.x);
                PlayerPrefs.SetFloat(currentScene + "y", playerPosition.y);
                PlayerPrefs.SetFloat(currentScene + "z", playerPosition.z);
                PlayerPrefs.Save();

                Debug.Log("X: " + PlayerPrefs.GetFloat(currentScene + "x") + "Y: " + PlayerPrefs.GetFloat(currentScene + "y") + "Z: " + PlayerPrefs.GetFloat(currentScene + "z"));

                //If "E" is pressed, change scene
                SceneManager.LoadScene(levelToLoad);

                Debug.Log("New Scene is loaded");
            }
        }
    }

    //If the object does not collide anymore
    void OnTriggerExit(Collider plyr)
    {
        //if the object is a player
        if(plyr.gameObject.tag == "Player")
        {
            //hide 2D text
            enterText.SetActive(false);
        }
    }

}
