//PROGRAMMER: Lyzer Merck B. Bautista
//This script is used by quest colliders to properly show popup message and to change mesh color dependeing on whether they are active or not.

using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    //Mesh variables
    [SerializeField]
    private Color color = Color.yellow;

    //Popup Variables
    public string questName;
    public string questText;

    //Get Renderer (MESH) and Popup Handler (use _ since renderer is a reserved keyword)
    private Renderer _renderer;
    private PopupHandler _popup;

    //Quest Variables
    private string[] quests;
    private int index;


    private void Awake()
    {
        //Get the mesh object and and the popup
        _renderer = GetComponentInParent<Renderer>();
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
                Activate();
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
            if (quests[index] == questName)
            {
                _popup.Show(questText);
                GameManager.Instance.nextQuest();
            }

        }
    }

    //This method sets the color of the quest collider to yellow if it is active
    private void Activate()
    {
        if (_renderer)
            _renderer.material.color = color;
    }

    //This method sets the color of the quest collider to yellow if it is inactive
    private void DeActivate()
    {
        _renderer.material.color = Color.white;
    }


}
