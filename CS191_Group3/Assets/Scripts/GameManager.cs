//Programmer: Lyzer Merck B. Bautista
//This script is used by the Game Manager Empty Object that serves as singleton where the previous scene name is stored.
//The Game Manager Empty Object is passed across all scenes to update necessary values for the player position.
//Previous scene is used to determine the position of the player in the new scene
//This script is also used to keep track of information regarding the quests.

using UnityEngine;

public class GameManager : MonoBehaviour
{
    //make accessible to other scripts
    public static GameManager Instance { get; private set; }
    //Previous Scene
    public string prevScene;
    //Status of the popup message
    public bool isPopupActive = false;

    //delagate-event for quests
    public delegate void ChangeQuestEvent(string questName);
    public event ChangeQuestEvent OnChangeQuestEvent;
    
    //Array of quests
    public string[] quests;
    //Index of quest
    public int questIndex;
    void Awake()
    {
        //If not yet initialized,
        if (Instance == null)
        {
            //refer to this and do not destroy
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //Quests should be added here:
            //Format: quests[questIndex] = "Quest Title";
            quests = new string[4];     //Make sure to change the array size
            quests[0] = "Introduction";
            quests[1] = "ToLecHall";
            quests[2] = "ToTLC";
            quests[3] = "End";

            //Initially quest index is 0 (first quest)
            questIndex = 0;
        }
        else
        {
            //if we already have an instance, destroy this instance
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        //Invoke the change quest event
        //This will call methods on quest trigger script to repond to the first quest
        OnChangeQuestEvent?.Invoke(quests[questIndex]);
    }

    public void nextQuest()
    {
        //If there are still quests, update the current quest and inform the quest triggers
        if(quests[questIndex] != "End")
        {
            questIndex++;
            OnChangeQuestEvent?.Invoke(quests[questIndex]);
        }
    }
}
