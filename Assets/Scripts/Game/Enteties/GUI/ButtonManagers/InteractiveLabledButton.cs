using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// A button with a label that changes color on hover and change a cursor after point on it.
/// </summary>
public class InteractiveLabledButton : MonoBehaviour
{
    [SerializeField] protected Button _button;
    [SerializeField] protected TMP_Text _label;

    [Header("Interaction")]
    [SerializeField] private Color _defaultButton = Color.white;
    [SerializeField] private Color _selectedButton = Color.yellow;
    [SerializeField] private Color _incorectButton = Color.red;
    [SerializeField] private Color _correntButton = Color.green;

    public Button ButtonComponent => _button;

    /// <summary>
    ///  method sets the label text of the button.
    /// </summary>
    /// <param name="text"></param>
    public void SetLabelText(string text)
    {
        if (_label != null)
            _label.text = text;
    }

    public void Select()
    {
        _button.gameObject.GetComponent<Image>().color = _selectedButton;
    }

    public void SetState(bool isCorrect)
    {
        if(isCorrect)
        {
            _button.gameObject.GetComponent<Image>().color = _correntButton;
        }
        else
        {
            _button.gameObject.GetComponent<Image>().color = _incorectButton;
        }
    }

    public void SetDefaultState()
    {
        _button.gameObject.GetComponent<Image>().color = _defaultButton;
    }
}