//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class VungleAdHandler_BigCode : MonoBehaviour
//{

//    public static VungleAdHandler_BigCode Instance;

//    public string RewardedVideoPlacementID;
//    public string SkippableAdPlacementID;


//    [HideInInspector]
//    public bool isInitialized;

//    public string appID;


//    private void Awake()
//    {
//        Instance = this;
//    }




//    public void Initialize()
//    {
//        Vungle.onInitializeEvent += OnInitializeEvent;

//        Vungle.init(appID/*, new string[] { RewardedVideoPlacementID,SkippableAdPlacementID}*/);
//    }

//    public void OnInitializeEvent()
//    {
//        Vungle.onInitializeEvent -= OnInitializeEvent;

//        //LoadAd(RewardedVideoPlacementID);
//        //LoadAd(SkippableAdPlacementID);

//        isInitialized = true;
//        //PluginManager.Instance.OnPluginInitializationCompleted();
//    }

//    public void RequestInterstitial()
//    {


//        //Debug.LogError("No Ads Purchased::::"+GameDataHandler.Instance.GameData.NoadsPurchased);
//        //Debug.LogError("Vungle Is Available::::"+Vungle.isAdvertAvailable(SkippableAdPlacementID));


//        if (PluginManager.Instance.isInternetAvailable &&
//            //PluginManager.Instance.lastShown > ServerDataHandler.Instance.BaseData.AdsDelay.AdToAdDelay && 
//            !GameDataHandler.Instance.GameData.NoadsPurchased)
//        {

//            if (!Vungle.isAdvertAvailable(SkippableAdPlacementID))
//            {
//                LoadAd(SkippableAdPlacementID);
//            }
//            else
//            {
//                PluginManager.Instance.LoadedAd = ADS.VUNGLE;
//                InterstialIsLoaded = true;

//                Debug.LogError("Vungle Ad Loaded");
//            }

//        }



//    }


//    public void RequestRewardedVideo()
//    {
//        if (Vungle.isAdvertAvailable(RewardedVideoPlacementID))
//        {
//            Debug.LogError("Vungle Rewarded Ad Loaded");

//            PluginManager.Instance.LoadedRewardedAd = ADS.VUNGLE;
//        }
//        else
//        {
//            LoadAd(RewardedVideoPlacementID);
//        }
//    }


//    public void ShowRewardedVideo()
//    {
//        ShowAd(RewardedVideoPlacementID);
//    }

//    public void LoadAd(string placementID)
//    {
//        Vungle.adPlayableEvent += adPlayableEvent;
//        Vungle.loadAd(placementID);
//    }

//    [HideInInspector]
//    public bool InterstialIsLoaded;
//    void adPlayableEvent(string placemnetID, bool playable)
//    {
//        Vungle.adPlayableEvent -= adPlayableEvent;

//        if (placemnetID.Equals(SkippableAdPlacementID))
//        {
//            if (!playable)
//            {
//                //PluginManager.Instance.ShowToast("Vungle Request Failed");
//                Debug.LogError("Vungle Request Failed");

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
//            else
//            {
//                //PluginManager.Instance.ShowToast("Vungle Request Loaded");
//                Debug.LogError("Vungle Request Loaded");

//                PluginManager.Instance.LoadedAd = ADS.VUNGLE;
//            }

//            InterstialIsLoaded = playable;


//        }

//        else if (placemnetID.Equals(RewardedVideoPlacementID))
//        {


//            if (playable)
//            {
//                Debug.LogError("Vungle Rewarded Ad Loaded");

//                PluginManager.Instance.LoadedRewardedAd = ADS.VUNGLE;
//            }
//            else
//            {
//                Debug.LogError("Vungle Rewarded Ad Failed");

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


//        //PluginManager.Instance.ShowToast("Vungle Ad Loaded::"+playable);
//    }

//    public void ShowAd(string placementID)
//    {
//        Vungle.onAdFinishedEvent += OnAdFinishedEvent;
//        Vungle.playAd(placementID);
//    }

//    public bool IsAdAvailable(string placementID)
//    {
//        return Vungle.isAdvertAvailable(placementID);
//    }

//    void OnAdFinishedEvent(string placementID, AdFinishedEventArgs args)
//    {
//        Vungle.onAdFinishedEvent -= OnAdFinishedEvent;

//        if (args.IsCompletedView)
//        {
//            if (placementID.Equals(RewardedVideoPlacementID))
//                PluginManager.Instance.OnRewardedVideoFinished();
//        }
//        else
//        {
//            if (placementID.Equals(RewardedVideoPlacementID))
//                PluginManager.Instance.OnRewardVideoMiddleClosed();
//        }

//    }



//    void OnApplicationPause(bool pauseStatus)
//    {
//        if (pauseStatus)
//        {
//            Vungle.onPause();
//        }
//        else
//        {
//            Vungle.onResume();
//        }
//    }
//}
