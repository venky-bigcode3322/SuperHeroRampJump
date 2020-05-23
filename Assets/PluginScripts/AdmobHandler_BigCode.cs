//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using System;
//using UnityEngine.SceneManagement;

//public class AdmobHandler_BigCode : MonoBehaviour {


//    public static AdmobHandler_BigCode Instance;

//    [HideInInspector]
//    public bool isIntialized;

//    public string appID;
    
//    public string InterstitialAdID;

//    public string rewardedVideoAdID;

//    public string BannerAdId;

//    // Use this for initialization
//    void Awake () {

//        Instance = this;

//    }


//    private void Start()
//    {
//        IntializeSDK();
//    }

//    public void Initialize()
//    {
//        if(interstitial == null)
//        interstitial = new InterstitialAd(InterstitialAdID);

//        if (ServerDataHandler.Instance.BaseData.MenuAds[0] == ADS.ADMOB)
//        {
//            RequestInterstitial(RequestPage.Menu_Page);
//        }
//    }


//    public InterstitialAd interstitial;

    
//    public void RequestInterstitial(RequestPage requestPage)
//    {
//        if (PluginManager.Instance.isInternetAvailable)
//        {
//            if (requestPage == RequestPage.Menu_Page)
//            {
//                if (!interstitial.IsLoaded())
//                {
//                    interstitial.OnAdLoaded += HandleOnInterstitialAdLoaded;
//                    interstitial.OnAdFailedToLoad += HandleOnInterstitialAdFailedToLoad;

//                    AdRequest request = new AdRequest.Builder().Build();
//                    interstitial.LoadAd(request);
//                }
//            }
//            else if (requestPage == RequestPage.Game_Page)
//            {
//                if (PluginManager.Instance.lastShown > ServerDataHandler.Instance.BaseData.AdsDelay.AdToAdDelay && !GameDataHandler.Instance.GameData.NoadsPurchased)
//                {
//                    if (!interstitial.IsLoaded())
//                    {
//                        interstitial.OnAdLoaded += HandleOnInterstitialAdLoaded;
//                        interstitial.OnAdFailedToLoad += HandleOnInterstitialAdFailedToLoad;

//                        AdRequest request = new AdRequest.Builder().Build();
//                        interstitial.LoadAd(request);
//                    }
//                    else
//                    {

//                        Debug.LogError("Admob Request Loaded");

//                        PluginManager.Instance.LoadedAd = ADS.ADMOB;
//                    }
//                }
//                else
//                {
//                    //PluginManager.Instance.ShowToast("Admob Request Still In Delay Time");

//                    Debug.LogError("Admob Request Still In Delay Time");

//                    PluginManager.Instance.LoadedAd = ADS.NEWREQUEST;

//                    PluginManager.Instance.RequestInterstitialIndex++;

//                    if (ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
//                    {
//                        if (PluginManager.Instance.RequestInterstitialIndex == ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount && PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame < ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount)
//                        {
//                            PluginManager.Instance.RequestInterstitialIndex = 0;
//                        }
//                        else if (PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame == ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount)
//                        {
//                            PluginManager.Instance.RequestInterstitialIndex = ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount;
//                        }

//                        if (PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame == ServerDataHandler.Instance.BaseData.InterstitialAds.Count)
//                        {
//                            PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame = 1;
//                        }
//                        else
//                        {
//                            PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame++;
//                        }
//                    }

//                    if (PluginManager.Instance.RequestInterstitialIndex >= ServerDataHandler.Instance.BaseData.InterstitialAds.Count)
//                    {
//                        PluginManager.Instance.RequestInterstitialIndex = 0;
//                    }

//                    PluginManager.RotationInterstitialAdsPreference = PluginManager.Instance.RequestInterstitialIndex;

//                    PluginManager.Instance.RequestInterstitial(PluginManager.Instance.RequestInterstitialIndex);

//                }
//            }
//        }
//    }


//    void IntializeSDK()
//    {
//        //if(!appID.Equals(string.Empty))
//        //MobileAds.Initialize(appID);

