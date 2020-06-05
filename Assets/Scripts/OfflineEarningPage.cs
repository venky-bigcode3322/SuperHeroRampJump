using System;
using UnityEngine;
using UnityEngine.UI;

public class OfflineEarningPage : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan ts = currentTime - Convert.ToDateTime(GlobalVariables.LastPlayedTime);
        offlineBonus = ((int)ts.TotalMinutes * (10 + GlobalVariables.OfflineEarningsLevel));
        BonusAmount.text = offlineBonus.ToString();
    }

    int offlineBonus;

    [SerializeField] Text BonusAmount;

    public void Claim()
    {
        GlobalVariables.AddCoins(offlineBonus);
    }
}