//Programmer: Lyzer Merck B. Bautista
//This script is used by the Game Manager Empty Object that serves as singleton where the previous scene name is stored.
//The Game Manager Empty Object is passed across all scenes to update necessary values for the player position.
//Previous scene is used to determine the position of the player in the new scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //make accessible to other scripts
    public static GameManager Instance { get; private set; }
    //Previous Scene
    public string prevScene;

    public string[] quests;
    public int questIndex;
    void Awake()
    {
        //If not yet initialized,
        if (Instance == null)
        {
            //refer to this and do not destroy
            Instance = this;
            DontDestroyOnLoad(gameObject);
            quests = new string[4];
            quests[0] = "Introduction";
            quests[1] = "ToLecHall";
            quests[2] = "ToTLC";
            quests[3] = "End";

            questIndex = 0;
        }
        else
        {
            //if we already have an instance, destroy this instance
            Destroy(gameObject);
        }
    }
}