//        if (interstitial == null)
//            interstitial = new InterstitialAd(InterstitialAdID);


//        // intialize rewarded video
//        rewardBasedVideo = RewardBasedVideoAd.Instance;

//       // BigCodeLibHandler.Instance.ShowToast("" + rewardBasedVideo);
//    }


//    public void ShowInterstialAd()
//    {
//        interstitial.OnAdClosed += HandleOnInterstitialAdClosed;
//        interstitial.Show();
//    }

//    public void HandleOnInterstitialAdLoaded(object sender, EventArgs args)
//    {
//        interstitial.OnAdLoaded -= HandleOnInterstitialAdLoaded;
//        interstitial.OnAdFailedToLoad -= HandleOnInterstitialAdFailedToLoad;

//        MainThread.Call(() => {

//            if (SceneManager.GetActiveScene().buildIndex == 0)
//            {
//                isIntialized = true;
//                PluginManager.Instance.OnAdmobInitialized();
//            }

//            PluginManager.Instance.lastShown = 0;

//            Debug.LogError("Admob Request Loaded");

//            //PluginManager.Instance.ShowToast("Admob Request Loaded");

//            PluginManager.Instance.LoadedAd = ADS.ADMOB;

//        });

//    }

//    public void HandleOnInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        interstitial.OnAdFailedToLoad -= HandleOnInterstitialAdFailedToLoad;
//        interstitial.OnAdLoaded -= HandleOnInterstitialAdLoaded;

//        Debug.LogError("Admob Request Failed");


//        MainThread.Call(() => {

//            if (SceneManager.GetActiveScene().buildIndex == 0)
//            {
//                isIntialized = true;
//                PluginManager.Instance.OnAdmobInitialized();
//            }
//            else
//            {
//                //PluginManager.Instance.ShowToast("Admob Request Failed");

//                PluginManager.Instance.LoadedAd = ADS.NEWREQUEST;


//                PluginManager.Instance.RequestInterstitialIndex++;


//                if (ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
//                {
//                    if (PluginManager.Instance.RequestInterstitialIndex == ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount && PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame < ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount)
//                    {
//                        PluginManager.Instance.RequestInterstitialIndex = 0;
//                    }
//                    else if (PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame == ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount)
//                    {
//                        PluginManager.Instance.RequestInterstitialIndex = ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount;
//                    }

//                    if (PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame == ServerDataHandler.Instance.BaseData.InterstitialAds.Count)
//                    {
//                        PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame = 1;
//                    }
//                    else
//                    {
//                        PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame++;
//                    }
//                }

//                if (PluginManager.Instance.RequestInterstitialIndex >= ServerDataHandler.Instance.BaseData.InterstitialAds.Count)
//                {
//                    PluginManager.Instance.RequestInterstitialIndex = 0;
//                }

//                PluginManager.RotationInterstitialAdsPreference = PluginManager.Instance.RequestInterstitialIndex;

//                PluginManager.Instance.RequestInterstitial(PluginManager.Instance.RequestInterstitialIndex);
//            }


//        });



//    }


//    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
//    {
//        interstitial.OnAdClosed -= HandleOnInterstitialAdClosed;


//        if (SceneManager.GetActiveScene().buildIndex == 1)
//        {
//#if UNITY_EDITOR
//            MenuAdHandler_BigCode.Instance.LoadGameScene();
//#endif

//#if !UNITY_EDITOR

//            MainThread.Call(OnAdCloseMainThread);
//#endif
//        }
//    }


//    public void OnAdCloseMainThread()
//    {
//        //GooglePlayGamesHandler.Instance.Initialize();
//        //GameDataHandler.Instance.NoInternetLoadGameData();
//        MenuAdHandler_BigCode.Instance.LoadGameScene();
//    }

























//    //BannerView
//    public BannerView bannerView;
//    public void ShowBannerView()
//    {
//        if (PluginManager.Instance.isInternetAvailable)
//        {
//            //PluginManager.Instance.ShowToast("Hi"+ ServerDataHandler.Instance.BaseData.isBannerAdAvailable);

