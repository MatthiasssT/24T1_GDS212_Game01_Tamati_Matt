using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public GameManager gameManager; // Reference to the GameManager script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit the destination!");
            // Call the YouWin method from the GameManager script
            gameManager.YouWin();
        }
    }
}
