using System;

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScreen : BasicScreen
{
    [SerializeField] private TMP_Text _correctAnswersText;
    [SerializeField] private TMP_Text _incorectAnswersText;
    [SerializeField] private TMP_Text _completionText;

    [SerializeField] private Button _closeSceen;
    [SerializeField] private Button _doneButton;

    private List<bool> _results = new();

    public override void SetInitData(object data)
    {
        base.SetInitData(data);

        _results = (List<bool>)data;
    }

    public override void Subscribe()
    {
        base.Subscribe();
        _closeSceen.onClick.AddListener(GoHome);
        _doneButton.onClick.AddListener(GoHome);
    }

    public override void UnSubscribe()
    {
        base.UnSubscribe();
        _closeSceen?.onClick.RemoveListener(GoHome);
        _doneButton?.onClick.RemoveListener(GoHome);
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        SetText();
    }

    private void SetText()
    {
        int correctAnswers = 0; 
        int incorectAnswers = 0;
        foreach (var result in _results)
        {
            if(result)
                correctAnswers++;
            else 
                incorectAnswers++;
        }

        _correctAnswersText.text = $"{correctAnswers} questions";
        _incorectAnswersText.text = incorectAnswers.ToString();

        int percentage = (int)Math.Round(((double)correctAnswers / _results.Count) * 100);

        _completionText.text = $"{percentage}%";
    }

    private void GoHome()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.MainMenu);
    }
}