//            if (ServerDataHandler.Instance.BaseData.isBannerAdAvailable)
//            {
//                if (bannerView == null)
//                {
//                    bannerView = new BannerView(BannerAdId, AdSize.MediumRectangle, 20000, 55);
//                    AdRequest request = new AdRequest.Builder().Build();
//                    bannerView.LoadAd(request);
//                }
//                else
//                {
//                    bannerView.Show();
//                }
//            }
//        }
//    }

//    public bool IsBannerAdAvailable()
//    {
//        if (PluginManager.Instance.isInternetAvailable)
//            return ServerDataHandler.Instance.BaseData.isBannerAdAvailable;
//        else
//            return false;
//    }

//    public void HideBannerView()
//    {
//        if (bannerView != null)
//            bannerView.Hide();
//    }



























//    //Mini Banner View
//    public BannerView MiniBannerView;
//    public void ShowMiniBannerView()
//    {
//        if (PluginManager.Instance.isInternetAvailable)
//        {
//            //PluginManager.Instance.ShowToast("MiniBannerView" + ServerDataHandler.Instance.BaseData.isMiniBannerAdAvailable);

//            if (ServerDataHandler.Instance.BaseData.isMiniBannerAdAvailable)
//            {
//                if (MiniBannerView == null)
//                {
//                    MiniBannerView = new BannerView(BannerAdId, AdSize.Banner, AdPosition.Bottom);
//                    AdRequest request = new AdRequest.Builder().Build();
//                    MiniBannerView.LoadAd(request);
//                }
//                else
//                {
//                    MiniBannerView.Show();
//                }
//            }
//        }
//    }



//    public bool IsMiniBannerAdAvailable()
//    {
//        if (PluginManager.Instance.isInternetAvailable)
//            return ServerDataHandler.Instance.BaseData.isMiniBannerAdAvailable;
//        else
//            return false;
//    }

//    public void HideMiniBannerView()
//    {
//        if (MiniBannerView != null)
//            MiniBannerView.Hide();
//    }















//    //Adaptive Banner View
//    public BannerView AdaptiveBannerView;
//    public void ShowAdaptiveBannerView()
//    {
//        if (PluginManager.Instance.isInternetAvailable)
//        {
//            //PluginManager.Instance.ShowToast("MiniBannerView" + ServerDataHandler.Instance.BaseData.isMiniBannerAdAvailable);

//            if (ServerDataHandler.Instance.BaseData.isAdaptiveBannerAdAvailable)
//            {
//                if (AdaptiveBannerView == null)
//                {
//                    AdSize adaptiveSize =
//                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

//                    AdaptiveBannerView = new BannerView(BannerAdId, adaptiveSize, AdPosition.Bottom);
//                    AdRequest request = new AdRequest.Builder().Build();
//                    AdaptiveBannerView.LoadAd(request);
//                }
//                else
//                {
//                    AdaptiveBannerView.Show();
//                }
//            }
//        }
//    }



//    public bool IsAdaptiveBannerAdAvailable()
//    {
//        if (PluginManager.Instance.isInternetAvailable)
//            return ServerDataHandler.Instance.BaseData.isAdaptiveBannerAdAvailable;
//        else
//            return false;
//    }

//    public void HideAdaptiveBannerView()
//    {
//        if (AdaptiveBannerView != null)
//            AdaptiveBannerView.Hide();
//    }

































//    private RewardBasedVideoAd rewardBasedVideo;
//    private void RequestRewardBasedVideo()
//    {



//        if (!IsRewardedVideoLoaded())
//        {

//            rewardBasedVideo.OnAdLoaded += onHandleRewardedVideoLoad;
//            rewardBasedVideo.OnAdFailedToLoad += onHandleRewardedVideoFailed;

//            AdRequest request = new AdRequest.Builder().Build();
//            this.rewardBasedVideo.LoadAd(request, rewardedVideoAdID);
//        }
//        else
//        {
//            Debug.LogError("Admob Rewarded Video Request Loaded");
//            PluginManager.Instance.LoadedRewardedAd = ADS.ADMOB;
//        }

//    }

