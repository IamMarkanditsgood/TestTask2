using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GamePlayManager _gamePlayManager;
    [SerializeField] private PoolObjectManager _poolObjectManager;
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Subscribe();
    }
    
    private void OnDisable()
    {
        UnSubscribe();
    }

    private void OnDestroy()
    {
        UnSubscribe();
        Destroy();
    }

    private void Init()
    {
        _poolObjectManager.Init();
        _uiManager?.Init();
        _gamePlayManager?.Init();
    }

    private void Destroy()
    {
        _poolObjectManager.DeInit();
        _uiManager?.Destroy();
        _gamePlayManager?.Destroy();
    }

    private void Subscribe()
    {
        _uiManager?.Subscribe();
    }

    private void UnSubscribe()
    {
        _uiManager?.UnSubscribe();
    }
}