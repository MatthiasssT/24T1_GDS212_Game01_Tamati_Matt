using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject popupUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player's car has the "Player" tag
        {
            // Activate the popup UI
            popupUI.SetActive(true);

            Time.timeScale = 0f;
        }
    }
}
