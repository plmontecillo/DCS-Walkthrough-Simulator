//Programmer: Lyzer Merck B. Bautista
//This script is used by the trigger box collider that handles changing scenes when the player collides on the collider and "E" button is pressed.

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

    private bool isPopupActive;

    void Awake()
    {
        //get the name of the current scene
        currentScene = SceneManager.GetActiveScene().name;

        var popup = FindObjectOfType<PopupHandler>();
        popup.OnPopupChange += UpdatePopupStatus;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Hide 2D text at the beginning
        enterText.SetActive(false);
    }

    private void UpdatePopupStatus(bool active)
    {
        isPopupActive = active;
    }

    //check what collides on the collider
    void OnTriggerStay(Collider plyr)
    {
        //Disable everything if the popup message is shown
        if (isPopupActive)
        {
            enterText.SetActive(false);
            return;
        }
            
        //if tag is Player
        if(plyr.gameObject.tag == "Player")
        {
            //show 2D text
            enterText.SetActive(true);
            //if "E" is pressed
            if(Input.GetButton("Use"))
            {
                Debug.Log("change scene button is pressed");
                //If Game Manager is already initialized
                if(GameManager.Instance != null)
                {
                    //update the prevScene
                    //prevScene is used to know the position of the player in the new scene
                    GameManager.Instance.prevScene = currentScene;
                }
                //change scene
                SceneManager.LoadScene(levelToLoad);
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
