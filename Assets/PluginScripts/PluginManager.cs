using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PluginManager : MonoBehaviour {

    public string CommonDataURL;

    public string BaseDataURL;

    public static PluginManager Instance;

    public bool isPotraitGame;

    public string NextScene;

    //[HideInInspector]
    public GameObject toastPopUp;


    [HideInInspector]
    public Texture2D gameExitTexture;


    [HideInInspector]
    public float lastShown;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        print(Application.persistentDataPath);

    }

    private void Update()
    {
        lastShown += Time.deltaTime;
    }



    [HideInInspector]
    public bool isInternetAvailable;
    // Use this for initialization
    void Start() {

        

        StartCoroutine(InternetHandler.Instance.CheckInternetConnection((isConnected) =>
        {
            isInternetAvailable = isConnected;

            StartCoroutine(HitServer());

            // Handle connection status here
            if (isConnected)
            {
                RequestingServerData();
            }
            else
            {
                //GameDataHandler.Instance.NoInternetLoadGameData();

                GameDataHandler.Instance.NoInternetLoadGameData();

                RequestingServerData();

                //if (!LoginHandler.Instance.isAuthenticated())
                //    SceneManager.LoadScene("LoginScene");
                //else
                //    SceneManager.LoadScene(PluginManager.Instance.NextScene);
            }

        }));

        //CreateDefaultServerJSON();



    }




    public DateTime CurrentDateTime
    {
        get
        {
            return currentDateTime.AddSeconds(Time.realtimeSinceStartup);
        }
    }

    private DateTime currentDateTime;


    private IEnumerator loadExitGameTexture()
    {
        WWW www = new WWW(ServerDataHandler.Instance.BaseData.ExitGame.gameLink);
        yield return www;

        gameExitTexture = www.texture;

        //gameExitTexture = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    private IEnumerator HitServer()
    {
        if (isInternetAvailable)
        {
            WWW www = new WWW(GameConstants_BigCode.DATE_TIME_URL);
            yield return www;
            if (www.error == null)
            {
                //Debug.LogError(www.text);
                string currentDateTimeString = www.text;

                string[] dateTime = currentDateTimeString.Split(new char[] { '#' });
                string[] date = dateTime[0].Split(new char[] { '-' });
                string[] time = dateTime[1].Split(new char[] { ':' });
                currentDateTime = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
            }
            else
            {
                currentDateTime = DateTime.Now;
            }
        }
        else
        {
            currentDateTime = DateTime.Now;
        }


        //Debug.LogError(CurrentDateTime.DayOfWeek);
    }


    private void RequestingServerData()
    {
        StartCoroutine(ServerDataHandler.Instance.LoadServerData());
    }


    public void OnServerDataLoaded()
    {
        PluginManager.Instance.lastShown = ServerDataHandler.Instance.BaseData.AdsDelay.AdToAdDelay;

        StartCoroutine(loadExitGameTexture());


        StartupInitializeTasks();
    }



    public void StartupInitializeTasks()
    {
        CheckGameUpdatedVersion();

    }


    public void ContinueToGame()
    {

#if UNITY_EDITOR
        // Gamedata Intialization
        //GameDataHandler.Instance.NoInternetLoadGameData();

        //MenuAd--> Editor Code
        StartCoroutine(BigCodeAdHandler_BigCode.Instance.Initialize());
#endif


        // MenuAd--> Admob (or) Bigcode
#if !UNITY_EDITOR

        if (PluginManager.Instance.isInternetAvailable && ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData != null && ServerDataHandler.Instance.BaseData.MenuAds[0] == ADS.BIGCODE)
            StartCoroutine(BigCodeAdHandler_BigCode.Instance.Initialize());
        //else if (PluginManager.Instance.isInternetAvailable && ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData != null && ServerDataHandler.Instance.BaseData.MenuAds[0] == ADS.ADMOB)
        //    AdmobHandler_BigCode.Instance.Initialize();
        else
            LoadNextScene();
#endif
    }

    public void CheckGameUpdatedVersion()
    {
        if (PluginManager.Instance.isInternetAvailable && ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData != null && BigCodeLibHandler_BigCode.Instance)
        {



#if UNITY_EDITOR || UNITY_IOS

            ContinueToGame();
#endif

#if UNITY_ANDROID

            if (ServerDataHandler.Instance.BaseData.version.armv7.Equals(Application.version))
            {
                ContinueToGame();
            }
            else
            {
                ShowUpdateVersionDailog();
            }
#endif

        }
        else
        {
            ContinueToGame();
        }
    }

    private void ShowUpdateVersionDailog()
    {
#if !UNITY_EDITOR
        if (BigCodeLibHandler_BigCode.Instance)
            BigCodeLibHandler_BigCode.Instance.ShowAlertDialog("Please Update The Game To Latest Version!!!", "Update", "Cancel", NativeDialogType.UPDATEPOPUP);
#endif
    }

    public void OnUpdateVersionCancel()
    {
        ContinueToGame();
    }


    public void OnBigCodeAdLoaded()
    {
        LoadNextScene();
    }

    public void OnAdmobInitialized()
    {
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
            OnDataLoaded();

        SceneManager.LoadScene("BaseScene");
    }


    public void OnDataLoaded()
    {
        //Modified
        //IronSourceHandler.Instance.Initialize();

        //UnityAdsHandler_BigCode.Instance.Initialize();

        //VungleAdHandler_BigCode.Instance.Initialize();

        //InappPurchaseHandler.Instance.Initialize();

        MainThread.Call(FacebookHandler_BigCode.Instance.Initialize);

#if UNITY_EDITOR

#else
        //FacebookAdsHandler.Instance.Initialize();
#endif

        StartCoroutine(TrackInstallsHandler.Instance.Initialize());

        //Modified
        //if (ServerDataHandler.Instance.BaseData.MenuAds[0] == ADS.BIGCODE && ServerDataHandler.Instance.BaseData.InterstitialAds.Contains(ADS.ADMOB))
        //    AdmobHandler_BigCode.Instance.Initialize();
    }
    

    public void ShowAcheivments()
    {
        if (GooglePlayGamesHandler.Instance)
            GooglePlayGamesHandler.Instance.SignIn(GooglePlayGamesHandler.SignInType.ACHEIVMENTS);
    }

    public void ShowLeaderboard()
    {
        if (GooglePlayGamesHandler.Instance)
            GooglePlayGamesHandler.Instance.SignIn(GooglePlayGamesHandler.SignInType.LEADERBOARD);


    }

    public void UnlockAchievment(string id)
    {
        if (GooglePlayGamesHandler.Instance)
            GooglePlayGamesHandler.Instance.ReportProgress(id, 100f);
    }

    public void PushScoreToLeaderboard(string id,long score)
    {
        if (GooglePlayGamesHandler.Instance)
            GooglePlayGamesHandler.Instance.ReportScore(id,score);
    }

    public void ShowLoading()
    {
        if (BigCodeLibHandler_BigCode.Instance)
            BigCodeLibHandler_BigCode.Instance.ShowLoading();
    }

    public void DismissLoading()
    {
        if (BigCodeLibHandler_BigCode.Instance)
            BigCodeLibHandler_BigCode.Instance.DismissLoading();
    }




    [HideInInspector]
    public ADS LoadedAd = ADS.NONE;

    [HideInInspector]
    public static int RotationInterstitialAdsPreference = -1;


    [HideInInspector]
    public static int RotationRewardedAdsPreference = -1;

    [HideInInspector]
    public int RequestInterstitialIndex;

    [HideInInspector]
    public int BackupRotationalInterstitialAdsCountInGame;

    [HideInInspector]
    public int BackupRotationalRewardedAdsCountInGame;


    private void RequestRotationInterstitialAd()
    {
        RotationInterstitialAdsPreference++; // 1

        if (ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
        {
            BackupRotationalInterstitialAdsCountInGame = 1;

            if (RotationInterstitialAdsPreference >= ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount )
            {
                RotationInterstitialAdsPreference = 0;
            }
        }

        if (RotationInterstitialAdsPreference >= ServerDataHandler.Instance.BaseData.InterstitialAds.Count)
        {
            RotationInterstitialAdsPreference = 0;
        }

        RequestInterstitial(RotationInterstitialAdsPreference);
    }


    private void RequestRotationRewardedAd()
    {
        RotationRewardedAdsPreference++;

        if (ServerDataHandler.Instance.BaseData.RewardedAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
        {
            BackupRotationalRewardedAdsCountInGame = 1;

            if (RotationRewardedAdsPreference >= ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount)
            {
                RotationRewardedAdsPreference = 0;
            }
        }

        if (RotationRewardedAdsPreference >= ServerDataHandler.Instance.BaseData.RewardVideoAds.Count)
        {
            RotationRewardedAdsPreference = 0;
        }
        RequestRewardedVideoAd(RotationRewardedAdsPreference);
    }

    public void RequestInterstitial()
    {
        if (ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.NORMAL)
            PluginManager.Instance.RequestInterstitial(0);
        else if (ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.ROTATIONAL ||
            ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
            PluginManager.Instance.RequestRotationInterstitialAd();
    }

    public void RequestRewardedVideoAd()
    {


        if (ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.NORMAL)
            PluginManager.Instance.RequestRewardedVideoAd(0);
        else if (ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.ROTATIONAL ||
            ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
            PluginManager.Instance.RequestRotationRewardedAd();
    }

    public void RequestInterstitial(int index = 0)
    {
        if (LoadedAd == ADS.NONE || LoadedAd == ADS.NEWREQUEST)
        {
            LoadedAd = ADS.LOADING;

            if (index < ServerDataHandler.Instance.BaseData.InterstitialAds.Count)
            {
                RequestInterstitialIndex = index;

                switch (ServerDataHandler.Instance.BaseData.InterstitialAds[index])
                {
                    case ADS.ADMOB:
                        //Modified
                        //AdmobHandler_BigCode.Instance.RequestInterstitial(RequestPage.Game_Page);

                        break;
                    case ADS.IRONSOURCE:
                        //Modified
                        //IronSourceHandler.Instance.RequestInterstitial();

                        break;
                    case ADS.VUNGLE:
                        //Modified
                        //VungleAdHandler_BigCode.Instance.RequestInterstitial();

                        break;
                    case ADS.UNITY:
                        //Modified
                        //UnityAdsHandler_BigCode.Instance.RequestInterstitial();

                        break;
                    case ADS.FACEBOOK:

#if UNITY_EDITOR

#else
                        //Modified
                        //FacebookAdsHandler.Instance.RequestInterstitial();
#endif

                        break;
                    case ADS.APPLOVIN:

                        //Modified
                        //AppLovinAdsHandler.Instance.RequestInterstitial();

                        break;
                    case ADS.MOPUB:

                        MoPubAdsHandler.Instance.RequestInterstitial();

                        break;
                    default:

                        PluginManager.Instance.LoadedAd = ADS.NEWREQUEST;

                        PluginManager.Instance.RequestInterstitialIndex = 0;
                        PluginManager.Instance.RequestInterstitial(PluginManager.Instance.RequestInterstitialIndex);

                        break;
                }
            }
            else
            {
                LoadedAd = ADS.NONE;
            }
        }
    }



    public void ShowInterstialAd()
    {
        if (PluginManager.Instance && PluginManager.Instance.isInternetAvailable && GameDataHandler.Instance && !GameDataHandler.Instance.GameData.NoadsPurchased)
        {
            switch (LoadedAd)
            {
                case ADS.VUNGLE:
                    //Modified
                    //if (VungleAdHandler_BigCode.Instance.InterstialIsLoaded)
                    //{
                    //    VungleAdHandler_BigCode.Instance.InterstialIsLoaded = false;
                    //    VungleAdHandler_BigCode.Instance.ShowAd(VungleAdHandler_BigCode.Instance.SkippableAdPlacementID);
                    //}

                    //PluginManager.Instance.ShowToast("Showing Vungle Ad");

                    break;
                case ADS.UNITY:
                    //Modified
                    //if (UnityAdsHandler_BigCode.Instance.IsAdReady(UnityAdsHandler_BigCode.SKIPPABLE_VIDEO))
                    //{
                    //    UnityAdsHandler_BigCode.Instance.ShowVideoAd();
                    //}

                    //PluginManager.Instance.ShowToast("Showing Unity Ad");

                    break;
                case ADS.ADMOB:
                    //Modified
                    //if (AdmobHandler_BigCode.Instance.interstitial.IsLoaded())
                    //{
                    //    AdmobHandler_BigCode.Instance.ShowInterstialAd();
                    //}

                    //PluginManager.Instance.ShowToast("Showing Admob Ad");

                    break;
                case ADS.IRONSOURCE:
                    //Modified
                    //if (IronSourceHandler.Instance.IsInterstitialLoaded())
                    //{
                    //    IronSourceHandler.Instance.ShowInterstitial();
                    //}

                    //PluginManager.Instance.ShowToast("Showing IronSource Ad");

                    break;
                case ADS.FACEBOOK:


#if UNITY_EDITOR

#else
                    //Modified
                    //if(FacebookAdsHandler.Instance.isInterstitialLoaded())
                    //{
                    //    FacebookAdsHandler.Instance.ShowInterstitial();
                    //}
#endif



                    //PluginManager.Instance.ShowToast("Showing Facebook Ad");


                    break;
                case ADS.APPLOVIN:


                    //if (AppLovinAdsHandler.Instance.isInterstitialReady())
                    //{
                    //    AppLovinAdsHandler.Instance.ShowInterstitial();
                    //}

                    break;
                case ADS.MOPUB:

                    MoPubAdsHandler.Instance.ShowInterstitial();

                    break;
            }

            if (LoadedAd != ADS.LOADING)
            {
                PluginManager.Instance.LoadedAd = ADS.NONE;
            }
        }

    }









    [HideInInspector]
    public ADS LoadedRewardedAd = ADS.NONE;

    [HideInInspector]
    public int RewardedVideoAdIndex;

    public void RequestRewardedVideoAd(int index = 0)
    {
        if (PluginManager.Instance.isInternetAvailable)
        {



            if (LoadedRewardedAd == ADS.NONE || LoadedRewardedAd == ADS.NEWREQUEST)
            {
                LoadedRewardedAd = ADS.LOADING;

                if (index < ServerDataHandler.Instance.BaseData.RewardVideoAds.Count)
                {
                    RewardedVideoAdIndex = index;

                    switch (ServerDataHandler.Instance.BaseData.RewardVideoAds[index])
                    {
                        case ADS.ADMOB:
                            //Modified
                            //AdmobHandler_BigCode.Instance.RequestRewardedVideo();

                            break;
                        case ADS.IRONSOURCE:
                            //Modified
                            //IronSourceHandler.Instance.RequestRewardedVideo();

                            break;
                        case ADS.VUNGLE:
                            //Modified
                            //VungleAdHandler_BigCode.Instance.RequestRewardedVideo();

                            break;
                        case ADS.UNITY:
                            //Modified
                            //UnityAdsHandler_BigCode.Instance.RequestRewardedVideo();

                            break;
                        case ADS.FACEBOOK:

#if UNITY_EDITOR

#else
                            //Modified
                            //FacebookAdsHandler.Instance.RequestRewardedVideo();
#endif
                            break;
                        case ADS.APPLOVIN:

                            //AppLovinAdsHandler.Instance.RequestRewardedVideo();

                            break;
                        case ADS.MOPUB:

                            MoPubAdsHandler.Instance.RequestRewardedVideo();

                            break;
                    }
                }
                else
                {
                    LoadedRewardedAd = ADS.NONE;
                }
            }
        }
    }





    int RewardedVideoAdsCount;
    [HideInInspector]
    public RewardType_BigCode RewardPlacement;

    public void ShowRewardedVideoAd(RewardType_BigCode placement)
    {
        if (PluginManager.Instance.isInternetAvailable)
        {

            if (LoadedRewardedAd != ADS.NONE && LoadedRewardedAd != ADS.LOADING && LoadedRewardedAd != ADS.NEWREQUEST)
            {
                RewardPlacement = placement;

                RewardedVideoAdsCount = ServerDataHandler.Instance.BaseData.RewardVideoAds.Count;

                if (RewardedVideoAdIndex < RewardedVideoAdsCount)
                {
                    switch (ServerDataHandler.Instance.BaseData.RewardVideoAds[RewardedVideoAdIndex])
                    {
                        case ADS.VUNGLE:

                            //Modified
                            //VungleAdHandler_BigCode.Instance.ShowRewardedVideo();


                            break;
                        case ADS.UNITY:
                            //Modified
                            //UnityAdsHandler_BigCode.Instance.ShowRewardedVideo();

                            break;
                        case ADS.ADMOB:
                            //Modified
                            //AdmobHandler_BigCode.Instance.ShowRewardedVideo();

                            break;
                        case ADS.IRONSOURCE:

                            //Modified
                            //IronSourceHandler.Instance.ShowRewardedVideo();

                            break;
                        case ADS.FACEBOOK:

#if UNITY_EDITOR

#else
                            //Modified
                        //FacebookAdsHandler.Instance.ShowRewardedVideo();
#endif

                            break;
                        case ADS.APPLOVIN:

                            //AppLovinAdsHandler.Instance.ShowRewardedVideoAd();

                            break;
                        case ADS.MOPUB:

                            MoPubAdsHandler.Instance.ShowRewardedVideo();

                            break;

                    }


                    if (LoadedRewardedAd != ADS.LOADING)
                    {
                        PluginManager.Instance.LoadedRewardedAd = ADS.NONE;
                    }


                }
            }
            else
            {
                PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Please Wait Ad Loading"));

                BridgeManager_Bigcode.Instance.OnNoRewardedVideoAdsFound();
            }

        }
        else
        {
            PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Network Not Available"));
        }

    }



    public void ShowToast(string msg)
    {
        if (PluginManager.Instance.toastPopUp)
        {

            if (msg.Contains("Coins added Sucessfully"))
            {
                string[] msgs = msg.Split(null);
                PluginManager.Instance.toastPopUp.GetComponentInChildren<Text>().text = msgs[0] + " " + LanguageHandler.GetCurrentLanguageText("Gold Added Successfully");
            }
            else if (msg.Contains("Diamond Added Successfully"))
            {
                string[] msgs = msg.Split(null);
                PluginManager.Instance.toastPopUp.GetComponentInChildren<Text>().text = msgs[0] + " " + LanguageHandler.GetCurrentLanguageText("Diamond Added Successfully");

            }
            else
            {
                PluginManager.Instance.toastPopUp.GetComponentInChildren<Text>().text = "" + LanguageHandler.GetCurrentLanguageText(msg);
            }

            BridgeManager_Bigcode.Instance.ToastContent(msg);
            ToastPopupScript.Instance.ShowToast();


        }
        else
        {
            if (BigCodeLibHandler_BigCode.Instance)
                BigCodeLibHandler_BigCode.Instance.ShowToast(msg);
        }


    }

    public void NativeShare(NativeShare nativeShare)
    {
        if (BigCodeLibHandler_BigCode.Instance)
            BigCodeLibHandler_BigCode.Instance.NativeSharing(nativeShare);
    }

    public void OnNoAdsFound()
    {
        if (BigCodeLibHandler_BigCode.Instance)
            BigCodeLibHandler_BigCode.Instance.DismissLoading();

        //PluginManager.Instance.ShowToast("No Ads Found");
    }

    public void ShowSharePopUp()
    {
        if (ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData != null)
        {
            BigCodeLibHandler_BigCode.Instance.ShowAlertDialog(ServerDataHandler.Instance.BaseData.SharePopUp.message, "Share", "Cancel", NativeDialogType.SHAREPOPUP);
        }
    }

    public void ShowSharePopUp(int levelNumber)
    {
        if (ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData != null && ServerDataHandler.Instance.BaseData.SharePopUp.levelNumbers.Contains(levelNumber))
        {
            ShowSharePopUp();
        }
    }

    public void ShowRatePopUp()
    {
        if (ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData != null)
        {
            BigCodeLibHandler_BigCode.Instance.ShowAlertDialog("Please Rate this game", "Rate US", "Cancel", NativeDialogType.RATEPOPUP);
        }
    }

    public void ShowRatePopUp(int levelNumber)
    {
        if (ServerDataHandler.Instance != null && ServerDataHandler.Instance.BaseData != null && 
            ServerDataHandler.Instance.BaseData.RatePopUp.isEnabled &&
            ServerDataHandler.Instance.BaseData.RatePopUp.levelNumbers.Contains(levelNumber))
        {
            if(GameDataHandler.Instance && GameDataHandler.Instance.GameData != null && !GameDataHandler.Instance.GameData.isRated)
            ShowRatePopUp();
        }
    }


    public void ShowPromoPage(string msg, string yesButtonName, string noButtonName)
    {
        if (BigCodeLibHandler_BigCode.Instance)
            BigCodeLibHandler_BigCode.Instance.ShowAlertPromoCode(msg, yesButtonName, noButtonName, NativeDialogType.PROMOPAGE);
    }


    public void OnRewardedVideoFinished()
    {
        MainThread.Call(BridgeManager_Bigcode.Instance.MainThreadOnRewardedFinished);
    }

    public void OnRewardVideoMiddleClosed()
    {
        BridgeManager_Bigcode.Instance.OnRewardVideoMiddleClosed();
    }


    public void OnProductPurchased(UnityEngine.Purchasing.Product product)
    {
        foreach (InappPurchaseHandler.InAppWithDiscountProduct inAppProduct in InappPurchaseHandler.Instance.DiscountProducts)
        {
            foreach (string innerProduct in inAppProduct.products)
            {
                if (innerProduct.Equals(product.definition.id))
                {
                    BridgeManager_Bigcode.Instance.RestorePurchases(InappPurchaseHandler.Instance.InAppProducts.IndexOf(inAppProduct.products[0]));

                    

                    break;
                }
            }
        }
    }

    public enum InAppPriceType
    {
        Normal,
        DisCount,
        Normal_Subscription,
        Normal_Discount,
        Subscription
    }

    public void SetProductText(int NormalProductIndex, Text textObj, InAppPriceType inAppPriceType)
    {
        if (PluginManager.Instance.isInternetAvailable && InappPurchaseHandler.Instance  && InappPurchaseHandler.Instance.products.Count > 0)
        {
            if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products.Count == 2)
            {
                if (inAppPriceType == InAppPriceType.Normal_Subscription)
                {
                    if (GameDataHandler.Instance.GameData.IsSubscription50Purchased)
                    {
                        if (ServerDataHandler.Instance.BaseData != null && ServerDataHandler.Instance.BaseData.IncludeNonConsumableInSubscription)
                        {
                            textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[1], LanguageHandler.GetCurrentLanguageText("Buy"));
                        }
                        else
                        {
                            textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0], LanguageHandler.GetCurrentLanguageText("Buy"));
                        }
                    }
                    else
                    {
                        textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0], LanguageHandler.GetCurrentLanguageText("Buy"));
                    }
                }
                else if (inAppPriceType == InAppPriceType.Normal_Discount)
                {

                }
                else if (inAppPriceType == InAppPriceType.Normal)
                {
                    textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0], LanguageHandler.GetCurrentLanguageText("Buy"));
                }
                else if (inAppPriceType == InAppPriceType.DisCount || inAppPriceType == InAppPriceType.Subscription)
                {
                    textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[1], LanguageHandler.GetCurrentLanguageText("Buy"));
                }


            }
            else if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products.Count == 3)
            {

                if (inAppPriceType == InAppPriceType.Normal_Subscription)
                {
                    if (GameDataHandler.Instance.GameData.IsSubscription50Purchased)
                    {
                        if (ServerDataHandler.Instance.BaseData != null && ServerDataHandler.Instance.BaseData.IncludeNonConsumableInSubscription)
                        {
                            textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[1], LanguageHandler.GetCurrentLanguageText("Buy"));
                        }
                        else
                        {
                            textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0], LanguageHandler.GetCurrentLanguageText("Buy"));
                        }
                    }
                    else
                    {
                        textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0], LanguageHandler.GetCurrentLanguageText("Buy"));
                    }
                }
                else if (inAppPriceType == InAppPriceType.Normal_Discount)
                {

                }
                else if (inAppPriceType == InAppPriceType.Normal)
                {
                    textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0], LanguageHandler.GetCurrentLanguageText("Buy"));
                }
                else if (inAppPriceType == InAppPriceType.DisCount)
                {
                    textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[2], LanguageHandler.GetCurrentLanguageText("Buy"));
                }
            }
            else if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products.Count == 1)
            {
                textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0], LanguageHandler.GetCurrentLanguageText("Buy"));
            }
        }
        else
        {
            textObj.text = PlayerPrefs.GetString(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0], LanguageHandler.GetCurrentLanguageText("Buy"));
        }
    }



    public void PurchaseProduct(int NormalProductIndex, InAppPriceType inAppPriceType)
    {
        if (InappPurchaseHandler.Instance && InappPurchaseHandler.Instance.products.Count > 0)
        {
            if (inAppPriceType == InAppPriceType.Normal_Subscription)
            {
                if (GameDataHandler.Instance.GameData.IsSubscription50Purchased)
                {
                    if (ServerDataHandler.Instance.BaseData != null && ServerDataHandler.Instance.BaseData.IncludeNonConsumableInSubscription)
                    {

                        if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].productType == InappPurchaseHandler.InAppProductType.NonConsumable)
                        {

                            bool isAnyProductPurchased = false;

                            foreach (string product in InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products)
                            {
                                if (InappPurchaseHandler.Instance.products[product].hasReceipt)
                                {
                                    isAnyProductPurchased = true;
                                }
                            }

                            if (isAnyProductPurchased)
                            {
                                PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Already Purchased"));
                            }
                            else
                            {
                                InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[1]);
                            }
                        }
                        else if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].productType == InappPurchaseHandler.InAppProductType.Consumable)
                        {
                            InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[1]);
                        }
                    }
                    else
                    {
                        if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].productType == InappPurchaseHandler.InAppProductType.NonConsumable)
                        {

                            bool isAnyProductPurchased = false;

                            foreach (string product in InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products)
                            {
                                if (InappPurchaseHandler.Instance.products[product].hasReceipt)
                                {
                                    isAnyProductPurchased = true;
                                }
                            }

                            if (isAnyProductPurchased)
                            {
                                PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Already Purchased"));
                            }
                            else
                            {
                                InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0]);
                            }
                            
                        }
                        else if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].productType == InappPurchaseHandler.InAppProductType.Consumable)
                        {
                            InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[1]);
                        }

                    }
                }
                else
                {
                    InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0]);
                }
            }
            else if (inAppPriceType == InAppPriceType.Normal_Discount)
            {

            }
            else if (inAppPriceType == InAppPriceType.Normal)
            {
                if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].productType == InappPurchaseHandler.InAppProductType.NonConsumable)
                {
                    bool isAnyProductPurchased = false;

                    foreach (string product in InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products)
                    {
                        if (InappPurchaseHandler.Instance.products[product].hasReceipt)
                        {
                            isAnyProductPurchased = true;
                        }
                    }

                    if (isAnyProductPurchased)
                    {
                        PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Already Purchased"));
                    }
                    else
                    {
                        InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0]);
                    }
                }
                else if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].productType == InappPurchaseHandler.InAppProductType.Consumable)
                {
                    InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0]);
                }


            }
            else if (inAppPriceType == InAppPriceType.DisCount || inAppPriceType == InAppPriceType.Subscription)
            {
                bool isAnyProductPurchased = false;

                foreach (string product in InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products)
                {
                    if (InappPurchaseHandler.Instance.products[product].hasReceipt)
                    {
                        isAnyProductPurchased = true;
                    }
                }

                if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].productType != InappPurchaseHandler.InAppProductType.Consumable && isAnyProductPurchased)
                {
                    PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Already Purchased"));
                }
                else
                {
                    if (inAppPriceType == InAppPriceType.DisCount)
                    {
                        if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products.Count == 2)
                            InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[1]);
                        else if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products.Count == 3)
                            InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[2]);
                    }
                    else if(inAppPriceType == InAppPriceType.Subscription)
                    {
                        if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products.Count == 1)
                        {
                            InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[0]);
                        }
                        else if (InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products.Count == 2)
                        {
                            InappPurchaseHandler.Instance.PurchaseProduct(InappPurchaseHandler.Instance.DiscountProducts[NormalProductIndex].products[1]);
                        }
                    }

                }
            }
            
        }


