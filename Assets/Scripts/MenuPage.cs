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

    [SerializeField] Text BestScoreText;

    [SerializeField] GameObject[] AllButtons;

    private Vector3[] DefaultButtonPositions;

    [SerializeField] DailyBonusPage DailyBonusPage;

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
        GetBestScore();
        CheckBonusUpgrade();

        StartCoroutine(AnimatePage(0));

        Invoke("CheckDailyBonus",2);
    }

    void CheckDailyBonus()
    {
        DailyBonusPage.CheckDailyBonus();
    }

    void GetBestScore()
    {
        BestScoreText.text = GlobalVariables.BestScore + " Mtrs";
    }

    private void OnDisable()
    {
        GlobalVariables.CoinsUpdateEvent -= UpdateCoins;
        GlobalVariables.DiamondUpdateEvent -= UpdateDiamonds;
    }


    public void DiamondsButton()
    {

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

    [SerializeField] Text BonusText;
    [SerializeField] Text BonusLevelText;

    void CheckBonusUpgrade()
    {
        BonusText.text = GlobalVariables.OfflineEarningsLevelPrice.ToString();
        BonusLevelText.text = "Level " + (GlobalVariables.OfflineEarningsLevel + 1);
    }

    public void OfflineEarningsButton()
    {
        ButtonClickSound();
        if (GlobalVariables.GameCoins >= GlobalVariables.OfflineEarningsLevelPrice)
        {
            GlobalVariables.DeductCoins(GlobalVariables.OfflineEarningsLevelPrice);
            GlobalVariables.OfflineEarningsLevel += 1;
            GlobalVariables.OfflineEarningsLevelPrice += 1000;
        }
        CheckBonusUpgrade();
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
        GameManager.instance.SetFuelforGame(20 + (GlobalVariables.UpgradeLevel + 1));

        SoundManager.Instance.StartCoroutine(SoundManager.Instance.PlayBG(MusicBG.PlayBG));
    }

    void UpdateCoins(int coins)
    {
        CoinsText.text = coins.ToString();
    }

    void UpdateDiamonds(int diamonds)
    {
        DiamondsText.text = diamonds.ToString();
    }



    private Vector3 dir = Vector3.zero;

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
            dir = AllButtons[i].transform.localPosition;

            if (i < 3)
            {
                dir.y = -1000;
            }
            else if(i > 2 && i < 5)
            {
                dir.x = -1000;
            }
            else if (i % 2 == 0)
            {
                dir.x = 1000;
            }
            else
            {
                dir.x = -1000;
            }

            iTween.MoveFrom(AllButtons[i], iTween.Hash("position", dir, "time", 0.75f, "isLocal", true, "easeType", iTween.EaseType.easeOutBack));

            yield return new WaitForSeconds(0.15f);
        }
    }
}