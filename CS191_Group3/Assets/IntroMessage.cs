using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMessage : MonoBehaviour
{
    private PopupHandler popup;
    private bool isActive;

    private void Awake()
    {
        popup = FindObjectOfType<PopupHandler>();
        isActive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isActive)
        {
            popup.Show("Welcome to DCS!\n Your first task is to get inside.");
            isActive = false;
        }
        Debug.Log("Intro");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
