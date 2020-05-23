using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using GameAnalyticsSDK;

public class Dummy_MenuPageHandler : MonoBehaviour {

    public void Start()
    {


        PluginManager.Instance.RequestRewardedVideoAd();
    }


    public void Play()
    {
        SceneManager.LoadScene("GameScene");

        //PluginManager.Instance.PurchaseProduct(0,PluginManager.InAppPriceType.Normal_Subscription);

    }

    public void MoreGames()
    {
        if (PluginManager.Instance)
            PluginManager.Instance.MoreGames();


        //GameAnalytics.NewDesignEvent("Achievements:Killing:Neutral:10_Kills");

    }


    public void Share()
    {
        if (PluginManager.Instance)
            PluginManager.Instance.NativeShare(NativeShare.COMMON);

        //GameAnalytics.NewDesignEvent("RequestInterstial");


    }



    public void SharePopup(int levelNumber)
    {
        levelNumber = 5;


        if (PluginManager.Instance)
            PluginManager.Instance.ShowSharePopUp(levelNumber);
    }


    public void RatePopup(int levelNumber)
    {

        levelNumber = 3;

        if (PluginManager.Instance)
            PluginManager.Instance.ShowRatePopUp(levelNumber);
    }


    public void FacebookShare()
    {
        //if (PluginManager.Instance)
        //    PluginManager.Instance.FacebookLogin(FacebookHandler_BigCode.SignIn.SHARE);

        LoginHandler.Instance.SignOut();

        PluginManager.Instance.ShowRewardedVideoAd(RewardType_BigCode.DoubleReward);

    }
}
