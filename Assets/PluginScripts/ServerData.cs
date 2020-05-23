using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CommonData
{
    public MenuAd MenuAd;
    
    public Discount Discount;

    public ExitPage ExitPage;

    public string MoreGames;

    public MiniMoreGames MiniMoreGames;
}


[System.Serializable]
public class BaseData
{
    public ADSHOWINGTYPE InterstitialAdsShowingType = ADSHOWINGTYPE.ROTATIONAL;

    public ADSHOWINGTYPE RewardedAdsShowingType = ADSHOWINGTYPE.ROTATIONAL;

    public int BackupRotationalInterstitialAdsCount;

    public int BackupRotationalRewardedAdsCount;

    public List<ADS> MenuAds;
    
    public List<ADS> VideoAds;

    public List<ADS> RewardVideoAds;

    public List<ADS> InterstitialAds;

    public Promo Promo;

    public Share Share;

    public RatePopUp RatePopUp;

    public SharePopUp SharePopUp;

    public AdsDelay AdsDelay;

    public bool isBannerAdAvailable;

    public bool isMiniBannerAdAvailable;

    public bool isAdaptiveBannerAdAvailable;


    public Version version;

    public bool IncludeNonConsumableInSubscription;

    public bool isalterAds;

    public ExitGame ExitGame;

    public int exitPageAdType;

}


[System.Serializable]
public struct ExitGame
{
    public string gameLink;
    public string gamePackage;
}

[System.Serializable]
public class SmileyKidoosCoupon
{
    public string coupon_code;
    public int discount;
    public string expiry_date;
    public DateTime unity_expiry_date;
}




[System.Serializable]
public struct MenuAd
{
    public List<string> excludeMenuAds;
    public Bigcode BigCodeAd;

}

public enum ADS
{
    BIGCODE,
    UNITY,
    ADMOB,
    VUNGLE,
    IRONSOURCE,
    FACEBOOK,
    APPLOVIN,
    MOPUB,
    NONE,
    LOADING,
    NEWREQUEST
}

public enum ADTYPE
{
    BANNER,
    INTERSTITIAL
}

public enum ADSHOWINGTYPE
{
    NORMAL,
    ROTATIONAL,
    BACKUP_ROTATIONAL
}


[System.Serializable]
public struct Bigcode
{
    public string Landscape_Link;
    public string Portrait_Link;
    public string PackageName;
}

[System.Serializable]
public struct Discount
{
    public bool isAvailable;
    public string DisCountText;
}

[System.Serializable]
public struct ExitPage
{
    public string Landscape_Link;
    public string Portrait_Link;
}

[System.Serializable]
public struct MiniMoreGames
{
    public bool isEnabled;
    public string folderLocation;
    public List<string> miniGamesPkg;
    public int version;
}



[System.Serializable]
public struct Promo
{
    public bool isEnabled;
    public int PromoCoins;
    public string PromoCode;
}

[System.Serializable]
public struct RatePopUp
{
    public bool isEnabled;
    public List<int> levelNumbers;
    public string message;
    public int Coins;
    public bool isNotNowButton;

}


[System.Serializable]
public struct SharePopUp
{
    public List<int> levelNumbers;
    public string message;
    public int Coins;
}

[System.Serializable]
public struct Share
{
    public string FacebookShare;
    public string WhatsAppShare;
    public string TwitterShare;
}

[System.Serializable]
public struct AdsDelay
{
    public float AdToAdDelay;
    public float LevelCompletedDelay;
    public float LevelFailedDelay;
}

[System.Serializable]
public struct Version
{
    public string armv7;
    public string x86;
}



