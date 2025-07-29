using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizCategory", menuName = "ScriptableObjects/GamePlay/QuizCategory", order = 1)]

public class CategoryConfig : ScriptableObject
{
    [Tooltip("Type of category. It has to be unique!")]
    [SerializeField] private CategoryTypes _categoryType;
    [Tooltip("This is just name and it is not used in any calculations.")]
    [SerializeField] private string _categoryName;
    [SerializeField] private List<QuizQuestionData> quizQuestions;

    public CategoryTypes CategoryType => _categoryType;
    public string CategoryName => _categoryName;
    public List<QuizQuestionData> QuizQuestions => quizQuestions;
}