using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class PathLoopManager : MonoBehaviour
{
    public static PathLoopManager Instance
    {
        get;private set;
    }

    [SerializeField] GameObject InitPiece;

    [SerializeField] GameObject[] PathPrefabs;

    private List<Transform> AvailablePaths = new List<Transform>();

    private void Awake()
    {
        Instance = this;
        if (!InitPiece.activeSelf)
            InitPiece.SetActive(true);
    }

    private void Start()
    {
        InitPath();
    }

    private Vector3 tempPosition = Vector3.zero;

    void InitPath()
    {
        //tempPosition.x = 720;
        GameObject obj;
        for (int i = 0; i < PathPrefabs.Length; i++)
        {
            obj = Instantiate(PathPrefabs[i]) as GameObject;
            obj.name = i.ToString();
            obj.SetActive(true);
            tempPosition.x = (i + 1)* 720;
            obj.transform.position = tempPosition;
            obj.transform.rotation = Quaternion.identity;
            AvailablePaths.Add(obj.transform);
            obj = null;
        }
    }

    bool leaveFirstPath = false;

    public void AssignNewPath()
    {
        if(!leaveFirstPath)
        {
            leaveFirstPath = true;
            return;
        }

        GameObject obj = AvailablePaths[0].gameObject;
        obj.SetActive(false);
        AvailablePaths.Remove(AvailablePaths[0].transform);
        tempPosition.x += 720;
        obj.transform.position = tempPosition;
        AvailablePaths.Add(obj.transform);
        obj.SetActive(true);
        obj = null;
    }
}