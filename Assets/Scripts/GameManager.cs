using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;private set;
    }
    
    [SerializeField] Slider FuelBar;

    private float InitialBarPercentage;

    private void Awake()
    {
        Instance = this;
        InitialBarPercentage = GlobalVariables.FuelPercentage;
    }

    public void CheckFuelHUD()
    {
       // Debug.Log("FuelPercentage:: " + GlobalVariables.FuelPercentage);
        FuelBar.value = GlobalVariables.FuelPercentage / InitialBarPercentage;
    }
}