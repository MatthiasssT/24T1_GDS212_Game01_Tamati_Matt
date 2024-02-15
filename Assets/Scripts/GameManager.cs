using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [TextArea(6, 10)]
    public string startMessage; // Message to be typed out
    public TMP_Text messageText; // Reference to the TextMeshPro Text component to display the message
    public GameObject startOfGamePanel; // Reference to the panel GameObject at the start of the game
    public Timer timer; // Reference to the Timer script
    public GameObject gameOverCanvas; // Reference to the Game Over canvas
    public TMP_Text awarenessText; // Reference to the TextMeshPro Text component to display the awareness level
    public TMP_Text awarenessMessageText; // Reference to the TextMeshPro Text component for awareness message

    // Set up color variables for different awareness levels
    public Color blueColor = Color.blue;
    public Color whiteColor = Color.white;
    public Color redColor = Color.red;

    private void Start()
    {
        // Start typing out the message
        StartCoroutine(TypeMessage(startMessage));
    }

    private IEnumerator TypeMessage(string message)
    {
        string typedMessage = "";
        foreach (char character in message)
        {
            // Append the next character to the typed message
            typedMessage += character;

            // Update the message text to show the typed message
            messageText.text = typedMessage;

            // Wait for 0.2 seconds before typing the next character
            yield return new WaitForSeconds(0.2f);
        }

        // Wait for 5 seconds before starting the game
        yield return new WaitForSeconds(2f);

        // Start the game
        StartGame();
    }

    private void StartGame()
    {
        // Deactivate the start of game panel
        startOfGamePanel.SetActive(false);

        // Call the StartTimer method from the Timer script
        timer.StartTimer(); // Example start time and target time, adjust as needed
    }   

    public void YouWin()
    {
        // Find the CarController GameObject in the scene
        GameObject carControllerObject = GameObject.FindWithTag("Player");

        // Ensure carControllerObject is not null
        if (carControllerObject != null)
        {
            // Get the CarController component from the GameObject
            CarController carController = carControllerObject.GetComponent<CarController>();

            // Ensure carController is not null
            if (carController != null)
            {
                // Get the awareness level from the CarController
                int awarenessLevel = carController.GetAwarenessLevel();

                // Activate the Game Over canvas
                gameOverCanvas.SetActive(true);

                // Set awareness text
                awarenessText.text = "Your Awareness Was " + awarenessLevel + "%";

                // Set color and message based on awareness level
                if (awarenessLevel <= 40)
                {
                    awarenessMessageText.color = blueColor;
                    awarenessMessageText.text = "You are blue pill dominant.";
                }
                else if (awarenessLevel <= 60)
                {
                    awarenessMessageText.color = whiteColor;
                    awarenessMessageText.text = "You are neutral.";
                }
                else
                {
                    awarenessMessageText.color = redColor;
                    awarenessMessageText.text = "You are red pill dominant.";
                }

                // Start coroutine to switch scene after 5 seconds
                StartCoroutine(SwitchSceneAfterDelay(5f, "MainMenu"));
            }
            else
            {
                Debug.LogError("CarController component not found on CarController GameObject!");
            }
        }
        else
        {
            Debug.LogError("CarController GameObject not found in the scene!");
        }
    }


    private IEnumerator SwitchSceneAfterDelay(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
