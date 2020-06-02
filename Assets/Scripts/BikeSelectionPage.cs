using System;
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

    [SerializeField] BikeProperties[] AllbikeProperties;

    [SerializeField] GameObject[] AllBikeModels;

    private float[] timerValues = new float[12];
    private bool[] timerRun = new bool[12];

    private int[] BikePrices = new int[] { 0, 1000, 5000, 6000, 7000, 8000, 9000, 10000, 11000, 12000, 13000, 14000 };
    private int[] BikeTimerValues = new int[] { 0, 0, 1, 7, 0, 0, 0, 0, 0, 21, 0, 0 };

    [SerializeField] Text CoinsText;
    [SerializeField] Text DiamondsText;

    [SerializeField] GameObject[] AllButtons;

    private Vector3[] DefaultButtonPositions;

    private void Awake()
    {
        DefaultButtonPositions = new Vector3[AllButtons.Length];

        for (int i = 0; i < DefaultButtonPositions.Length; i++)
            DefaultButtonPositions[i] = AllButtons[i].transform.localPosition;
    }

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

        SetDefualtUnlockTimerValues();
        CheckBikeUnlockStatus();

        StartCoroutine(AnimatePage(0));
    }

    private void OnDisable()
    {
        GlobalVariables.CoinsUpdateEvent -= UpdateCoins;
        GlobalVariables.DiamondUpdateEvent -= UpdateDiamonds;
    }

    private DateTime previosTime;

    private void SetDefualtUnlockTimerValues()
    {
        for (int i = 0; i < BikeTimerValues.Length; i++)
        {
            if(GlobalVariables.GetTimerValue(i) == string.Empty)
            {
                previosTime = DateTime.Now;
                previosTime = previosTime.AddDays(BikeTimerValues[i]);
                GlobalVariables.SetTimerValue(i,previosTime.ToString());
            }
        }
    }

    private int currentBikeType = 0;

    public void SelectedBikeModel(int index)
    {
        AllBikeModels[currentBikeType].SetActive(false);
        currentBikeType = index;
        AllBikeModels[currentBikeType].SetActive(true);
    }

    private TimeSpan tp;
    private List<int> CurrentTimerIndex = new List<int>();

    public void CheckBikeUnlockStatus()
    {
        CurrentTimerIndex.Clear();
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

                        previosTime = DateTime.Parse(GlobalVariables.GetTimerValue(i));
                        timerValues[i] = (float)(previosTime - DateTime.Now).TotalSeconds;
                        tp = TimeSpan.FromSeconds(timerValues[i]);
                        if(tp.TotalHours > 24)
                        {
                            AllbikeProperties[i].TimerText.text = tp.Days + "Days";
                        }
                        else
                        {
                            timerRun[i] = true;
                            CurrentTimerIndex.Add(i);
                        }

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
        if(GlobalVariables.GameCoins >= BikePrices[index])
        {
            GlobalVariables.DeductCoins(BikePrices[index]);
            GlobalVariables.UnlockBike(index);
        }
        else
        {
            Debug.Log("InSufficient Coins");
        }
    }

    public void DiamondsBuyButton(int index)
    {
        if (GlobalVariables.GameDiamonds >= BikePrices[index])
        {
            GlobalVariables.DeductDiamonds(BikePrices[index]);
            GlobalVariables.UnlockBike(index);
        }
        else
        {
            Debug.Log("InSufficient Diamonds");
        }
    }

    public void InAppBuyButton(int index)
    {

    }

    public void SelectBikeButton(int index)
    {
        GlobalVariables.selectedBike = index;
        if (GameManager.instance) GameManager.instance.InstantiateBikeAgain();
        BackButton();
    }

    public void WatchVideoToGet50Diamonds()
    {
        if (PluginManager.Instance) PluginManager.Instance.ShowRewardedVideoAd(RewardType_BigCode.Get50Diamonds);
    }

    public void PurchaseDiamonds()
    {

    }

    void UpdateCoins(int coins)
    {
        CoinsText.text = coins.ToString();
    }

    void UpdateDiamonds(int diamonds)
    {
        DiamondsText.text = diamonds.ToString();
    }

    private TimeSpan _tp;
    private void Update()
    {
        if(CurrentTimerIndex.Count > 0)
        {
            for (int i = 0; i < CurrentTimerIndex.Count; i++)
            {
                if (timerRun[CurrentTimerIndex[i]])
                {
                    timerValues[CurrentTimerIndex[i]] -= Time.deltaTime;
                    if(timerValues[CurrentTimerIndex[i]] <= 0)
                    {
                        timerRun[CurrentTimerIndex[i]] = false;
                        GlobalVariables.UnlockBike(CurrentTimerIndex[i]);
                        CheckBikeUnlockStatus();
                    }
                    _tp = TimeSpan.FromSeconds(timerValues[CurrentTimerIndex[i]]);
                    AllbikeProperties[CurrentTimerIndex[i]].TimerText.text = string.Format("{0:00}:{1:00}:{2:00}", _tp.Hours, _tp.Minutes, _tp.Seconds);
                }
            }
        }
    }

   // private Vector3 dir = Vector3.zero;

    IEnumerator AnimatePage(float waitTime)
    {
        for (int i = 0; i < AllButtons.Length; i++)
            AllButtons[i].SetActive(false);

        for (int i = 0; i < DefaultButtonPositions.Length; i++)
            AllButtons[i].transform.localPosition = DefaultButtonPositions[i];

        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < AllButtons.Length; i++)
        {
            AllButtons[i].SetActive(true);
            //dir = AllButtons[i].transform.localPosition;

            //if (i < 3)
            //{
            //    dir.y = -1000;
            //}
            //else if (i > 2 && i < 5)
            //{
            //    dir.x = -1000;
            //}
            //else if (i % 2 == 0)
            //{
            //    dir.x = 1000;
            //}
            //else
            //{
            //    dir.x = -1000;
            //}

            iTween.ScaleFrom(AllButtons[i], iTween.Hash("Scale", Vector3.zero, "time", 0.65f, "isLocal", true, "easeType", iTween.EaseType.easeOutBack));
            iTween.RotateFrom(AllButtons[i], iTween.Hash("z", 45, "time", 0.65f, "isLocal", true, "easeType", iTween.EaseType.easeOutBack));

            yield return new WaitForSeconds(0.1f);
        }
    }
}