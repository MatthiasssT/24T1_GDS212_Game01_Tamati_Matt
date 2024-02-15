using System.Collections;
using UnityEngine;
using TMPro;

public class NavigationManager : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public Transform destination; // Reference to the destination GameObject
    public TMP_Text distanceText; // Reference to the TextMeshPro Text component to display the distance

    public Transform objectToRotate; // Reference to the object that will rotate towards the destination

    void Update()
    {
        // Calculate the distance between the player and the destination in meters
        float distance = Vector3.Distance(player.position, destination.position);

        // Choose appropriate unit (meters or kilometers) based on the distance
        string unit;
        float distanceToShow;
        if (distance < 1000f)
        {
            unit = "m"; // Use meters if the distance is less than 1000 meters
            distanceToShow = Mathf.Round(distance); // Round distance to the nearest integer
        }
        else
        {
            unit = "km"; // Use kilometers if the distance is 1000 meters or more
            distanceToShow = distance / 1000f; // Convert distance to kilometers
        }

        // Update the text to display the distance with the appropriate unit
        distanceText.text = distanceToShow + " " + unit;

        // Rotate the object towards the destination
        RotateObjectTowardsDestination();
    }

    // Method to rotate the object towards the destination
    void RotateObjectTowardsDestination()
    {
        // Calculate the direction to the destination
        Vector3 directionToDestination = destination.position - objectToRotate.position;
        directionToDestination.y = 0f; // Ignore changes in the y-axis to keep the object level

        // Calculate the rotation to face the destination
        Quaternion targetRotation = Quaternion.LookRotation(directionToDestination);

        // Lock the x-axis rotation to 90 degrees
        targetRotation.eulerAngles = new Vector3(90f, targetRotation.eulerAngles.y, 0f);

        // Apply the rotation to the object
        objectToRotate.rotation = targetRotation;
    }
}
