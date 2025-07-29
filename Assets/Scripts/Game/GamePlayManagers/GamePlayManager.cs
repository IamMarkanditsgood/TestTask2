using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GamePlayManager 
{
    public static GamePlayManager Instance { get; private set; }

    [SerializeField] private List<CategoryConfig> _categoryConfigs;
    [SerializeField] private CategoryTypes _gameCategoryQuestions;

    private int _currentQuestionIndex = 0;

    public List<CategoryConfig> CategoryConfigs => _categoryConfigs;

    public CategoryConfig CurrentCategory { get; private set; }

    public int CurrentQuestionIndex => _currentQuestionIndex;

    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception("An instance of GamePlayManager already exists.");
        }
        
    }

    public void Destroy()
    {
        if (Instance != null)
        {
            Instance = null;
        }
        else
        {
            throw new Exception("No instance of GamePlayManager to destroy.");
        }
    }

    public void StartNewGame()
    {
        _currentQuestionIndex = 0;
        SetCurrentCategory(_gameCategoryQuestions);
    }

    /// <summary>
    /// Method to set the current category based on the provided CategoryTypes enum.
    /// </summary>
    /// <param name="category"></param>
    public void SetCurrentCategory(CategoryTypes category)
    {
        CurrentCategory = GetCategoryConfig(category);
    }

    /// <summary>
    /// Mthod to get the current question text from the current category.
    /// </summary>
    /// <returns></returns>
    public string GetCurrentQuestionText()
    {
        return CurrentCategory.QuizQuestions[_currentQuestionIndex].questionText;
    }

    /// <summary>
    /// Method to get the current answers for the current question in the current category.
    /// </summary>
    /// <returns></returns>
    public List<string> GetCurrentAnswers()
    {
        return CurrentCategory.QuizQuestions[_currentQuestionIndex].answers;
    }

    /// <summary>
    /// Method to check if the selected answer is correct for the current question in the current category.
    /// </summary>
    /// <param name="answerIndex"></param>
    /// <returns></returns>
    public bool IsCorrectAnswer(int answerIndex)
    {
        Debug.Log($"Checking answer: {answerIndex} for question index: {_currentQuestionIndex}");
        Debug.Log($"Selected answer: {answerIndex} correct: {CurrentCategory.QuizQuestions[_currentQuestionIndex].correntAnswer}");
        return CurrentCategory.QuizQuestions[_currentQuestionIndex].correntAnswer == answerIndex;
    }

    /// <summary>
    /// Method to check if the current question is the last question.
    /// </summary>
    /// <returns></returns>
    public bool IsLastQuestion()
    {
        return _currentQuestionIndex >= CurrentCategory.QuizQuestions.Count - 1;
    }

    /// <summary>
    /// Method to move to the next question in the current category if posible. If the current question is the last one, it resets to the first question.   
    /// </summary>
    public void NextQuestion()
    {
        _currentQuestionIndex++;
        if (_currentQuestionIndex >= CurrentCategory.QuizQuestions.Count)
        {
            _currentQuestionIndex = 0; // Reset to the first question if we exceed the count
        }
    }

    private CategoryConfig GetCategoryConfig(CategoryTypes categoryType)
    {
        foreach (CategoryConfig categoryConfig in _categoryConfigs)
        {
            if(categoryConfig.CategoryType == categoryType) { return categoryConfig; }
        }
        return null;
    }
}