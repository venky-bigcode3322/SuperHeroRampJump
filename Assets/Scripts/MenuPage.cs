using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPage : PopupBase
{
    [SerializeField] Text FuelUpgradeLevelText;
    [SerializeField] Text FuelUpgradePriceText;

    [SerializeField] Text CoinsText;
    [SerializeField] Text DiamondsText;

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    public override AllPages CurrentPage => AllPages.MenuPage;

    public override bool IsActive => gameObject.activeSelf;

    public override void MoreGames()
    {
        base.MoreGames();
    }

    private void OnEnable()
    {
        GlobalVariables.CoinsUpdateEvent += UpdateCoins;
        GlobalVariables.DiamondUpdateEvent += UpdateDiamonds;
        UpdateCoins(GlobalVariables.GameCoins);
        UpdateDiamonds(GlobalVariables.GameDiamonds);
        CheckFuelUpgradeHud();
    }

    private void OnDisable()
    {
        GlobalVariables.CoinsUpdateEvent -= UpdateCoins;
        GlobalVariables.DiamondUpdateEvent -= UpdateDiamonds;
    }


    public void DiamondsButton()
    {
        //
    }

    public void FuelUpgradeButton()
    {
        GlobalVariables.UpgradeLevel += 1;
        GlobalVariables.UpgradeLevelPrice += 1000;
        CheckFuelUpgradeHud();
    }

    void CheckFuelUpgradeHud()
    {
        FuelUpgradeLevelText.text = "Level " + (GlobalVariables.UpgradeLevel + 1);
        FuelUpgradePriceText.text = GlobalVariables.UpgradeLevelPrice.ToString();
    }

    public void OfflineEarningsButton()
    {
        
    }

    public void CharactersButton()
    {
        if (UiHandler.Instance) UiHandler.Instance.ShowPopup(CurrentPage, AllPages.CharacterSelection);
    }

    public void BikesButton()
    {
        if (UiHandler.Instance) UiHandler.Instance.ShowPopup(CurrentPage, AllPages.BikeSelection);
    }

    public void SettingsButton()
    {
        if (UiHandler.Instance) UiHandler.Instance.ShowPopup(CurrentPage, AllPages.Settings);
    }

    public void TapToContinue()
    {
        if (UiHandler.Instance) UiHandler.Instance.ShowPopup(CurrentPage, AllPages.Ingame);
        GlobalVariables.FuelPercentage = 20 + (GlobalVariables.UpgradeLevel + 1);
        Debug.LogError("FuelPercentage:: " + GlobalVariables.FuelPercentage);
    }

    void UpdateCoins(int coins)
    {
        CoinsText.text = coins.ToString();
    }

    void UpdateDiamonds(int diamonds)
    {
        DiamondsText.text = diamonds.ToString();
    }
}