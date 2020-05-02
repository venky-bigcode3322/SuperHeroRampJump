using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private List<Collider> AllBodyColliders = new List<Collider>();
    private List<Rigidbody> AllBodyRigidbody = new List<Rigidbody>();

    private Rigidbody _rigidbody;

    private void Awake()
    {
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            AllBodyRigidbody.Add(item);
            item.isKinematic = true;
            item.interpolation = RigidbodyInterpolation.Extrapolate;
        }

        foreach (var item in GetComponentsInChildren<Collider>())
        {
            AllBodyColliders.Add(item);
            item.enabled = false;
        }
         
        _rigidbody = GetComponent<Rigidbody>();

        _animator = GetComponent<Animator>();
        _animator.SetTrigger("DrivePose");
    }

    public void ReleaseCharacter()
    {
        _animator.SetTrigger("TPose");

        transform.rotation = Quaternion.LookRotation(Vector3.down - transform.up);
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        applyforce = true;
        ActivateRagdoll();
    }

    bool applyforce = false;

    private void FixedUpdate()
    {
        if (applyforce)
        {
            //_rigidbody.AddForce(transform.right * 10,ForceMode.Impulse);
        }
    }

    public void ActivateRagdoll() 
    {
        _animator.enabled = false;
        foreach (var item in AllBodyRigidbody)
        {
            item.isKinematic = false;
            item.AddForce(Vector3.right * item.mass * 50, ForceMode.Impulse);
        }

        foreach (var item in AllBodyColliders)
        {
            item.enabled = true;
        }

        _rigidbody.useGravity = true;
    }
}