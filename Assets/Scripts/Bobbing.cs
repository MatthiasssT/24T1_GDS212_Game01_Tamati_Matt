using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    public float bobSpeed = 1.0f; // Speed of the bobbing motion
    public float bobHeight = 1.0f; // Height of the bobbing motion
    private Vector3 startPos; // Starting position of the object

    void Start()
    {
        startPos = transform.position; // Store the starting position
    }

    void Update()
    {
        // Calculate the vertical offset based on the sine function and time
        float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        
        // Update the position of the object
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
}
