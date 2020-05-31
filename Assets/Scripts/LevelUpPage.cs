using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPage : PopupBase
{
    public override AllPages CurrentPage => AllPages.LevelUpPage;

    public override bool IsActive => gameObject.activeSelf;

    [SerializeField] Text LevelText;

    [SerializeField] Text LevelAmount;

    [SerializeField] Text MultipliedAmountText;

    [SerializeField] Button TrippleRewardButton;

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    public void Back()
    {
        if (UiHandler.Instance) UiHandler.Instance.CheckAndClosePopup();
    }

    private void OnEnable()
    {
        SetDetails();
    }

    void SetDetails()
    {
        LevelText.text = "LEVEL " + GlobalVariables.GameLevel;
        var val = (GlobalVariables.GameLevel * 1000);
        LevelAmount.text = val.ToString();
        GlobalVariables.AddCoins(val);
        MultipliedAmountText.text = (val * 3).ToString();
    }

    public void TrippleReward()
    {

    }

    public void TrippleRewardSuccess()
    {
        TrippleRewardButton.interactable = false;
    }

    public void NextLevelClick()
    {
        GlobalVariables.GameLevel += 1;
        if (UiHandler.Instance) UiHandler.Instance.ShowPopup(CurrentPage, AllPages.FinishPage);
    }
}