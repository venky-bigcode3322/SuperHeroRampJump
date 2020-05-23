
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using AudienceNetwork;

//public class FacebookAdsHandler : MonoBehaviour
//{


//    private InterstitialAd interstitialAd;
//    private RewardedVideoAd rewardedVideoAd;
//    public string interstitial_placement_id, rewarded_placement_id;
//    private bool isLoadedInterstitial,isLoadedRewarded,isRewardedLoaded;
//    private bool didClose;

//    public static FacebookAdsHandler Instance;

//    void Awake()
//    {
//        Instance = this;
//    }

//    // Use this for initialization
//    public void Initialize()
//    {
//        interstitialAd = new InterstitialAd(interstitial_placement_id);

       

//        rewardedVideoAd = new RewardedVideoAd(rewarded_placement_id);

//        interstitialAd.Register(gameObject);

//        this.rewardedVideoAd.Register(this.gameObject);

//        interstitialCallBacks();

//        rewardedVideoCallBacks();
//    }


//    public void interstitialCallBacks()
//    {
//        // Set delegates to get notified on changes or when the user interacts with the ad.
//        interstitialAd.InterstitialAdDidLoad = delegate ()
//        {
//            Debug.LogError("Interstitial ad loaded.");

//            isLoadedInterstitial = true;
//            didClose = false;

//            PluginManager.Instance.LoadedAd = ADS.FACEBOOK;

//            //PluginManager.Instance.ShowToast("Facebook Ad Request Loaded");
//            Debug.LogError("Facebook Ad Request Loaded");
//        };


//        interstitialAd.InterstitialAdDidFailWithError = delegate (string error)
//        {
//            Debug.LogError("Facebook Interstitial ad failed to load with error: " + error);

//            //PluginManager.Instance.ShowToast("Facebook Ad Failed To Load Error:"+error);

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

//        };

//        interstitialAd.InterstitialAdWillLogImpression = delegate ()
//        {
//            Debug.LogError("Interstitial ad logged impression.");
//        };

//        interstitialAd.InterstitialAdDidClick = delegate ()
//        {
//            Debug.LogError("Interstitial ad clicked.");
//        };

//        interstitialAd.InterstitialAdDidClose = delegate ()
//        {
//            Debug.LogError("Interstitial ad did close.");

//            didClose = true;
//            if (interstitialAd != null)
//            {
//                interstitialAd.Dispose();
//            }
//        };

//#if UNITY_ANDROID
//        interstitialAd.interstitialAdActivityDestroyed = delegate ()
//        {
//            if (!didClose)
//            {
//                Debug.LogError("Interstitial activity destroyed without being closed first.");
//                Debug.LogError("Game should resume.");
//            }
//        };
//#endif
//    }



//    public void rewardedVideoCallBacks()
//    {
//        // Set delegates to get notified on changes or when the user interacts with the ad.
//        this.rewardedVideoAd.RewardedVideoAdDidLoad = (delegate ()
//        {

//            Debug.LogError("Facebook RewardedVideo ad loaded.");

//            isRewardedLoaded = true;

//            PluginManager.Instance.LoadedRewardedAd = ADS.FACEBOOK;
//        });
//        this.rewardedVideoAd.RewardedVideoAdDidFailWithError = (delegate (string error)
//        {

//            Debug.LogError("Facebook RewardedVideo ad failed to load with error: " + error);

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

//        });
//        this.rewardedVideoAd.RewardedVideoAdWillLogImpression = (delegate ()
//        {
//            Debug.LogError("RewardedVideo ad logged impression.");
//        });
//        this.rewardedVideoAd.RewardedVideoAdDidClick = (delegate ()
//        {
//            Debug.LogError("RewardedVideo ad clicked.");
//        });

//        this.rewardedVideoAd.RewardedVideoAdDidClose = (delegate ()
//        {
//            Debug.LogError("Rewarded video ad did close.");

//            PluginManager.Instance.OnRewardedVideoFinished();



//        });






//        // For S2S validation you need to register the following two callback
//        this.rewardedVideoAd.RewardedVideoAdDidSucceed = (delegate ()
//        {
//            Debug.LogError("Rewarded video ad validated by server");


//        });
//        this.rewardedVideoAd.RewardedVideoAdDidFail = (delegate ()
//        {
//            Debug.LogError("Rewarded video ad not validated, or no response from server");

//            PluginManager.Instance.OnRewardVideoMiddleClosed();

//        });
//    }


//    public bool isInterstitialLoaded()
//    {
//        return isLoadedInterstitial;
//    }


//    public void RequestInterstitial()
//    {
//        if (PluginManager.Instance.isInternetAvailable &&
//            //PluginManager.Instance.lastShown > ServerDataHandler.Instance.BaseData.AdsDelay.AdToAdDelay && 
//            !GameDataHandler.Instance.GameData.NoadsPurchased)
//        {
//            //PluginManager.Instance.ShowToast("Requesting Facebook Ad");

//            if (!isLoadedInterstitial)
//            {
//                interstitialAd.LoadAd();
//            }
//            else
//            {
//                //PluginManager.Instance.ShowToast("Facebook Ad Request Loaded");

//                Debug.LogError("Facebook Ad Request Loaded");
//                PluginManager.Instance.LoadedAd = ADS.FACEBOOK;
//            }
//        }
//    }



//    // Show button
//    public void ShowInterstitial()
//    {
//        if (isLoadedInterstitial)
//        {
//            interstitialAd.Show();
//            isLoadedInterstitial = false;
//        }

//    }

//    void OnDestroy()
//    {
//        // Dispose of interstitial ad when the scene is destroyed
//        if (interstitialAd != null)
//        {
//            interstitialAd.Dispose();
//        }

//        if (this.rewardedVideoAd != null)
//        {
//            this.rewardedVideoAd.Dispose();
//        }

//        Debug.Log("InterstitialAdTest was destroyed!");
//    }





//    public void RequestRewardedVideo()
//    {

//        if (!isRewardedLoaded)
//        {
//            rewardedVideoAd.LoadAd();

//        }
//        else
//        {
//            Debug.LogError("Facebook RewardedVideo ad loaded.");

//            PluginManager.Instance.LoadedRewardedAd = ADS.FACEBOOK;
//        }

       

//    }


//    public void ShowRewardedVideo()
//    {
//        if (isRewardedLoaded)
//        {
//            isRewardedLoaded = false;
//            this.rewardedVideoAd.Show();
//        }
//    }

//    public bool IsRewardedVideoAvailable()
//    {
//        return isRewardedLoaded;
//    }
//}
