using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    private bool isTriggered = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTriggered)
        {
            if (collision.transform.CompareTag("Bike") || collision.transform.root.CompareTag("Player"))
            {
                isTriggered = true;
                GlobalVariables.CollectedKeys += 1;
                Debug.LogError("Key Collected!");
            }
        }
    }
}