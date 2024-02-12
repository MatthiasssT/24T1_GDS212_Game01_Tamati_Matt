using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText; // The text of the question
    public string redPillText; // The text for the red pill option
    public string bluePillText; // The text for the blue pill option
    public int redPillAwarenessEffect; // The awareness effect of choosing the red pill
    public int bluePillAwarenessEffect; // The awareness effect of choosing the blue pill
}

public class QuestionSystem : MonoBehaviour
{
    public Question[] questions; // Array of questions

    // Get a random question from the array
    public Question GetRandomQuestion()
    {
        return questions[Random.Range(0, questions.Length)];
    }
}
