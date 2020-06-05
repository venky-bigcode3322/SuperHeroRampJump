using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ObstacleBase
{
    [SerializeField] ObstaclesTypes _obstaclesTypes;

    public override ObstaclesTypes obstacleType { get => _obstaclesTypes; }

    private Vector3 initialPosition;
    private Vector3 initialRotation;

    private void Awake() => initialPosition = transform.localPosition;

    private void OnEnable() => resetPosition();

    private bool saveInitialData = false;

    private bool isTriggered = false;
    
    void resetPosition()
    {
        if(!saveInitialData)
        {
            saveInitialData = true;
            initialPosition = transform.localPosition;
            initialRotation = transform.localRotation.eulerAngles;
        }
        transform.localPosition = initialPosition;
        transform.localRotation = Quaternion.Euler(initialRotation);

        if (isTriggered) isTriggered = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTriggered) 
        {
            if (collision.transform.CompareTag("Bike") || collision.transform.root.CompareTag("Player"))
            {
                isTriggered = true;
                //Debug.Log("Colliding:: + "+ collision.collider.name);
                GlobalVariables.BonusReward += 100;
                if (ScoreManager.Instance) ScoreManager.Instance.AddToQueue(_obstaclesTypes + "\n 100");
            }
        }
    }
}