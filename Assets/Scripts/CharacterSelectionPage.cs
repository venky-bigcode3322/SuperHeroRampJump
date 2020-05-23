using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionPage : PopupBase
{
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

    private void OnEnable() => SetPageDetails();


    [SerializeField] private GameObject[] SuperHeroObjs;
    
    void SetPageDetails()
    {
        for (int i = 0; i < SuperHeroObjs.Length; i++)
            SuperHeroObjs[i].SetActive(false);

        SuperHeroObjs[GlobalVariables.selectedCharacter].SetActive(true);
    }

    public void SelectCharacter(int index)
    {
        GlobalVariables.selectedCharacter = index;
        SetPageDetails();
    }

    public void BackButton()
    {
        if (UiHandler.Instance) UiHandler.Instance.CheckAndClosePopup();
    }
}