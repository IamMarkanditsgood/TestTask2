using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BasicScreen
{
    [SerializeField] private RectTransform _answerButtonsContent;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private TMP_Text _progresText;
    [SerializeField] private float _delayBeforeNextQuestion;
    [SerializeField] private Button _replyButton;

    private List<InteractiveLabledButton> _answerButtons = new();
    private GamePlayManager _gamePlayManager;

    private bool _canSelectAnswer;

    private int _currentAnswer;

    private List<bool> _result = new();

    public override void Subscribe()
    {
        base.Subscribe();
        _replyButton.onClick.AddListener(Reply);
    }

    public override void UnSubscribe()
    {
        base.UnSubscribe();
        _replyButton.onClick.RemoveListener(Reply);
    }

    public override void ResetScreen()
    {
        _gamePlayManager = null;

        if (_answerButtons.Count != 0)
            _answerButtons[_currentAnswer].SetDefaultState();

        ResetCategoryButtons();
    }

    public override void SetScreen()
    {
        StartGame();
    }

    private void StartGame()
    {
        _gamePlayManager = GamePlayManager.Instance;
        _gamePlayManager.StartNewGame();

        _result.Clear();

        NextQuestion();

        _canSelectAnswer = true;
    }

    private void NextQuestion()
    {
        if(_answerButtons.Count != 0)
            _answerButtons[_currentAnswer].SetDefaultState();

        _replyButton.interactable = false;
        _currentAnswer = 0;

        SetQuestion();
        SetAnswers();
        SetProgres();

        _canSelectAnswer = true;
    }

    private void SetQuestion()
    {
        _questionText.text = _gamePlayManager.GetCurrentQuestionText();
    }

    private void SetAnswers()
    {
        ResetCategoryButtons();
        SetCategoryButtons(_gamePlayManager.GetCurrentAnswers());
    }
    private void ResetCategoryButtons()
    {
        foreach (InteractiveLabledButton answerButton in _answerButtons)
        {
            PoolObjectManager.instant.AnserButtonPool.DisableComponent(answerButton);
        }

        UnSubscibeCategoryButtons(_answerButtons);
        _answerButtons.Clear();
    }

    private void SetCategoryButtons(List<string> answers)
    {
        for (int i = 0; i < answers.Count; i++)
        {
            InteractiveLabledButton categoryButton = PoolObjectManager.instant.AnserButtonPool.GetFreeComponent();
            _answerButtons.Add(categoryButton);
            categoryButton.transform.SetParent(_answerButtonsContent, false);
            categoryButton.SetLabelText(answers[i]);
        }

        SubscribeCategoryButtons(_answerButtons);
    }

    private void SetProgres()
    {
        int totalQuestions = _gamePlayManager.CurrentCategory.QuizQuestions.Count;
        int currentQuestion = _gamePlayManager.CurrentQuestionIndex + 1;

        _progresText.text = $"QUESTION {currentQuestion} OF {totalQuestions}";
    }

    private void SubscribeCategoryButtons(List<InteractiveLabledButton> answerButtons)
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            int index = i;
            answerButtons[index].ButtonComponent.onClick.AddListener(() => AnswerPressed(index));
        }
    }

    private void UnSubscibeCategoryButtons(List<InteractiveLabledButton> answerButtons)
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            int index = i;
            answerButtons[index].ButtonComponent.onClick.RemoveAllListeners();
        }
    }

    private void AnswerPressed(int newAnswerIndex)
    {
        if (!_canSelectAnswer) return;

        _answerButtons[_currentAnswer].SetDefaultState();

        _currentAnswer = newAnswerIndex;
        _replyButton.interactable = true;

        _answerButtons[_currentAnswer].Select();
    }

    private void Reply()
    {
        if (!_canSelectAnswer) return;

        _canSelectAnswer = false;

        if (_gamePlayManager.IsCorrectAnswer(_currentAnswer))
        {
            ReplyState(true);
        }
        else
        {
            ReplyState(false);
        }
        StartCoroutine(ContinueWithDelay());
    }

    private void ReplyState(bool isCorrect)
    {
        _answerButtons[_currentAnswer].SetState(isCorrect);
        _result.Add(isCorrect);
    }


    private void HandleNextStep()
    {
        if (_gamePlayManager.IsLastQuestion())
        {
            UIManager.Instance.GetScreen(ScreenTypes.Result).SetInitData(_result);
            UIManager.Instance.ShowScreen(ScreenTypes.Result);
        }
        else
        {
            _gamePlayManager.NextQuestion();
            NextQuestion();
        }
    }

    private IEnumerator ContinueWithDelay()
    {
        yield return new WaitForSeconds(_delayBeforeNextQuestion);

        HandleNextStep();
    }
}