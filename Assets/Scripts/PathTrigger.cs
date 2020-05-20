using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTrigger : MonoBehaviour
{
    private bool _isTriggered = false;

    private void OnEnable() => _isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player") && !_isTriggered)
        {
            if (PathLoopManager.Instance) PathLoopManager.Instance.AssignNewPath();
            _isTriggered = true;
        }
    }
}