//PROGRAMMER: Lyzer Merck Bautista
//this script is used to display the current task in the canvas.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class taskCanvas : MonoBehaviour
{

    public ContentSizeFitter _contentFitter;
    public Text currentTask;

    public void Awake()
    {
        //add event listener
        GameManager.Instance.OnTaskCanvasChange += changeCurrentTask;
    }

    public void Start()
    {
        currentTask.text = GameManager.Instance.currentTask;
    }

    public IEnumerator RefreshContentFitter()
    {
        _contentFitter.enabled = false;
        yield return null;
        _contentFitter.enabled = true;
    }


    //update the current task
    public void changeCurrentTask(string newTask)
    {
        if (currentTask != null)
        {
            currentTask.text = newTask;
            StartCoroutine(RefreshContentFitter());
            
        }
        
    }
}
