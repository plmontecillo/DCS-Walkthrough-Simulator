using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class BulletinTrigger : MonoBehaviour
{
    //Animation
    private Animator animator;
    public GameObject bboard; // animated model

    //Popup Variables
    public string title;
    public string desc;

    //Get Popup Handler (use _ since renderer is a reserved keyword)
    private BulletinPopup _popup;

    //showText
    public Text showText;
    public GameObject textObject;

    public ContentSizeFitter _contentFitter;

    private void Awake()
    {
        //Get the mesh object and and the popup
        _popup = FindObjectOfType<BulletinPopup>();
        showText = textObject.GetComponent<Text>();

    }

    //This method checks if the player collides to the quest collider.
    //If yes, check if the quest is active. Show popup message and update quest if so.
    private void OnTriggerStay(Collider other)
    {
        if (_popup)
        {
            if (other.gameObject.tag == "Crosshair")
            {   

                textObject.SetActive(true);
                showText.text = "Press E to show information.";
                _contentFitter.enabled = true;

                if (Input.GetButton("Use"))
                {
                    animator.SetTrigger("Interact"); // play animation when interacted with
                    Debug.Log("E is pressed");
                    _popup.Show(title, desc);
                }
            }
        }
    }

    private void Start()
    {
        animator = bboard.GetComponent<Animator>(); // assign bboard Animator component to a variable
    }

    public IEnumerator RefreshContentFitter()
    {
        _contentFitter.enabled = !_contentFitter.enabled;
        yield return null;
        _contentFitter.enabled = !_contentFitter.enabled;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Crosshair")
        {
            //return default string
            showText.text = "Press E to Enter";
            textObject.SetActive(false);
            _contentFitter.enabled = false;
            StartCoroutine(RefreshContentFitter());
        }
    }

}
