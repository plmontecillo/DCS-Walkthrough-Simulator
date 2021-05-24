//PROGRAMMER: Lyzer Merck B. Bautista
//This script is used by quest colliders to properly show popup message and to change mesh color dependeing on whether they are active or not.

using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    //Quest indicator model
    public GameObject model;
    private Animator animator;
    private Renderer rend;
    
    //Popup Variables
    public string questName;
    public string questText;

    //Get the Popup Handler
    private PopupHandler _popup;

    //Quest Variables
    private string[] quests;
    private int index;


    private void Awake()
    {
        //Get the mesh object, animator, and the popup
        animator = model.GetComponent<Animator>();
        rend = model.GetComponentInChildren<Renderer>();
        _popup = FindObjectOfType<PopupHandler>();

        //Add event to the delegate when a the current quest is updated
        //CurrentQuestChanged method will be called
        GameManager.Instance.OnChangeQuestEvent += CurrentQuestChanged;

        //Check if the quest is the current active quest and change its color accordingly.
        quests = GameManager.Instance.quests;
        index = GameManager.Instance.questIndex;

        if(quests[index] == questName)
            Activate();
        else
            DeActivate();
       
    }

    //This method updates the color if the quest collider depending on whether it is active or not.
    private void CurrentQuestChanged(string quest)
    {
        if(this != null)
        {
            //need to update the index for the OnTriggerEnter method
            index = GameManager.Instance.questIndex;
            if (quest == questName)
            {
                Activate();
                
            }
            else
                DeActivate();
        }
    }

    //This method checks if the player collides to the quest collider.
    //If yes, check if the quest is active. Show popup message and update quest if so.
    private void OnTriggerEnter(Collider other)
    {
        if (_popup)
        {
            if (other.gameObject.tag == "Player")
            {
                if (quests[index] == questName)
                {
                    _popup.Show(questText);
                    //update the task canvas
                    GameManager.Instance.changeCurrentTask(questText);
                    GameManager.Instance.nextQuest();
                }
            }
        }
    }

    //This method shows the mesh
    private void Activate()
    {
        if (rend) {
            rend.enabled = true;
        }
    }

    //This method hides the mesh
    private void DeActivate()
    {
        rend.enabled = false;
    }


}
