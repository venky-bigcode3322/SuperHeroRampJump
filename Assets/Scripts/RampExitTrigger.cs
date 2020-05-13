﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampExitTrigger : MonoBehaviour
{
    bool isTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            if (BikeController.Instance) BikeController.Instance.ActivateFuel();
        }
    }
}