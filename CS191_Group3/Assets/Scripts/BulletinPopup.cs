using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class BulletinPopup : MonoBehaviour
{
    //Delegate-Event for Popup events
    public delegate void PopupEvent(bool active);
    public event PopupEvent OnPopupChange;

    //Variable for the text object of the popup message
    [SerializeField]
    private Text title = null;
    [SerializeField]
    private Text desc = null;

    //Initially hide the popup message
    private void Awake()
    {
        transform.DOScale(Vector3.zero, 0);
        Hide();
    }

    //This method is used to show the popup message together with the message string
    public void Show(string title, string desc)
    {
        OnPopupChange?.Invoke(true);
        this.title.text = title;
        this.desc.text = desc;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
        Cursor.lockState = CursorLockMode.None;
        GameManager.Instance.isPopupActive = true;
    }

    //This method is used to hide the popup message
    //This is called when the "Okay" button is pressed
    public void Hide()
    {
        OnPopupChange?.Invoke(false);
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutCubic);
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.isPopupActive = false;
    }
}

