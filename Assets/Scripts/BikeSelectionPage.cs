using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeSelectionPage : PopupBase
{
    [System.Serializable]
    public struct BikeProperties
    {
        [SerializeField] string BikeName;
        public BikeUnlockType BikeUnlockType;
        public GameObject SelectButton;
        public GameObject CoinsBuyButton;
        public Text CoinCostText;
        public GameObject DiamondsBuyButton;
        public Text DiamondsCostText;
        public GameObject InAppButton;
        public Text InappText;
        public GameObject TimerObject;
        public Text TimerText;
    }

    public override AllPages CurrentPage => AllPages.BikeSelection;

    public override bool IsActive => gameObject.activeSelf;

    [SerializeField] Button[] BikeTypeButtons;

    [SerializeField] Sprite[] ActiveInactiveButtonSprite; // 0- Inactive :: 1- Active 

    [SerializeField] GameObject[] BikeTypeScrollViews;

    [SerializeField] BikeProperties[] AllbikeProperties;

    private int[] BikePrices = new int[] { 0, 1000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000, 13000, 14000 };

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
        CheckBikeUnlockStatus();
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

    public void CheckBikeUnlockStatus()
    {
        for (int i = 0; i < AllbikeProperties.Length; i++)
        {
            AllbikeProperties[i].SelectButton.SetActive(false);
            AllbikeProperties[i].InAppButton.SetActive(false);
            AllbikeProperties[i].CoinsBuyButton.SetActive(false);
            AllbikeProperties[i].DiamondsBuyButton.SetActive(false);

            if (GlobalVariables.CheckBikeUnlockedStatus(i))
            {
                AllbikeProperties[i].SelectButton.SetActive(true);
            }
            else
            {
                switch (AllbikeProperties[i].BikeUnlockType)
                {
                    case BikeUnlockType.Coins:
                        AllbikeProperties[i].CoinsBuyButton.SetActive(true);
                        AllbikeProperties[i].CoinCostText.text = BikePrices[i].ToString();
                        break;
                    case BikeUnlockType.DailyBonus:
                        AllbikeProperties[i].TimerObject.SetActive(true);
                        break;
                    case BikeUnlockType.Diamonds:
                        AllbikeProperties[i].DiamondsBuyButton.SetActive(true);
                        AllbikeProperties[i].DiamondsCostText.text = BikePrices[i].ToString();
                        break;
                    case BikeUnlockType.InApp:
                        AllbikeProperties[i].InAppButton.SetActive(true);
                        break;
                }
            }
        }
    }

    public void BackButton()
    {
        if (UiHandler.Instance) UiHandler.Instance.CheckAndClosePopup();
    }

    public void CoinsBuyButton(int index)
    {

    }

    public void DiamondsBuyButton(int index)
    {

    }

    public void InAppBuyButton(int index)
    {

    }

    public void SelectBikeButton(int index)
    {

    }

    public void WatchVideoToGet50Diamonds()
    {

    }

    public void PurchaseDiamonds()
    {

    }
}