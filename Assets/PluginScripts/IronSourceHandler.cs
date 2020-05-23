//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class IronSourceHandler : MonoBehaviour {

//    public string App_ID;

//    public static IronSourceHandler Instance;

//    [HideInInspector]
//    public bool isInitialized;


//    private void Awake()
//    {
//        Instance = this;
//    }

//    private void Start()
//    {
//        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
//        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
//        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
//        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
//        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
//        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
//        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;





//        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
//        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
//        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
//        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
//        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
//        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
//        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
//    }



//    public void Initialize()
//    {
//        IronSource.Agent.init(App_ID, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL);
//        IronSource.Agent.validateIntegration();

//        isInitialized = true;
//        //PluginManager.Instance.OnPluginInitializationCompleted();

        
//    }


//    public void RequestRewardedVideo()
//    {
//        if (IsRewardedVideoAvailable())
//        {
//            Debug.LogError("Ironsource Rewarded Ad Loaded");

//            PluginManager.Instance.LoadedRewardedAd = ADS.IRONSOURCE;
//        }
//        else
//        {
//            Debug.LogError("Ironsource Rewarded Ad Failed");

//            PluginManager.Instance.LoadedRewardedAd = ADS.NEWREQUEST;

//            PluginManager.Instance.RewardedVideoAdIndex++;


//            if (ServerDataHandler.Instance.BaseData.RewardedAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
//            {
//                if (PluginManager.Instance.RewardedVideoAdIndex == ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount && PluginManager.Instance.BackupRotationalRewardedAdsCountInGame < ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount)
//                {
//                    PluginManager.Instance.RewardedVideoAdIndex = 0;
//                }
//                else if (PluginManager.Instance.BackupRotationalRewardedAdsCountInGame == ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount)
//                {
//                    PluginManager.Instance.RewardedVideoAdIndex = ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount;
//                }

//                if (PluginManager.Instance.BackupRotationalRewardedAdsCountInGame == ServerDataHandler.Instance.BaseData.RewardVideoAds.Count)
//                {
//                    PluginManager.Instance.BackupRotationalRewardedAdsCountInGame = 1;
//                }
//                else
//                {
//                    PluginManager.Instance.BackupRotationalRewardedAdsCountInGame++;
//                }
//            }

//            if (PluginManager.Instance.RewardedVideoAdIndex >= ServerDataHandler.Instance.BaseData.RewardVideoAds.Count)
//            {
//                PluginManager.Instance.RewardedVideoAdIndex = 0;
//            }

//            PluginManager.RotationRewardedAdsPreference = PluginManager.Instance.RewardedVideoAdIndex;


//            PluginManager.Instance.RequestRewardedVideoAd(PluginManager.Instance.RewardedVideoAdIndex);
//        }

//    }
    

//    public void ShowRewardedVideo()
//    {
//        IronSource.Agent.showRewardedVideo();

//    }

//    public bool IsRewardedVideoAvailable()
//    {
//        return IronSource.Agent.isRewardedVideoAvailable();
//    }

//    private void OnApplicationPause(bool isPaused)
//    {
//        IronSource.Agent.onApplicationPause(isPaused);
//    }







//    public void RequestInterstitial()
//    {
//        if (PluginManager.Instance.isInternetAvailable && 
//            //PluginManager.Instance.lastShown > ServerDataHandler.Instance.BaseData.AdsDelay.AdToAdDelay && 
//            !GameDataHandler.Instance.GameData.NoadsPurchased) 
//        {
//            if (!IronSource.Agent.isInterstitialReady())
//            {
//                IronSource.Agent.loadInterstitial();
//            }
//            else
//            {
//                Debug.LogError("Ironsource interstial loaded");
//                PluginManager.Instance.LoadedAd = ADS.IRONSOURCE;
//            }

//        }
//    }

//    public bool IsInterstitialLoaded()
//    {
//        return IronSource.Agent.isInterstitialReady();
//    }

//    public void ShowInterstitial()
//    {
//        IronSource.Agent.showInterstitial();
//    }











































//    void RewardedVideoAdOpenedEvent()
//    {

//    }

//    void RewardedVideoAdClosedEvent()
//    {
//        if (isRewardAvailable)
//        {
//            isRewardAvailable = false;

//            PluginManager.Instance.OnRewardedVideoFinished();
//        }
//        else
//        {
//            PluginManager.Instance.OnRewardVideoMiddleClosed();
//        }
//    }

//    void RewardedVideoAvailabilityChangedEvent(bool available)
//    {
//        bool rewardedVideoAvailability = available;

//    }
//    void RewardedVideoAdStartedEvent()
//    {

//    }
//    void RewardedVideoAdEndedEvent()
//    {

//    }

//    private bool isRewardAvailable;
//    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
//    {
//        isRewardAvailable = true;

//    }
//    void RewardedVideoAdShowFailedEvent(IronSourceError error)
//    {

//    }


















//    void InterstitialAdLoadFailedEvent(IronSourceError error)
//    {
//        Debug.LogError("IronSource Request Failed");

//        //PluginManager.Instance.ShowToast("IronSource Request Failed");

//        PluginManager.Instance.LoadedAd = ADS.NEWREQUEST;

//        PluginManager.Instance.RequestInterstitialIndex++; //1
       
//        if (ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
//        {
//            if (PluginManager.Instance.RequestInterstitialIndex == ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount && PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame < ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount)
//            {
//                PluginManager.Instance.RequestInterstitialIndex = 0;
//            }
//            else if (PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame == ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount)
//            {
//                PluginManager.Instance.RequestInterstitialIndex = ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount;
//            }

//            if (PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame == ServerDataHandler.Instance.BaseData.InterstitialAds.Count)
//            {
//                PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame = 1;
//            }
//            else
//            {
//                PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame++;
//            }
//        }

//        if (PluginManager.Instance.RequestInterstitialIndex >= ServerDataHandler.Instance.BaseData.InterstitialAds.Count)
//        {
//            PluginManager.Instance.RequestInterstitialIndex = 0;
//        }

//        PluginManager.RotationInterstitialAdsPreference = PluginManager.Instance.RequestInterstitialIndex;

//        PluginManager.Instance.RequestInterstitial(PluginManager.Instance.RequestInterstitialIndex);
//    }

//    void InterstitialAdShowSucceededEvent()
//    {

//    }

//    void InterstitialAdShowFailedEvent(IronSourceError error)
//    {

//    }

//    void InterstitialAdClickedEvent()
//    {

//    }

//    void InterstitialAdClosedEvent()
//    {

//    }

//    void InterstitialAdReadyEvent()
//    {
//        //PluginManager.Instance.ShowToast("IronSource Request Loaded");

//        Debug.LogError("IronSource Request Loaded");

//        PluginManager.Instance.LoadedAd = ADS.IRONSOURCE;

//    }

//    void InterstitialAdOpenedEvent()
//    {

//    }
//}
