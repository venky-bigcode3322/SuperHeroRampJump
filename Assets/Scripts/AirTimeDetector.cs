using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTimeDetector : MonoBehaviour
{
    public bool isGrounded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer != LayerMask.NameToLayer("Bike"))
        {   
            GameObject obj = GameManager.instance.DustParticlePool.ReleseReusable();
            obj.transform.position = transform.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}