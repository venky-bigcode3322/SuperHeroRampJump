using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;

public class MoPubAdsHandler : MonoBehaviour
{

    public string IntertistialID,RewardedVideoID,bannerID;

    private bool isInterstitialLoading, isInterstitialLoaded;
    private bool isRewardVideoLoading, isRewardVideoLoaded;


    public static MoPubAdsHandler Instance;

    public void OnMoPubInitialized()
    {

        Debug.Log("Mopub Initialized Successfully");

    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MoPub.LoadInterstitialPluginsForAdUnits(new string[] { IntertistialID });
        MoPub.LoadRewardedVideoPluginsForAdUnits(new string[] { RewardedVideoID });
        MoPub.LoadBannerPluginsForAdUnits(new string[] { bannerID });




    }


    private void OnEnable()
    {
        MoPubManager.OnAdLoadedEvent += OnAdLoadedEvent;
        MoPubManager.OnAdFailedEvent += OnAdFailedEvent;

        MoPubManager.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MoPubManager.OnInterstitialFailedEvent += OnInterstitialFailedEvent;
        MoPubManager.OnInterstitialDismissedEvent += OnInterstitialDismissedEvent;

        MoPubManager.OnRewardedVideoLoadedEvent += OnRewardedVideoLoadedEvent;
        MoPubManager.OnRewardedVideoFailedEvent += OnRewardedVideoFailedEvent;
        MoPubManager.OnRewardedVideoFailedToPlayEvent += OnRewardedVideoFailedToPlayEvent;
        MoPubManager.OnRewardedVideoClosedEvent += OnRewardedVideoClosedEvent;

    }

    private void OnDisable()
    {
        // Remove all event handlers
        MoPubManager.OnAdLoadedEvent -= OnAdLoadedEvent;
        MoPubManager.OnAdFailedEvent -= OnAdFailedEvent;

        MoPubManager.OnInterstitialLoadedEvent -= OnInterstitialLoadedEvent;
        MoPubManager.OnInterstitialFailedEvent -= OnInterstitialFailedEvent;
        MoPubManager.OnInterstitialDismissedEvent -= OnInterstitialDismissedEvent;

        MoPubManager.OnRewardedVideoLoadedEvent -= OnRewardedVideoLoadedEvent;
        MoPubManager.OnRewardedVideoFailedEvent -= OnRewardedVideoFailedEvent;
        MoPubManager.OnRewardedVideoFailedToPlayEvent -= OnRewardedVideoFailedToPlayEvent;
        MoPubManager.OnRewardedVideoClosedEvent -= OnRewardedVideoClosedEvent;
    }



    public void RequestInterstitial()
    {
        if (!isInterstitialLoaded && !isInterstitialLoading)
        {
            isInterstitialLoading = true;
            MoPub.RequestInterstitialAd(IntertistialID);
        }
        
    }

    bool isBannerHide;
    public void RequestBanner()
    {
        if (!isBannerHide)
            MoPub.RequestBanner(bannerID, MoPub.AdPosition.TopCenter, MoPub.MaxAdSize.Width300Height50);
        else if(isBannerHide)
            ShowBanner(true);

    }

    public void ShowBanner(bool isShow)
    {
        if (!isShow)
        {
            isBannerHide = true;
        }
        else
        {
            isBannerHide = false;

        }

        MoPub.ShowBanner(bannerID, isShow);
    }



    public void ShowInterstitial()
    {
        if (isInterstitialLoaded)
        {
            isInterstitialLoaded = false;
            MoPub.ShowInterstitialAd(IntertistialID);
        }
    }


    public void RequestRewardedVideo()
    {
        if (!isRewardVideoLoaded && !isRewardVideoLoading)
        {
            isRewardVideoLoading = true;
            MoPub.RequestRewardedVideo(RewardedVideoID);
        }
    }


    public void ShowRewardedVideo()
    {
        if (isRewardVideoLoaded)
        {
            isRewardVideoLoaded = false;
            MoPub.ShowRewardedVideo(RewardedVideoID);
        }
        else
        {
            if(!PluginManager.Instance.isInternetAvailable)
            {
                PluginManager.Instance.ShowToast("Network not Available");
            }
            else
            {
                PluginManager.Instance.ShowToast("Ad loading please wait...");
            }
           
        }
    }

    private void OnAdLoadedEvent(string adUnitId, float height)
    {
        Debug.LogError("adloded:::" + adUnitId);
    }


    private void OnAdFailedEvent(string adUnitId, string error)
    {
        Debug.LogError("adFailed:::" + adUnitId+"::::::"+error);

    }






















    // Interstitial Events
    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        isInterstitialLoading = false;
        isInterstitialLoaded = true;

        Debug.LogError("OnInterstitialLoadedEvent");
    }

    private void OnInterstitialFailedEvent(string adUnitId, string error)
    {
        isInterstitialLoading = false;
        isInterstitialLoaded = false;


        Debug.LogError("OnInterstitialFailedEvent");

    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        Debug.LogError("OnInterstitialDismissedEvent");
    }


























    // Rewarded Video Events
    private void OnRewardedVideoLoadedEvent(string adUnitId)
    {
        isRewardVideoLoading = false;
        isRewardVideoLoaded = true;

        var availableRewards = MoPub.GetAvailableRewards(adUnitId);
       
        Debug.LogError("OnRewardedVideoLoadedEvent");
    }
    private void OnRewardedVideoFailedEvent(string adUnitId, string error)
    {
        isRewardVideoLoading = false;
        isRewardVideoLoaded = false;

        Debug.LogError("OnRewardedVideoFailedEvent:::"+ error);
    }

    private void OnRewardedVideoFailedToPlayEvent(string adUnitId, string error)
    {
        Debug.LogError("OnRewardedVideoFailedToPlayEvent");

    }

    private void OnRewardedVideoClosedEvent(string adUnitId)
    {
        Debug.LogError("OnRewardedVideoClosedEvent");
        PluginManager.Instance.OnRewardedVideoFinished();

        PluginManager.Instance.RequestRewardedVideoAd();

       

    }
}
