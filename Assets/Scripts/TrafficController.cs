using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
    public Renderer goLightRenderer; // Reference to the green light renderer
    public Renderer stopLightRenderer; // Reference to the red light renderer

    public bool isGreen = false; // Indicates if the light is currently green

    [SerializeField] private Material greenMaterial; // Material for the green light
    [SerializeField] private Material redMaterial; // Material for the red light
    [SerializeField] private Material offMaterial; // Material for when the light is off

    private WaitForSeconds lightSwitchDelay = new WaitForSeconds(5f); // Delay between light switches

    private void Start()
    {
        // Start the coroutine to switch lights
        StartCoroutine(SwitchLights());
    }

    private IEnumerator SwitchLights()
    {
        while (true)
        {
            // Toggle the lights
            if (isGreen)
            {
                goLightRenderer.material = offMaterial;
                stopLightRenderer.material = redMaterial;
            }
            else
            {
                goLightRenderer.material = greenMaterial;
                stopLightRenderer.material = offMaterial;
            }

            // Update the light state
            isGreen = !isGreen;

            // Wait for the specified delay
            yield return lightSwitchDelay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CarController carController = other.GetComponent<CarController>();
            if (carController != null)
            {
                if (isGreen)
                {
                    // Decrease awareness by 5 if the light is green
                    carController.DecreaseAwareness(5);
                    Debug.Log("Player went through a green light. Awareness decreased.");
                }
                else
                {
                    // Increase awareness by 7 if the light is not green (red or off)
                    carController.IncreaseAwareness(7);
                    Debug.Log("Player went through a red light. Awareness increased.");
                }
            }
        }
    }
}
