//PROGRAMMER: Sharlene Yap and Lyzer Bautista
//This script is used to toggle the opening/closing of doors.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Door : MonoBehaviour
{
    Animator anim;
    public GameObject door;

    //showText
    public Text showText;
    public GameObject textObject;

    public ContentSizeFitter _contentFitter;

    public void Awake()
    {
        //get the text object in canvas
        showText = textObject.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = door.GetComponent<Animator>();
    }
    
    void OnTriggerStay(Collider c)
    {
        if (c.tag == "Player" )
        {
            //show text and set its string
            textObject.SetActive(true);
            showText.text = "Press E to open or close.";
            _contentFitter.enabled = true;

            //if "E" is pressed, activate animation to open/close the door.
            if (Input.GetButtonDown("Use"))
            {
                anim.SetTrigger("Interact");
            }

        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            //return default string
            showText.text = "Press E to Enter";
            textObject.SetActive(false);
            _contentFitter.enabled = false;
            StartCoroutine(RefreshContentFitter());
        }
    }

    public IEnumerator RefreshContentFitter()
    {
        _contentFitter.enabled = !_contentFitter.enabled;
        yield return null;
        _contentFitter.enabled = !_contentFitter.enabled;
    }

}
