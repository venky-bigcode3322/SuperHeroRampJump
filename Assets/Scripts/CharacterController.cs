using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Animator animator;

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

        animator = GetComponent<Animator>();

        Hips.transform.GetChild(2).GetComponent<Collider>().enabled = true;
        Hips.transform.GetChild(2).tag = "Player";
    }

    public void ReleaseCharacter(float val)
    {
        animator.SetTrigger(Flying);

        var transform1 = transform;
        var rotation = transform1.rotation;
        rotation = Quaternion.LookRotation(Vector3.down - transform1.up);
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        ActivateRagdoll();

        rotation = Quaternion.LookRotation(Vector3.right);
        transform.rotation = rotation;

        Debug.LogError("Velocity:: " + val);
        foreach (var item in AllBodyRigidbody)
        {
            item.AddForce(Vector3.right * (item.mass * val), ForceMode.Impulse);
        }
    }

    private Vector3 _currentPosition;
    private static readonly int DriverPose = Animator.StringToHash("DriverPose");
    private static readonly int Flying = Animator.StringToHash("Flying");

    private void FixedUpdate()
    {
        //if (applyforce)
        //{
        //    foreach (var item in AllBodyRigidbody)
        //    {
        //        item.AddForce(((Vector3.right - Vector3.down) / 2) * item.mass, ForceMode.Impulse);
        //    }
        //}
        _currentPosition = Hips.localPosition;
        _currentPosition.x = 0;
        Hips.localPosition = _currentPosition;

        if (BikeController.instance.currentBikeState == BikeController.BikeControlStates.ReleaseCharacterFromBike)
        {
            if(Hips.transform.position.y < 5)
            {
                animator.enabled = false;
            }     
        }
    }

    private void ActivateRagdoll() 
    {
        //_animator.enabled = false;
        foreach (var item in AllBodyRigidbody)
        {
            item.isKinematic = false;
            //item.AddForce(Vector3.right * item.mass * 50, ForceMode.Impulse);
        }

        foreach (var item in AllBodyColliders)
        {
            item.enabled = true;
        }

        _rigidbody.useGravity = true;
    }

    public void SetDriverPose(int index)
    {
        animator.SetInteger(DriverPose, index);
    }
}