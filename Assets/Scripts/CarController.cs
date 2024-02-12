using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    // Input axis names
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    // Input values
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentBreakForce;
    [SerializeField] private bool isBreaking;

    // Motor parameters
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    // Speed limit
    [SerializeField] private float maxSpeed;

    // Wheel colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    // Wheel transforms
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    // TextMeshPro Text component to display speed
    public TMP_Text speedText;

    // UI Slider to display awareness level
    public Slider awarenessSlider;

    public int awarenessLevel = 20; // Initial awareness level out of 100

    private bool isIncreasingAwareness = false;

    private void FixedUpdate()
    {
        // Handle input and control
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        // Update the speed display
        UpdateSpeedText();

        // Update the awareness level display
        UpdateAwarenessSlider();

        // Check if the car's speed is over 100 kph
        if (GetComponent<Rigidbody>().velocity.magnitude * 3.6f > 100f)
        {
            // Start increasing awareness level gradually
            if (!isIncreasingAwareness)
            {
                StartCoroutine(IncreaseAwarenessOverTime());
                isIncreasingAwareness = true;
            }
        }
        else
        {
            // Stop increasing awareness level if the speed is below 100 kph
            StopCoroutine(IncreaseAwarenessOverTime());
            isIncreasingAwareness = false;
        }
    }

    // Get input from player
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    // Apply motor force and brake force
    private void HandleMotor()
    {
        // Limit car speed
        if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed)
        {
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        }
        else
        {
            frontLeftWheelCollider.motorTorque = 0f;
            frontRightWheelCollider.motorTorque = 0f;
        }

        currentBreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    // Apply brake force to all wheels
    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }

    // Method to update the speed display
    private void UpdateSpeedText()
    {
        // Calculate the speed in kilometers per hour (KPH)
        float speedKPH = GetComponent<Rigidbody>().velocity.magnitude * 3.6f; // Conversion from m/s to km/h

        // Update the TextMeshPro Text component with the calculated speed
        speedText.text = Mathf.Round(speedKPH).ToString() + " KPH";
    }

    // Steer the wheels based on input
    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    // Update wheel positions and rotations
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    // Update single wheel position and rotation
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    // Update the awareness level display using UI Slider
    private void UpdateAwarenessSlider()
    {
        // Set the value of the UI Slider to the current awareness level
        awarenessSlider.value = awarenessLevel;
    }

    // Method to decrease awareness level
    public void DecreaseAwareness(int amount)
    {
        awarenessLevel -= amount;
        // Clamp the awareness level between 0 and 100
        awarenessLevel = Mathf.Clamp(awarenessLevel, 0, 100);
        Debug.Log("Awareness Level Decreased: " + awarenessLevel);
    }

    // Method to increase awareness level
    public void IncreaseAwareness(int amount)
    {
        awarenessLevel += amount;
        // Clamp the awareness level between 0 and 100
        awarenessLevel = Mathf.Clamp(awarenessLevel, 0, 100);
        Debug.Log("Awareness Level Increased: " + awarenessLevel);
    }

    // Coroutine to increase awareness level gradually if speed is over 100 kph
    private IEnumerator IncreaseAwarenessOverTime()
    {
        while (true)
        {
            // Check if the car's speed is over 100 kph
            if (GetComponent<Rigidbody>().velocity.magnitude * 3.6f > 100f)
            {
                // Increase awareness level by 1 every 2 seconds
                awarenessLevel += 1;
                // Clamp the awareness level between 0 and 100
                awarenessLevel = Mathf.Clamp(awarenessLevel, 0, 100);
            }
            // Wait for 2 seconds before checking again
            yield return new WaitForSeconds(2f);
        }
    }
}