#if UNITY_EDITOR
        BridgeManager_Bigcode.Instance.RestorePurchases(NormalProductIndex);
#endif

    }




    public static DateTime StringToDateTime(string dateType)
    {
        string[] dateTime = dateType.Split(new char[] { '#' });
        string[] date = dateTime[0].Split(new char[] { '-' });
        string[] time = dateTime[1].Split(new char[] { ':' });
        return new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]));
    }

    public void MoreGames()
    {
        string link = null;

        if (ServerDataHandler.Instance)
         link = ServerDataHandler.Instance.CommonData.MoreGames;

        if (link.Contains("."))
        {
            Application.OpenURL("market://details?id="+link);
        }
        else
        {
            Application.OpenURL("market://search?q=pub:"+link);
        }
    }

    public void RateUS()
    {
        Application.OpenURL("market://details?id=" + Application.identifier);
    }

    public void ExitPage()
    {

        if (SliderMenuAdHandler.Instance)
        {
            SliderMenuAdHandler.Instance.ShowMoreGamesSideOff();
        }

        if (BridgeManager_Bigcode.Instance)
            BridgeManager_Bigcode.Instance.ShowExitPage();

    }

    public static int LevelFailCount;
    public void ShowLevelFailedInterstitialAd()
    {

        if (ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData.isalterAds)
        {
            if (LevelFailCount % 2 == 0)
            {
                StartCoroutine(ShowLevelFailedInterstitialAd(0));

                Debug.LogError("Level Failed Ad Called");

                SetReachedPage(AnalyticsHandler_BigCode.Page.LF_page);
            }
        }
        else
        {
            StartCoroutine(ShowLevelFailedInterstitialAd(0));

            SetReachedPage(AnalyticsHandler_BigCode.Page.LF_page);
        }

        LevelFailCount++;

    }

    public void ShowPauseToHomeInterstitalAd()
    {
        StartCoroutine(ShowLevelCompletedInterstitialAd(0));

        SetReachedPage(AnalyticsHandler_BigCode.Page.Pause_To_Home);
    }

    private IEnumerator ShowLevelFailedInterstitialAd(int index)
    {
        if (PluginManager.Instance.isInternetAvailable)
        {
            yield return new WaitForSeconds(ServerDataHandler.Instance.BaseData.AdsDelay.LevelFailedDelay);

            ShowInterstialAd();

        }
    }

    public void ShowLevelCompletedInterstitialAd()
    {
        StartCoroutine(ShowLevelCompletedInterstitialAd(0));

        SetReachedPage(AnalyticsHandler_BigCode.Page.LC_page);
    }

    private IEnumerator ShowLevelCompletedInterstitialAd(int index)
    {
        if (PluginManager.Instance.isInternetAvailable)
        {
            yield return new WaitForSeconds(ServerDataHandler.Instance.BaseData.AdsDelay.LevelCompletedDelay);

            ShowInterstialAd();
        }
    }
   
    public void OnPromoCodeSuccess(int gift)
    {
        PluginManager.Instance.ShowToast("Promo Code Applied Successfully..!");
    }


    public void SetReachedPage(AnalyticsHandler_BigCode.Page page)
    {
        if (AnalyticsHandler_BigCode.Instance)
            AnalyticsHandler_BigCode.Instance.SetReachedPage(page);
    }

    public void SetReachedPage(AnalyticsHandler_BigCode.Page page,int levelNo)
    {
        if (AnalyticsHandler_BigCode.Instance)
            AnalyticsHandler_BigCode.Instance.SetReachedPage(page,levelNo);
    }

    public void SetReachedPage(string page)
    {
        if (AnalyticsHandler_BigCode.Instance)
            AnalyticsHandler_BigCode.Instance.SetReachedPage(page);
    }

    public void SetReachedPage(string page, int levelNo)
    {
        if (AnalyticsHandler_BigCode.Instance)
            AnalyticsHandler_BigCode.Instance.SetReachedPage(page, levelNo);
    }




    public void Login(LoginHandler.LoginType loginType)
    {
        LoginHandler.Instance.Login(loginType);
    }



    //public delegate void PlayGamesSiginIn(bool isSuccess);
    //public static event PlayGamesSiginIn PlayGamesSignInEvent;


    public void GoogleSignIn()
    {
        if (GooglePlayGamesHandler.Instance)
        {
            if (GooglePlayGamesHandler.Instance.isSignIn)
                GooglePlayGamesHandler.Instance.Logout();
            else
                GooglePlayGamesHandler.Instance.SignIn();

        }
    }

    public void GoogleSignInOnStart()
    {
        if (GooglePlayGamesHandler.Instance)
        {
            GooglePlayGamesHandler.Instance.SignIn();
        }
    }

    public void OnSignIn(bool isSignIn)
    {
        BridgeManager_Bigcode.Instance.OnSignInEvent(isSignIn);
    }

    public void OnSignOut(bool isSignIn)
    {
        BridgeManager_Bigcode.Instance.OnSignInEvent(isSignIn);
    }







    void CreateDefaultServerJSON()
    {
        CommonData CommonData = new CommonData();

        CommonData.MenuAd = new MenuAd();
        CommonData.MenuAd.excludeMenuAds = new List<string>();
        CommonData.MenuAd.excludeMenuAds.Add("com.freesimgames.truckdriversimulator");
        CommonData.MenuAd.excludeMenuAds.Add("com.mtsfreegames.bikerace2018");
        CommonData.MenuAd.excludeMenuAds.Add("com.mtsfreegames.hollywoodgangsters");


        CommonData.MenuAd.BigCodeAd = new Bigcode();
        CommonData.MenuAd.BigCodeAd.Portrait_Link = "https://bigcode.s3.amazonaws.com/main/ads/shark.jpg";
        CommonData.MenuAd.BigCodeAd.Landscape_Link = "https://bigcode.s3.amazonaws.com/main/ads/shark.jpg";
        CommonData.MenuAd.BigCodeAd.PackageName = "com.mtsfreegames.bikerace2018";

        CommonData.Discount = new Discount();
        CommonData.Discount.DisCountText = "Get Special discount of flat 50% off";
        CommonData.Discount.isAvailable = true;

        CommonData.ExitPage = new ExitPage();
        CommonData.ExitPage.Landscape_Link = "https://integergames.s3.amazonaws.com/exit.html";
        CommonData.ExitPage.Portrait_Link = "https://integergames.s3.amazonaws.com/exit.html";

        CommonData.MoreGames = "com.freesimgames.truckdriversimulator";

        CommonData.MiniMoreGames = new MiniMoreGames();
        CommonData.MiniMoreGames.folderLocation = "https://bigcode.s3.amazonaws.com/allicons/";
        CommonData.MiniMoreGames.miniGamesPkg = new List<string>();
        CommonData.MiniMoreGames.miniGamesPkg.Add("com.freesimgames.truckdriversimulator");
        CommonData.MiniMoreGames.miniGamesPkg.Add("com.mtsfreegames.bikerace2018");
        CommonData.MiniMoreGames.miniGamesPkg.Add("com.mtsfreegames.hollywoodgangsters");



        BaseData BaseData = new BaseData();
        BaseData.MenuAds = new List<ADS>();
        BaseData.MenuAds.Add(ADS.ADMOB);
        BaseData.MenuAds.Add(ADS.BIGCODE);

        BaseData.VideoAds = new List<ADS>();
        BaseData.VideoAds.Add(ADS.VUNGLE);
        BaseData.VideoAds.Add(ADS.UNITY);
        BaseData.VideoAds.Add(ADS.ADMOB);

        BaseData.RewardVideoAds = new List<ADS>();
        BaseData.RewardVideoAds.Add(ADS.VUNGLE);
        BaseData.RewardVideoAds.Add(ADS.UNITY);
        BaseData.RewardVideoAds.Add(ADS.ADMOB);

        BaseData.InterstitialAds = new List<ADS>();
        BaseData.InterstitialAds.Add(ADS.VUNGLE);
        BaseData.InterstitialAds.Add(ADS.UNITY);
        BaseData.InterstitialAds.Add(ADS.ADMOB);

        BaseData.Promo = new Promo();
        BaseData.Promo.isEnabled = true;
        BaseData.Promo.PromoCode = "SAMBA123";
        BaseData.Promo.PromoCoins = 100;

        BaseData.RatePopUp = new RatePopUp();
        BaseData.RatePopUp.levelNumbers = new List<int>();
        BaseData.RatePopUp.levelNumbers.Add(3);
        BaseData.RatePopUp.levelNumbers.Add(7);
        BaseData.RatePopUp.levelNumbers.Add(10);
        BaseData.RatePopUp.message = "Love Little Ganesha - Running Game &#10;  Please Rate us!";
        BaseData.RatePopUp.Coins = 100;

        BaseData.SharePopUp = new SharePopUp();
        BaseData.SharePopUp.levelNumbers = new List<int>();
        BaseData.SharePopUp.levelNumbers.Add(5);
        BaseData.SharePopUp.levelNumbers.Add(8);
        BaseData.SharePopUp.levelNumbers.Add(9);
        BaseData.SharePopUp.message = "Love Little Ganesha - Running Game &#10;  Please Share!";
        BaseData.SharePopUp.Coins = 100;

        BaseData.Share = new Share();
        BaseData.Share.FacebookShare = "http://bigcode.co.in/littleganesha/fb/";
        BaseData.Share.TwitterShare = "http://bigcode.co.in/littleganesha/wa/";
        BaseData.Share.WhatsAppShare = "http://bigcode.co.in/littleganesha/tw/";

        BaseData.AdsDelay = new AdsDelay();
        BaseData.AdsDelay.AdToAdDelay = 30;
        BaseData.AdsDelay.LevelCompletedDelay = 1;
        BaseData.AdsDelay.LevelFailedDelay = 1;


        Debug.Log(JsonUtility.ToJson(CommonData));
        Debug.Log(JsonUtility.ToJson(BaseData));
    }











    //public delegate void CoinsAdded(int total);
    //public static event CoinsAdded CoinsAddedEvent;
    //public void AddCoins(int addCoins, bool showToast = false)
    //{


    //    //int coinsHave = PlayerPrefs.GetInt(GameConstants.COINS_PLAYERPREF, 0);
    //    //coinsHave += addCoins;
    //    //PlayerPrefs.SetInt(GameConstants.COINS_PLAYERPREF, coinsHave);
    //    if (showToast && (addCoins > 0))
    //    {
    //        ShowToast(addCoins + " "+LanguageHandler.GetCurrentLanguageText("Coins Added Successfully"));
    //    }
    //    if (CoinsAddedEvent != null)
    //    {
    //        CoinsAddedEvent(addCoins);
    //    }

    //}

    public IEnumerator ShowDelayToast(float Delaytime, string msg)
    {
        yield return new WaitForSeconds(Delaytime);

        if (PluginManager.Instance)
            PluginManager.Instance.ShowToast(msg);
    }

    public bool IsRated
    {
        get
        {
            return PlayerPrefs.HasKey(GameConstants_BigCode.RATED);
        }
    }


}

