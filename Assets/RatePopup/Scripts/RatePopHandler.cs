using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatePopHandler : MonoBehaviour {

    public static RatePopHandler Instance;

    public GameObject RatePopObj;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        //ShowRatePopUp(3);
    }

    public void ShowRatePopUp(int levelNumber)
    {
        if (ServerDataHandler.Instance != null && ServerDataHandler.Instance.BaseData != null &&
            ServerDataHandler.Instance.BaseData.RatePopUp.isEnabled &&
            ServerDataHandler.Instance.BaseData.RatePopUp.levelNumbers.Contains(levelNumber))
        {
            if (GameDataHandler.Instance && GameDataHandler.Instance.GameData != null && !GameDataHandler.Instance.GameData.isRated)
                RatePopObj.SetActive(true);
        }
    }
    
}
