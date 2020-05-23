//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Monetization;

//public class UnityAdsHandler_BigCode : MonoBehaviour
//{

//    public static UnityAdsHandler_BigCode Instance;

//    public string AdID;

//    public static string REWARDED_VIDEO = "rewardedVideo";
//    public static string SKIPPABLE_VIDEO = "video";


//    bool testMode = false;

//    [HideInInspector]
//    public bool isIntialized;

//    private void Awake()
//    {
//        Instance = this;
//    }


//    public void Initialize()
//    {
//        if (Monetization.isSupported && !Monetization.isInitialized)
//        {
//            Monetization.Initialize(AdID, testMode);
//        }

//        isIntialized = true;
//        //PluginManager.Instance.OnPluginInitializationCompleted();
//    }


//    public void ShowVideoAd()
//    {
//        //ShowOptions options = new ShowOptions();
//        //options.resultCallback = OnVideoAdCompleted;

//        //Advertisement.Show(null, options);

//        ShowAdCallbacks options = new ShowAdCallbacks();
//        options.finishCallback = OnVideoAdCompleted;

//        ShowAdPlacementContent ad = Monetization.GetPlacementContent(SKIPPABLE_VIDEO) as ShowAdPlacementContent;
//        ad.Show(options);

//    }

//    public void RequestRewardedVideo()
//    {
//        if (IsAdReady(REWARDED_VIDEO))
//        {
//            Debug.LogError("Unity Rewarded Video Request Loaded");

//            PluginManager.Instance.LoadedRewardedAd = ADS.UNITY;
//        }
//        else
//        {
//            Debug.LogError("Unity Rewarded Video Request Failed");

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
//        ShowAdCallbacks options = new ShowAdCallbacks();
//        options.finishCallback = OnRewardedVideoAdCompleted;

//        ShowAdPlacementContent ad = Monetization.GetPlacementContent(REWARDED_VIDEO) as ShowAdPlacementContent;
//        ad.Show(options);
//    }


//    public void OnVideoAdCompleted(ShowResult result)
//    {
//        switch (result)
//        {
//            case ShowResult.Finished:

//                break;
//            case ShowResult.Skipped:

//                break;
//            case ShowResult.Failed:


//                break;
//        }

//    }



//    public void OnRewardedVideoAdCompleted(ShowResult result)
//    {
//        switch (result)
//        {
//            case ShowResult.Finished:

//                PluginManager.Instance.OnRewardedVideoFinished();

//                break;
//            case ShowResult.Skipped:
//                PluginManager.Instance.OnRewardVideoMiddleClosed();

//                break;
//            case ShowResult.Failed:


//                break;
//        }

//    }

//    public bool IsAdReady(string placementID)
//    {

//        return Monetization.IsReady(placementID);
//    }


//    public void RequestInterstitial()
//    {
//        if (IsAdReady(SKIPPABLE_VIDEO))
//        {
//            // PluginManager.Instance.ShowToast("Unity Request Loaded");
//            Debug.LogError("Unity Request Loaded");

//            PluginManager.Instance.LoadedAd = ADS.UNITY;

//        }
//        else
//        {
//            //PluginManager.Instance.ShowToast("Unity Request Failed");
//            Debug.LogError("Unity Request Failed");

//            PluginManager.Instance.LoadedAd = ADS.NEWREQUEST;

//            PluginManager.Instance.RequestInterstitialIndex++;

//            if (ServerDataHandler.Instance.BaseData.InterstitialAdsShowingType == ADSHOWINGTYPE.BACKUP_ROTATIONAL)
//            {
//                if (PluginManager.Instance.RequestInterstitialIndex == ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount && PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame < ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount)
//                {
//                    PluginManager.Instance.RequestInterstitialIndex = 0;
//                }
//                else if (PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame == ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount)
//                {
//                    PluginManager.Instance.RequestInterstitialIndex = ServerDataHandler.Instance.BaseData.BackupRotationalInterstitialAdsCount;
//                }


//                if (PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame == ServerDataHandler.Instance.BaseData.InterstitialAds.Count)
//                {
//                    PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame = 1;
//                }
//                else
//                {
//                    PluginManager.Instance.BackupRotationalInterstitialAdsCountInGame++;
//                }
//            }

//            if (PluginManager.Instance.RequestInterstitialIndex >= ServerDataHandler.Instance.BaseData.InterstitialAds.Count)
//            {
//                PluginManager.Instance.RequestInterstitialIndex = 0;
//            }

//            PluginManager.RotationInterstitialAdsPreference = PluginManager.Instance.RequestInterstitialIndex;

//            PluginManager.Instance.RequestInterstitial(PluginManager.Instance.RequestInterstitialIndex);
//        }
//    }








//    private string promoPlacementId;

//    public void ShowPromo(PromoPlacement promoPlacement)
//    {
//        switch (promoPlacement)
//        {
//            case PromoPlacement.Menu_Page:

//                //if (!GameDataHandler.Instance.GameData.IsSubscription50Purchased)
//                //    promoPlacementId = "menuPlacement";
//                //else
//                promoPlacementId = "menuPagePlacement";

//                break;
//            case PromoPlacement.TrainSelection_Page:

//                //if (!GameDataHandler.Instance.GameData.IsSubscription50Purchased)
//                //    promoPlacementId = "upgradePlacement";
//                //else
//                promoPlacementId = "trainSelectionPlacement";

//                break;
//            case PromoPlacement.Store_Page:

//                //if (!GameDataHandler.Instance.GameData.IsSubscription50Purchased)
//                //    promoPlacementId = "shopPlacement";
//                //else
//                promoPlacementId = "storePlacement";

//                break;
//            case PromoPlacement.LevelComplete_Page:
//                //if (!GameDataHandler.Instance.GameData.IsSubscription50Purchased)
//                //    promoPlacementId = "levelfailPlacement";
//                //else
//                promoPlacementId = "levelCompletePlacement";
//                break;

//            case PromoPlacement.LevelFail_Page:
//                //if (!GameDataHandler.Instance.GameData.IsSubscription50Purchased)
//                //    promoPlacementId = "levelfailPlacement";
//                //else
//                promoPlacementId = "levelFailPlacement";
//                break;
//        }


//        StartCoroutine(ShowPromo());





//    }


//    IEnumerator ShowPromo()
//    {
//        yield return new WaitForSeconds(1f);




//#if !UNITY_EDITOR

//        PlacementContent placementContent = Monetization.GetPlacementContent(promoPlacementId);

//        if (placementContent != null)
//        {
//            PromoAdPlacementContent promo = placementContent as PromoAdPlacementContent;

//            if (promo != null)
//                promo.Show();
//        }
       
//#endif

//    }


//}

//public enum PromoPlacement
//{
//    Menu_Page,
//    LevelComplete_Page,
//    TrainSelection_Page,
//    Store_Page,
//    LevelFail_Page
//}
