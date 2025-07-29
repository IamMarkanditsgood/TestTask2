using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BasicScreen : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private ScreenTypes _screenType;

    protected bool _isActive;

    public ScreenTypes ScreenType => _screenType;

    public virtual void SetInitData(object data) { }

    public virtual void Init()
    {
    }

    public virtual void Subscribe()
    {
    }

    public virtual void UnSubscribe()
    {
    }

    public virtual void Show()
    {
        _view.SetActive(true);
        _isActive = true;

        SetScreen();
    }
    public virtual void Hide() 
    {
        ResetScreen();
        EventSystem.current.SetSelectedGameObject(null);
        _view.SetActive(false);
        _isActive = false;
    }

    public abstract void SetScreen();

    public abstract void ResetScreen();
}