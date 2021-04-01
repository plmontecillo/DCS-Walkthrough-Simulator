using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    
    [SerializeField]
    private Color color = Color.yellow;

    public string questName;
    public string questText;

    private Renderer _renderer;
    private PopupHandler _popup;

    private string[] quests;
    private int index;

    private void Awake()
    {
        _renderer = GetComponentInParent<Renderer>();
        _popup = FindObjectOfType<PopupHandler>();
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            quests = GameManager.Instance.quests;
            index = GameManager.Instance.questIndex;
            if (quests[index] == questName)
            {
                Activate();
                Debug.Log(questName);
            }
            else
            {
                DeActivate();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var quests = GameManager.Instance.quests;
        var index = GameManager.Instance.questIndex;

        if (_popup)
        {
            if (quests[index] == questName)
            {
                _popup.Show(questText);
                GameManager.Instance.questIndex++;
                Debug.Log(GameManager.Instance.questIndex);
            }

        }

        DeActivate();
    }

    private void Activate()
    {
        if (_renderer)
        {
            _renderer.material.color = color;
        }
        
    }

    private void DeActivate()
    {
        _renderer.material.color = Color.white;
    }

    private void Update()
    {
        Start();
    }
}
