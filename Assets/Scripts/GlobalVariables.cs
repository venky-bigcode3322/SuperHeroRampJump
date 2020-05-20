using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static float FuelPercentage = 100;

    public static int selectedCharacter
    {
        get => PlayerPrefs.GetInt("SelectedCharacter",0);
        set => PlayerPrefs.SetInt("SelectedCharacter", value);
    }
}