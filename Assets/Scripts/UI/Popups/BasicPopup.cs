using UnityEngine;
using UnityEngine.UI;

public abstract class BasicPopup : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private PopupTypes _popupType;
    [SerializeField] private Button _closeButton;

    public bool isActive;

    public PopupTypes PopupType => _popupType;

    public virtual void SetInitData(object data) { }

    public void Init()
    {
    }

    public virtual void Subscribe()
    {
        if(_closeButton != null)
        {
            _closeButton.onClick.AddListener(ClosePressed);
        }
    }

    public virtual void Unsubscribe()
    {
        if (_closeButton != null)
        {
            _closeButton.onClick.RemoveAllListeners();
        }
    }

    public virtual void Show()
    {
        isActive = true;
        
        SetPopup();
        _view.SetActive(true);
    }


    public virtual void Hide()
    {
        isActive = false;

        _view.SetActive(false);
        ResetPopup();  
    }

    public virtual void ClosePressed()
    {
        Hide();
    }

    public abstract void SetPopup();

    public abstract void ResetPopup();
}