using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateParticle : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Deactivate", 2.5f);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