//    private bool IsRewardedVideoLoaded()
//    {
//        return rewardBasedVideo.IsLoaded();
//    }

//    public void ShowRewardedVideo()
//    {
//        rewardBasedVideo.OnAdRewarded += OnAdRewarded;
//        rewardBasedVideo.OnAdClosed += OnAdClosed;


//        rewardBasedVideo.Show();
//    }

//    private void OnAdClosed(object sender,EventArgs args)
//    {
//        rewardBasedVideo.OnAdRewarded -= OnAdRewarded;
//        rewardBasedVideo.OnAdClosed -= OnAdClosed;

//        PluginManager.Instance.LoadedRewardedAd = ADS.NONE;

//        PluginManager.Instance.RequestRewardedVideoAd();


//        PluginManager.Instance.OnRewardVideoMiddleClosed();

//    }

//    private void OnAdRewarded(object sender, EventArgs args)
//    {
//        rewardBasedVideo.OnAdRewarded -= OnAdRewarded;
//        rewardBasedVideo.OnAdClosed -= OnAdClosed;

//        PluginManager.Instance.OnRewardedVideoFinished();
//    }



//    //int Rewardindex;
//    //RewardType_BigCode RewardPlacement;

//    //public void LoadAndShowRewardVideo(int index,RewardType_BigCode placement)
//    //{
//    //    RewardPlacement = placement;


//    //    this.Rewardindex = index;

//    //    RequestRewardBasedVideo();
//    //}

    
//    public void RequestRewardedVideo()
//    {
//        RequestRewardBasedVideo();

//    }

//    private void onHandleRewardedVideoLoad(object sender, EventArgs args)
//    {
//        rewardBasedVideo.OnAdLoaded -= onHandleRewardedVideoLoad;
//        rewardBasedVideo.OnAdFailedToLoad -= onHandleRewardedVideoFailed;

//        Debug.LogError("Admob Rewarded Video Request Loaded");

//        PluginManager.Instance.LoadedRewardedAd = ADS.ADMOB;

//    }

//    private void onHandleRewardedVideoFailed(object sender, EventArgs args)
//    {
//        //BigCodeLibHandler.Instance.DismissLoading();

//        rewardBasedVideo.OnAdLoaded -= onHandleRewardedVideoLoad;
//        rewardBasedVideo.OnAdFailedToLoad -= onHandleRewardedVideoFailed;

//        Debug.LogError("Admob Rewarded Video Request Failed");

//        PluginManager.Instance.LoadedRewardedAd = ADS.NEWREQUEST;

//        PluginManager.Instance.RewardedVideoAdIndex++;

//        if (ServerDataHandler.Instance.BaseData.RewardedAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
//        {
//            if (PluginManager.Instance.RewardedVideoAdIndex == ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount && PluginManager.Instance.BackupRotationalRewardedAdsCountInGame < ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount)
//            {
//                PluginManager.Instance.RewardedVideoAdIndex = 0;
//            }
//            else if (PluginManager.Instance.BackupRotationalRewardedAdsCountInGame == ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount)
//            {
//                PluginManager.Instance.RewardedVideoAdIndex = ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount;
//            }

//            if (PluginManager.Instance.BackupRotationalRewardedAdsCountInGame == ServerDataHandler.Instance.BaseData.RewardVideoAds.Count)
//            {
//                PluginManager.Instance.BackupRotationalRewardedAdsCountInGame = 1;
//            }
//            else
//            {
//                PluginManager.Instance.BackupRotationalRewardedAdsCountInGame++;
//            }
//        }

//        if (PluginManager.Instance.RewardedVideoAdIndex >= ServerDataHandler.Instance.BaseData.RewardVideoAds.Count)
//        {
//            PluginManager.Instance.RewardedVideoAdIndex = 0;
//        }
//        PluginManager.RotationRewardedAdsPreference = PluginManager.Instance.RewardedVideoAdIndex;


//        PluginManager.Instance.RequestRewardedVideoAd(PluginManager.Instance.RewardedVideoAdIndex);

//    }
//}

//public enum RequestPage
//{
//    Menu_Page,
//    Game_Page
//}