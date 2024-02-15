using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the TextMeshPro Text component to display the timer
    public TMP_Text titleText; // Reference to the TextMeshPro Text component to display the title

    private bool isTimerRunning = false; // Indicates if the timer is currently running
    private float startTime = 0f; // The starting time of the timer
    private float targetTime = 719f; // The target time for the timer (11 minutes and 59 seconds in seconds)
    private float elapsedTime = 0f; // The elapsed time since the timer started
    [SerializeField] private CarController carController; // Reference to the CarController script

    void Start()
    {
        // Get a reference to the CarController script
        carController = FindObjectOfType<CarController>();

        // Uncomment the following line if you want to start the timer automatically when the script starts
        // StartTimer();
    }

    void Update()
    {
        // Update the timer if it's running
        if (isTimerRunning)
        {
            elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
            if (elapsedTime >= targetTime)
            {
                StopTimer();
            }
        }
    }

    // Method for starting the timer
    public void StartTimer()
    {
        startTime = Time.time - 599f; // Start time is set to 9:59
        isTimerRunning = true;
    }

    // Method for stopping the timer
    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("Game Over"); // Send debug message when the timer reaches the target time

        // Increase awareness by 30
        if (carController != null)
        {
            carController.IncreaseAwareness(30);
        }

        // Change the title text
        if (titleText != null)
        {
            titleText.text = "You were too late...";
        }

        // Call the YouWin function from the GameManager script
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.YouWin();
        }
    }

    // Method for updating the timer text
    private void UpdateTimerText(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        string timerString = string.Format("{0:00}:{1:00}PM", minutes, seconds);
        timerText.text = timerString;
    }
}
