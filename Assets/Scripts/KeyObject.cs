using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            if (other.transform.root.CompareTag("Bike") || other.transform.root.CompareTag("Player"))
            {
                isTriggered = true;
                GlobalVariables.CollectedKeys += 1;
                if (SoundManager.Instance) SoundManager.Instance.PlayKeyCollectionSound();
                Debug.LogError("Key Collected!");
                gameObject.SetActive(false);
            }
        }
    }
}