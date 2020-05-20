using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : PopupBase
{
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

    public void DiamondsButton()
    {
        
    }

    public void FuelUpgradeButton()
    {
        
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
    }
}