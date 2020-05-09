using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Animator _animator;

    private List<Collider> AllBodyColliders = new List<Collider>();
    private List<Rigidbody> AllBodyRigidbody = new List<Rigidbody>();

    private Rigidbody _rigidbody;

    [SerializeField] Transform Hips;

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
        ActivateRagdoll();

        transform.rotation = Quaternion.LookRotation(Vector3.down - transform.up);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            applyforce = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            applyforce = false;
        }
    }


    bool applyforce = false;

    private Vector3 currentPosition;

    private void FixedUpdate()
    {
        if (applyforce)
        {
            foreach (var item in AllBodyRigidbody)
            {
                item.AddForce(((Vector3.right - Vector3.down) / 2) * item.mass, ForceMode.Impulse);
            }
        }
        currentPosition = Hips.localPosition;
        currentPosition.x = 0;
        Hips.localPosition = currentPosition;
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