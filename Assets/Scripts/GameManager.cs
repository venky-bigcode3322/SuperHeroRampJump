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

    private void Awake()
    {
        Instance = this;
    }

    public void CheckFuelHUD()
    {
       // Debug.Log("FuelPercentage:: " + GlobalVariables.FuelPercentage);
        FuelBar.value = GlobalVariables.FuelPercentage / 200f;
    }
}