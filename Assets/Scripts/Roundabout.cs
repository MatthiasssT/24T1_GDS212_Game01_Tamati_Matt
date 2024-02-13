using UnityEngine;

public class Roundabout : MonoBehaviour
{
    private float triggerSpeedThreshold = 80f; // Speed threshold in km/h
    private float delay = 2f; // Delay in seconds before disabling the collider

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float playerSpeedKPH = other.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;

            if (playerSpeedKPH > triggerSpeedThreshold)
            {
                CarController carController = other.GetComponent<CarController>();
                if (carController != null)
                {
                    carController.IncreaseAwareness(5);
                }
            }
            else
            {
                CarController carController = other.GetComponent<CarController>();
                if (carController != null)
                {
                    carController.DecreaseAwareness(2);
                }
            }

            Invoke("DisableCollider", delay);
        }
    }

    private void DisableCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
}
