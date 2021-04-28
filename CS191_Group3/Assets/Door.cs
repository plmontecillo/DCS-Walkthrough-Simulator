//PROGRAMMER: Sharlene Yap
//This script is used to toggle the opening/closing of doors.

using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        anim = door.GetComponent<Animator>();
    }
    
    void OnTriggerStay(Collider c)
    {
        if (c.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Interact");
        }
    }
 
}
