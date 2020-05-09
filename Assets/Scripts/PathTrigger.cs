using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTrigger : MonoBehaviour
{
    bool isTriggered = false;

    private void OnEnable()
    {
        isTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player") && !isTriggered)
        {
            if (PathLoopManager.Instance) PathLoopManager.Instance.AssignNewPath();
            isTriggered = true;
        }
    }
}