using UnityEngine;
using UnityEngine.UI;
public class BulletinTrigger : MonoBehaviour
{

    //Mesh variables
    [SerializeField]
    private Color color = Color.yellow;

    //Popup Variables
    public string title;
    public string desc;

    //Get Renderer (MESH) and Popup Handler (use _ since renderer is a reserved keyword)
    private Renderer _renderer;
    private BulletinPopup _popup;

    //showText
    public Text showText;
    public GameObject textObject;

    private void Awake()
    {
        //Get the mesh object and and the popup
        _renderer = GetComponentInParent<Renderer>();
        _popup = FindObjectOfType<BulletinPopup>();
        showText = textObject.GetComponent<Text>();

    }

    //This method checks if the player collides to the quest collider.
    //If yes, check if the quest is active. Show popup message and update quest if so.
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("bulletin board triggered");
        if (_popup)
        {
            if (other.gameObject.tag == "Crosshair")
            {   
                //change color
                _renderer.material.color = color;

                textObject.SetActive(true);
                showText.text = "Press E to show information.";

                if (Input.GetButton("Use"))
                {
                    Debug.Log("E is pressed");
                    _popup.Show(title, desc);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Crosshair")
        {
            _renderer.material.color = Color.white;

            //return default string
            showText.text = "Press E to Enter";
            textObject.SetActive(false);
        }
    }

}
