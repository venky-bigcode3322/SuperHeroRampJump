using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeSelectionPage : PopupBase
{
    public override AllPages CurrentPage => AllPages.BikeSelection;

    public override bool IsActive => gameObject.activeSelf;

    [SerializeField] Button[] BikeTypeButtons;

    [SerializeField] Sprite[] ActiveInactiveButtonSprite; // 0- Inactive :: 1- Active 

    [SerializeField] GameObject[] BikeTypeScrollViews;

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    private int currentBikeType = 0;

    public void SelectCurrentBikeType(int index)
    {
        if (currentBikeType == index)
            return;

        BikeTypeButtons[currentBikeType].image.sprite = ActiveInactiveButtonSprite[0];
        BikeTypeScrollViews[currentBikeType].SetActive(false);
        BikeTypeButtons[index].image.sprite = ActiveInactiveButtonSprite[1];
        BikeTypeScrollViews[index].SetActive(true);
        currentBikeType = index;
    }

    public void BackButton()
    {
        if (UiHandler.Instance) UiHandler.Instance.CheckAndClosePopup();
    }
}