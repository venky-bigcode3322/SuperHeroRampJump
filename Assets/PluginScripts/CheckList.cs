using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckList : MonoBehaviour {

	// Use this for initialization
	void Start () {


        //Calling Ads From Setup


        //Note We Need To Call this method when game starts
        PluginManager.Instance.RequestInterstitial();

        //To Show Ad At LevelCompleted
        PluginManager.Instance.ShowLevelCompletedInterstitialAd();

        //To Show Ad At LevelFailed
        PluginManager.Instance.ShowLevelFailedInterstitialAd();

        //To Show Ad At Pause To Home
        PluginManager.Instance.ShowPauseToHomeInterstitalAd();

        //To Show Rewarded Video
        //Rewarded Video Callback you will observe in (BridgeManager_Bigcode.Instance.MainThreadOnRewardedFinished) method 
        PluginManager.Instance.ShowRewardedVideoAd(RewardType_BigCode.TrippleReward);

        //Facebook Login with out share
        //PluginManager.Instance.FacebookLogin(FacebookHandler_BigCode.SignIn.NONE);

        //Facebook Login with share
        //Facebook Callback  BridgeManager_Bigcode.Instance.OnFacebookShare
        //PluginManager.Instance.FacebookLogin(FacebookHandler_BigCode.SignIn.SHARE);


        //Calling Analytics Sample Call
        PluginManager.Instance.SetReachedPage(AnalyticsHandler_BigCode.Page.Game_Page);

        //Get Server or Local Date
        System.DateTime date =  PluginManager.Instance.CurrentDateTime;



        //Checks
        //if new game download .json files from links and copy to streaming assets folder and change there extentions .json to .txt
        //Check version numbers in file which are in streaming assets before relese.








    }


}
