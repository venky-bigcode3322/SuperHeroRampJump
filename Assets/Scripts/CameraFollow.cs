﻿using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance
    {
        get;private set;
    }

    // camera will follow this object
    public Transform Target;
    //camera transform
    public Transform camTransform;
    // offset between camera and target
    public Vector3 Offset;
    // change this value to get desired smoothness
    public float SmoothTime = 0.3f;

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Instance = this;
       // Offset = camTransform.position - Target.position;
    }

    private void LateUpdate()
    {
        // update position
        Vector3 targetPosition = Target.position + Offset;
        camTransform.position = Vector3.Lerp(transform.position, targetPosition,SmoothTime * Time.deltaTime);

        // update rotation
        transform.LookAt(Target);
    }
}