using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 15;
    // the height we want the camera to be above the target
    public float height = 5;
    // How much we 
    public float heightDamping = 3;
    public float rotationDamping = 3;

    [HideInInspector] public float lerpDistance;
    [HideInInspector] public float lerpHeight;

    public Vector3 offset;

    private void Awake()
    {
        Instance = this;
        lerpDistance = distance;
        lerpHeight = height;
    }

    public bool turnAround = false;

    public void ActivateTurnCamera()
    {
        lerpHeight += 10;
        Invoke(nameof(turnOn), 2.2f);
    }

    private void turnOn()
    {
        turnAround = true;
    }

    private void LateUpdate()
    {
        if (target)
        {
            if (!turnAround)
            {
                // Calculate the current rotation angles
                var wantedRotationAngle = target.eulerAngles.y;

                height = Mathf.Lerp(height, lerpHeight, Time.deltaTime * 2);

                var position = target.position;
                var wantedHeight = position.y + height;

                var transform1 = transform;
                var currentRotationAngle = transform1.eulerAngles.y;
                var currentHeight = transform1.position.y;

                // Damp the rotation around the y-axis
                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle + offset.y, rotationDamping * Time.deltaTime);

                // Damp the height
                currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

                // Convert the angle into a rotation
                Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

                // Set the position of the camera on the x-z plane to:
                // distance meters behind the target

                distance = Mathf.Lerp(distance, lerpDistance, Time.deltaTime * 2);

                var pos = position;
                pos -= currentRotation * Vector3.forward * distance;
                pos.y = currentHeight;
                transform.position = pos;

                // Always look at the target
                transform.LookAt(target);
            }
            else
            {
                transform.RotateAround(target.position, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime);
            }
        }
    }
}