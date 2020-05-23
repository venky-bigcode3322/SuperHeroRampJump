//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AppLovinAdsHandler : MonoBehaviour {


//    public string SDK_KEY;

//    public string InterstitialZoneID;
//    public string RewardedZoneID;

//    private bool IsPreloadingInterstitial = false;
//    private bool IsPreloadingRewardedVideo = false;

//    public static AppLovinAdsHandler Instance;



//    private void Awake()
//    {
//        Instance = this;
//    }


//    // Use this for initialization
//    void Start () {

//        // Set SDK key and initialize SDK
//        AppLovin.SetSdkKey(SDK_KEY);
//        AppLovin.InitializeSdk();
//        AppLovin.SetTestAdsEnabled("false");
//        AppLovin.SetUnityAdListener("PluginManager");
//       // AppLovin.SetRewardedVideoUsername("demo_user");

//    }

//    private void onAppLovinEventReceived(string ev)
//    {
//        Debug.LogError("AppLovin Event:::::::::::::" + ev);

//        if (IsPreloadingInterstitial && (ev.Equals("LOADEDINTER") || ev.Equals("LOADFAILED")))
//        {
//            IsPreloadingInterstitial = false;

//            if (ev.Equals("LOADEDINTER"))
//            {

//                Debug.LogError("Applovin Request Loaded");

//                PluginManager.Instance.LoadedAd = ADS.APPLOVIN;

//            }
//            else
//            {
//                Debug.LogError("Applovin Request Failed");

//                //PluginManager.Instance.ShowToast("IronSource Request Failed");

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
//        }
//        else if (IsPreloadingRewardedVideo && (ev.Equals("LOADEDREWARDED") || ev.Equals("LOADFAILED")))
//        {

//            IsPreloadingRewardedVideo = false;

//            if (ev.Equals("LOADEDREWARDED"))
//            {
//                Debug.LogError("Applovin Rewarded Ad Loaded");

//                PluginManager.Instance.LoadedRewardedAd = ADS.APPLOVIN;
//            }
//            else
//            {
//                Debug.LogError("AppLovin Rewarded Ad Failed");

//                PluginManager.Instance.LoadedRewardedAd = ADS.NEWREQUEST;

//                PluginManager.Instance.RewardedVideoAdIndex++;

//                if (ServerDataHandler.Instance.BaseData.RewardedAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
//                {
//                    if (PluginManager.Instance.RewardedVideoAdIndex == ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount && PluginManager.Instance.BackupRotationalRewardedAdsCountInGame < ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount)
//                    {
//                        PluginManager.Instance.RewardedVideoAdIndex = 0;
//                    }
//                    else if (PluginManager.Instance.BackupRotationalRewardedAdsCountInGame == ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount)
//                    {
//                        PluginManager.Instance.RewardedVideoAdIndex = ServerDataHandler.Instance.BaseData.BackupRotationalRewardedAdsCount;
//                    }

//                    if (PluginManager.Instance.BackupRotationalRewardedAdsCountInGame == ServerDataHandler.Instance.BaseData.RewardVideoAds.Count)
//                    {
//                        PluginManager.Instance.BackupRotationalRewardedAdsCountInGame = 1;
//                    }
//                    else
//                    {
//                        PluginManager.Instance.BackupRotationalRewardedAdsCountInGame++;
//                    }
//                }

//                if (PluginManager.Instance.RewardedVideoAdIndex >= ServerDataHandler.Instance.BaseData.RewardVideoAds.Count)
//                {
//                    PluginManager.Instance.RewardedVideoAdIndex = 0;
//                }

//                PluginManager.RotationRewardedAdsPreference = PluginManager.Instance.RewardedVideoAdIndex;



//                PluginManager.Instance.RequestRewardedVideoAd(PluginManager.Instance.RewardedVideoAdIndex);
//            }
//        }
//        else if (ev.Equals("REWARDAPPROVED"))
//        {
//            PluginManager.Instance.OnRewardedVideoFinished();
//        }
//    }

//    public bool isInterstitialReady()
//    {
//        return AppLovin.HasPreloadedInterstitial(null);
//    }

//    public void RequestInterstitial()
//    {
//        if (AppLovin.HasPreloadedInterstitial(null))
//        {
//            IsPreloadingInterstitial = false;
//            Debug.LogError("Applovin interstial loaded");
//            PluginManager.Instance.LoadedAd = ADS.APPLOVIN;
//        }
//        else
//        {
//            Debug.LogError("App Lovin Loading...");

//            IsPreloadingInterstitial = true;
//            AppLovin.PreloadInterstitial(null);
//        }
//    }

//    public void ShowInterstitial()
//    {
//        AppLovin.ShowInterstitial();
//    }

//    public void RequestRewardedVideo()
//    {
//        if (AppLovin.IsIncentInterstitialReady(null))
//        {
//            IsPreloadingRewardedVideo = false;

//            Debug.LogError("Applovin Rewarded Ad Loaded");

//            PluginManager.Instance.LoadedRewardedAd = ADS.APPLOVIN;
//        }
//        else
//        {

//            Debug.LogError("Applovin Rewarded Ad Loading");


//            IsPreloadingRewardedVideo = true;
//            AppLovin.LoadRewardedInterstitial(null);
//        }
//    }

//    public void ShowRewardedVideoAd()
//    {
//        AppLovin.ShowRewardedInterstitial();
//    }
//}
