using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PoolObjectManager
{
    [Header("Prefabs")]
    [SerializeField] private InteractiveLabledButton _categoryButtonPrefab;
    [SerializeField] private InteractiveLabledButton _answerButtonPrefab;
    [SerializeField] private Image _confettiPrefab;
    [Header("Containers")]
    [SerializeField] private Transform _categoryButtonContainer;
    [SerializeField] private Transform _answerContainer;
    [SerializeField] private Transform _confettiContainer;
    [Header("Pool sizes")]
    [SerializeField] private int _startCategoryButtonSize = 2;
    [SerializeField] private int _startAnswerButtonSize = 2;
    [SerializeField] private int _startConfettiSize = 50;

    //Pools
    public ObjectPool<InteractiveLabledButton> CategoryButtonPool = new();
    public ObjectPool<InteractiveLabledButton> AnserButtonPool = new();
    public ObjectPool<Image> ConfettiPool = new();

    public static PoolObjectManager instant;

    public void Init()
    {
        if (instant == null)
        {
            instant = this;
        }
        InitPoolObjects();
    }

    public void DeInit()
    {
        if (instant != null)
        {
            instant = null;
        }
    }

    private void InitPoolObjects()
    {
        CategoryButtonPool.InitializePool(_categoryButtonPrefab, _categoryButtonContainer, _startCategoryButtonSize);
        AnserButtonPool.InitializePool(_answerButtonPrefab, _answerContainer, _startAnswerButtonSize);
        ConfettiPool.InitializePool(_confettiPrefab, _confettiContainer, _startConfettiSize);
    }
}