using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] Obstacles;

    private int currentObstacleIndex = 0;

    private void OnEnable() => EnableRandomObstacle();

    private void OnDisable() => DisableCurrentObstacle();

    void EnableRandomObstacle()
    {
        int randomNumber = Random.Range(0, Obstacles.Length);

        Obstacles[randomNumber].SetActive(true);

        currentObstacleIndex = randomNumber;
    }

    void DisableCurrentObstacle()
    {
        Obstacles[currentObstacleIndex].SetActive(false);
    }
}