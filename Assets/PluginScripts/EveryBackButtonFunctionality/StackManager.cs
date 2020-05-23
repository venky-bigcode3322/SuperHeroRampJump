using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StackManager : MonoBehaviour
{



    public List<CallMethod> pages;


    private void Awake()
    {
        pages = new List<CallMethod>();
        BigCodeLibHandler_BigCode.Instance.StackManager = this;
    }

 

    
}


[System.Serializable]
public class CallMethod
{
    public delegate void Func();
    public delegate void Function(string param);
    public delegate void FunctionGameObject(GameObject param);

    public string parameter;

    public MethodType MethodType = MethodType.NONPARAMETERIZED;

    public Func fun;
    public Function function;
    public FunctionGameObject functionGameObject;
}

public enum MethodType
{
    PARAMETERIZED,
    NONPARAMETERIZED,
    GAMEOBJECTPARAM
}

