using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupHandler : MonoBehaviour
{
    public delegate void PopupEvent(bool active);

    public event PopupEvent OnPopupChange;

    [SerializeField]
    private Text message = null;
    private void Awake()
    {
        transform.DOScale(Vector3.zero, 0);
        Hide();
        
    }

    public void Show(string message)
    {
        OnPopupChange?.Invoke(true);
        this.message.text = message;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Hide()
    {
        OnPopupChange?.Invoke(false);
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutCubic);
        Debug.Log("Hide called");
        Cursor.lockState = CursorLockMode.Locked;
    }
}
