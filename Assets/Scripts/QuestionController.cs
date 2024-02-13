using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionController : MonoBehaviour
{
    public TMP_Text questionText;
    public Button redPillButton;
    public Button bluePillButton;

    private Question currentQuestion;

    public void DisplayQuestion(Question question)
    {
        currentQuestion = question;
        questionText.text = question.questionText;
        redPillButton.GetComponentInChildren<TMP_Text>().text = question.redPillText;
        bluePillButton.GetComponentInChildren<TMP_Text>().text = question.bluePillText;
    }

    public void OnRedPillSelected()
    {
        // Update player's awareness level based on the selected pill option
        CarController carController = FindObjectOfType<CarController>();
        carController.IncreaseAwareness(currentQuestion.redPillAwarenessEffect);

        Debug.Log("red button pressed");

        // Deactivate the popup UI
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnBluePillSelected()
    {
        // Update player's awareness level based on the selected pill option
        CarController carController = FindObjectOfType<CarController>();
        carController.DecreaseAwareness(currentQuestion.bluePillAwarenessEffect);

        Debug.Log("blue button pressed");

        // Deactivate the popup UI
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
