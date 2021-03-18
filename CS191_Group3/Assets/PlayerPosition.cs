using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    public string currentScene;
    public Vector3 pos;

    
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    // Start is called before the first frame update
    

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        //Your Function You Want to Call
        if (PlayerPrefs.HasKey(currentScene + "x"))
        {
            pos = new Vector3(PlayerPrefs.GetFloat(currentScene + "x"),
            PlayerPrefs.GetFloat(currentScene + "y"),
            PlayerPrefs.GetFloat(currentScene + "z"));

            this.transform.position = pos;
            Debug.Log(transform.position);
        }
    }

}
