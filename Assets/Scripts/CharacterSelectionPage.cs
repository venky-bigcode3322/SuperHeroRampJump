using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionPage : PopupBase
{
    [SerializeField] Text CoinsText;
    [SerializeField] Text DiamondsText;

    public override AllPages CurrentPage => AllPages.CharacterSelection;

    public override bool IsActive => gameObject.activeSelf;

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GlobalVariables.CoinsUpdateEvent += UpdateCoins;
        GlobalVariables.DiamondUpdateEvent += UpdateDiamonds;

        UpdateCoins(GlobalVariables.GameCoins);
        UpdateDiamonds(GlobalVariables.GameDiamonds);

        SetPageDetails();
    }

    private void OnDisable()
    {
        GlobalVariables.CoinsUpdateEvent -= UpdateCoins;
        GlobalVariables.DiamondUpdateEvent -= UpdateDiamonds;
    }

    [SerializeField] private GameObject[] SuperHeroObjs;

    [SerializeField] private Button[] SelectButtons;
    
    void SetPageDetails()
    {
        for (int i = 0; i < SuperHeroObjs.Length; i++)
            SuperHeroObjs[i].SetActive(false);

        SuperHeroObjs[GlobalVariables.selectedCharacter].SetActive(true);

        for (int i = 0; i < SelectButtons.Length; i++)
        {
            if(i < GlobalVariables.unlockedCharacters)
            {
                SelectButtons[i].interactable = true;
            }
            else
            {
                SelectButtons[i].interactable = false;
            }
        }
    }

    public void SelectCharacter(int index)
    {
        GlobalVariables.selectedCharacter = index;
        SetPageDetails();
        if (BikeController.instance) BikeController.instance.ActivateSuperHeroCharacter();
    }

    public void BackButton()
    {
        if (UiHandler.Instance) UiHandler.Instance.CheckAndClosePopup();
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