using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    // Minimum and maximum speed of the car
    public float minSpeed;
    public float maxSpeed;
    private float currentSpeed;

    // Rigidbody of the car and AudioSource for car sound
    private Rigidbody carRb;
    private AudioSource carAudio;

    // Minimum and maximum pitch for the car sound
    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;

    void Start()
    {
        // Getting the AudioSource and Rigidbody components
        carAudio = GetComponent<AudioSource>();
        carRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Update the engine sound
        EngineSound();
    }

    // Function to control the engine sound based on car speed
    void EngineSound()
    {
        // Calculate the current speed of the car
        currentSpeed = carRb.velocity.magnitude;
        // Calculate the pitch based on the car speed
        pitchFromCar = carRb.velocity.magnitude / 60f;

        // Adjust the pitch of the engine sound based on the car speed
        if (currentSpeed < minSpeed)
        {
            carAudio.pitch = minPitch;
        }
        else if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }
        else if (currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }
    }
}
