using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampExitTrigger : MonoBehaviour
{
    private bool _isTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isTriggered)
        {
            _isTriggered = true;
            if (BikeController.instance) BikeController.instance.ActivateFuel();
        }
    }
}