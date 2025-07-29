using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BasicScreen
{
    [Header("Buttons")]
    [SerializeField] private Button _playButton;

    public override void Subscribe()
    {
        base.Subscribe();
        _playButton.onClick.AddListener(PlayButtonPressed);
    }
    public override void UnSubscribe()
    {
        base.UnSubscribe();
        _playButton.onClick.RemoveListener(PlayButtonPressed);
    }

    public override void ResetScreen()
    {
        
    }

    public override void SetScreen()
    {
    }

    private void PlayButtonPressed()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.GamePlay);
    }
}