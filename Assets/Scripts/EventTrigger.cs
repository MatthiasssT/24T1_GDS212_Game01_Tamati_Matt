using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject popupUI;
    public string eventName; // Event name to match with the question
    [SerializeField] private BoxCollider boxCollider;

    private QuestionSystem questionSystem;

    private void Start()
    {
        // Find the QuestionSystem object in the scene
        questionSystem = FindObjectOfType<QuestionSystem>();
        if (questionSystem == null)
        {
            Debug.LogError("QuestionSystem object not found in the scene.");
        }

        boxCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the popup UI
            popupUI.SetActive(true);

            // Find a question with the matching event name
            Question matchingQuestion = FindQuestionWithEventName(eventName);
            if (matchingQuestion != null)
            {
                // Populate the UI with data from the matching question
                QuestionController questionController = popupUI.GetComponent<QuestionController>();
                if (questionController != null)
                {
                    questionController.DisplayQuestion(matchingQuestion);
                    Time.timeScale = 0f;
                }
                else
                {
                    Debug.LogError("PopupController component not found on the popupUI GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("No question found with the event name: " + eventName);
            }

            StartCoroutine(RemoveTrigger());

        }
    }

    // Find a question with the matching event name
    private Question FindQuestionWithEventName(string eventName)
    {
        foreach (Question question in questionSystem.questions)
        {
            if (question.eventName == eventName)
            {
                return question;
            }
        }
        return null; // No question found with the specified event name
    }

    IEnumerator RemoveTrigger() 
    { 
    
        yield return new WaitForSeconds(2f);
        boxCollider.enabled = false;

    }
}
