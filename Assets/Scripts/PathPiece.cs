using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPiece : MonoBehaviour
{
    [SerializeField] Transform[] ObstacleGenerators;

    private Vector3 generatorPosition = Vector3.zero;

    private void OnEnable()
    {
        generatorPosition.x = Random.Range(-300, -150);
        ObstacleGenerators[0].localPosition = generatorPosition;

        generatorPosition.x = Random.Range(-150, 0);
        ObstacleGenerators[1].localPosition = generatorPosition;

        generatorPosition.x = Random.Range(0, 150);
        ObstacleGenerators[2].localPosition = generatorPosition;

        generatorPosition.x = Random.Range(150, 300);
        ObstacleGenerators[3].localPosition = generatorPosition;
    }
}