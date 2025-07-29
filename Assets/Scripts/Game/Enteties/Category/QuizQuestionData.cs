using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuizQuestionData 
{
    public string questionText;
    public List<string> answers;
    public int correntAnswer;
}