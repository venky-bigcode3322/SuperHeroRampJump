using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KeyGenerator : MonoBehaviour
{
    [SerializeField] GameObject KeyObject;

    private void Start()
    {
        GenerateKey(Vector3.zero);
    }

    public void GenerateKey(Vector3 Position)
    {
        GameObject obj = Instantiate(KeyObject) as GameObject;
        Position.y = Random.Range(40,50);
        Position.z = 0;
        obj.transform.position = Position;
    }
}