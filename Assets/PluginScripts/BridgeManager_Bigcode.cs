using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class BridgeManager_Bigcode : MonoBehaviour {

    public static BridgeManager_Bigcode Instance;

	// Use this for initialization
	void Awake () {

        Instance = this;
	}

    private void Start()
    {
        AddingSubscriptionsTrainsData();
    }

    public void AddingSubscriptionsTrainsData()
    {
        if (PlayerPrefs.HasKey("TRAINS"))
        {
            string allTrainIDs = PlayerPrefs.GetString("TRAINS");
            string[] allTrains = allTrainIDs.Split('#');

            if (allTrains.Length == 10)
            {
                for (int i = 10; i < 15; i++)
                {
                    allTrains[i] = "0";
                }

                allTrainIDs = "";

                foreach (string charec in allTrains)
                {
                    allTrainIDs += charec;
                    allTrainIDs += "#";
                }

                PlayerPrefs.SetString("TRAINS", allTrainIDs);
            }
        }
    }

    public void RestorePurchases(int index)
    {


        switch (index)
        {
            case 0:
                PurchasedNoAds();

                if (PluginManager.Instance && SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1)
                    PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("No Ads Purchased Successfully"));

                break;
            case 1:


                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:



                break;
            case 7:


                break;
            case 8:



                break;




            case 9:
            case 10:
            case 11:


                GameDataHandler.Instance.GameData.IsSubscription50Purchased = true;

                if(SubscriptionHandler.Instance && SubscriptionHandler.Instance.VIPButton)
                SubscriptionHandler.Instance.VIPButton.SetActive(false);

                if (InappPurchaseHandler.Instance && InappPurchaseHandler.Instance.subscriptions.Count > 0)
                    GameDataHandler.Instance.GameData.SubScriptionExpiryDate = JsonUtility.ToJson((GameData.JsonDateTime)InappPurchaseHandler.Instance.subscriptions[InappPurchaseHandler.Instance.InAppProducts[index]].getExpireDate());


                // Restricting for Same day Should not give benifits
                if (GameDataHandler.Instance.GameData.lastBenfitGivenDate.Equals(string.Empty) || (!GameDataHandler.Instance.GameData.lastBenfitGivenDate.Equals(string.Empty) &&
                    ((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.lastBenfitGivenDate)).Day != PluginManager.Instance.CurrentDateTime.Day))
                {



                    switch (index)
                    {
                        case 9:

                            GameDataHandler.Instance.GameData.SubscriptionType = Weekly;

                            WeeklyBenfit();


                            break;
                        case 10:

                            GameDataHandler.Instance.GameData.SubscriptionType = Monthly;

                            MonthlyBenfit();

                            break;
                        case 11:

                            GameDataHandler.Instance.GameData.SubscriptionType = Yearly;


                            YearlyBenfit();

                            break;
                    }
                }
                else
                {

                    switch (index)
                    {
                        case 9:

                            GameDataHandler.Instance.GameData.SubscriptionType = Weekly;

                            Weekly_Subscription_Benfit();


                            break;
                        case 10:

                            GameDataHandler.Instance.GameData.SubscriptionType = Monthly;

                            Monthly_Subscription_Benfit();

                            break;
                        case 11:

                            GameDataHandler.Instance.GameData.SubscriptionType = Yearly;


                            Yearly_Subscription_Benfit();

                            break;
                    }
                }


                break;

            case 12:







                break;
            case 13:


                break;
        }
    }


    



   



    public void OnSubscriptionExpire()
    {

        //if(MenuPageHandler.Instance)
        //MenuPageHandler.Instance.VIPButton.SetActive(true);

        //No Ads Revert
        switch (GameDataHandler.Instance.GameData.SubscriptionType)
        {
            case Weekly:


                break;
            case Monthly:



                break;
            case Yearly:



                break;
          
        }
    }


    void PurchasedNoAds()
    {
        PlayerPrefs.SetString(GameConstants_BigCode.NOADS, "success");

        GameDataHandler.Instance.GameData.NoadsPurchased = true;

    }

    


    void RemoveAdsFree()
    {
        if (InappPurchaseHandler.Instance)
            if (!InappPurchaseHandler.Instance.products[InappPurchaseHandler.Instance.InAppProducts[0]].hasReceipt)
            {
                GameDataHandler.Instance.GameData.NoadsPurchased = false;
            }
    }


    public void Give_OfflineSubscriptionBenifit()
    {
        if (!PluginManager.Instance.isInternetAvailable && GameDataHandler.Instance.GameData.IsSubscription50Purchased)
        {
            switch (GameDataHandler.Instance.GameData.SubscriptionType)
            {
                case Weekly:

                    //Debug.Log("Subsctiontion expiry date:::::" + ((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.SubScriptionExpiryDate)).Day);
                    //Debug.Log("Last Benifit Given date:::::" + ((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.lastBenfitGivenDate)).Day);
                    //Debug.Log("Current date:::::" + PluginManager.Instance.CurrentDateTime.Day);
                    //Debug.Log("Condition:::" + ((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.SubScriptionExpiryDate)).CompareTo(PluginManager.Instance.CurrentDateTime));



                    if (((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.SubScriptionExpiryDate)).CompareTo(PluginManager.Instance.CurrentDateTime) > 0)
                    {
                        if (((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.lastBenfitGivenDate)).Day != PluginManager.Instance.CurrentDateTime.Day)
                            Weekly_Subscription_DailyBenifit();
                    }
                    else
                    {
                        OnSubscriptionExpire();
                    }

                    break;
                case Monthly:

                    if (((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.SubScriptionExpiryDate)).CompareTo(PluginManager.Instance.CurrentDateTime) > 0)
                    {
                        if (((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.lastBenfitGivenDate)).Day != PluginManager.Instance.CurrentDateTime.Day)
                            Monthly_Subscription_DailyBenifit();
                    }
                    else
                    {
                        OnSubscriptionExpire();
                    }

                    break;
                case Yearly:

                    if (((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.SubScriptionExpiryDate)).CompareTo(PluginManager.Instance.CurrentDateTime) > 0)
                    {
                        if (((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.lastBenfitGivenDate)).Day != PluginManager.Instance.CurrentDateTime.Day)
                            Yearly_Subscription_DailyBenifit();
                    }
                    else
                    {
                        OnSubscriptionExpire();
                    }

                    break;
            }
        }
    }


    // Weekley Subscription
    public void WeeklyBenfit()
    {
        Weekly_Subscription_DailyBenifit();

        Weekly_Subscription_Benfit();

    }

    public void Weekly_Subscription_DailyBenifit()
    {

        int previousDays = 0;

        // Giving previous missed subscription data
        if (!GameDataHandler.Instance.GameData.lastBenfitGivenDate.Equals(string.Empty))
        {
            DateTime lastOpenedDate = ((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.lastBenfitGivenDate));
            System.TimeSpan previousDate = PluginManager.Instance.CurrentDateTime - lastOpenedDate;
            previousDays = previousDate.Days;

        }
        else
        {
            previousDays = 1;
        }



        GameDataHandler.Instance.GameData.lastBenfitGivenDate = JsonUtility.ToJson((GameData.JsonDateTime)PluginManager.Instance.CurrentDateTime);

        PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Subscription Benefits Given Successfully"));

        //Give Daily Benifit Here
        


        if (LocalNotificationHandler_BigCode.Instance)
            LocalNotificationHandler_BigCode.Instance.ScheduleNotification(1, "Hey!! Collect your subscription rewards for the day ...");

    }

    public void Weekly_Subscription_Benfit()
    {

    }

    //Monthly Subscription
    public void MonthlyBenfit()
    {
        Monthly_Subscription_DailyBenifit();

        Monthly_Subscription_Benfit();
    }

    public void Monthly_Subscription_DailyBenifit()
    {

        int previousDays = 0;

        // Giving previous missed subscription data
        if (!GameDataHandler.Instance.GameData.lastBenfitGivenDate.Equals(string.Empty))
        {
            DateTime lastOpenedDate = ((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.lastBenfitGivenDate));
            System.TimeSpan previousDate = PluginManager.Instance.CurrentDateTime - lastOpenedDate;

            previousDays = previousDate.Days;
        }
        else
        {
            previousDays = 1;
        }

        GameDataHandler.Instance.GameData.lastBenfitGivenDate = JsonUtility.ToJson((GameData.JsonDateTime)PluginManager.Instance.CurrentDateTime);

        PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Subscription Benefits Given Successfully"));

        //Give Daily Benifit Here

       


        if (LocalNotificationHandler_BigCode.Instance)
            LocalNotificationHandler_BigCode.Instance.ScheduleNotification(1, "Hey!! Collect your subscription rewards for the day ...");

    }


    public void Monthly_Subscription_Benfit()
    {
        
    }


    //Yearly Subscription
    public void YearlyBenfit()
    {
        Yearly_Subscription_DailyBenifit();

        Yearly_Subscription_Benfit();
    }

    public void Yearly_Subscription_DailyBenifit()
    {

        int previousDays = 0;

        // Giving previous missed subscription data
        if (!GameDataHandler.Instance.GameData.lastBenfitGivenDate.Equals(string.Empty))
        {
            DateTime lastOpenedDate = ((DateTime)JsonUtility.FromJson<GameData.JsonDateTime>(GameDataHandler.Instance.GameData.lastBenfitGivenDate));
            System.TimeSpan previousDate = PluginManager.Instance.CurrentDateTime - lastOpenedDate;

            previousDays = previousDate.Days;
        }
        else
        {
            previousDays = 1;
        }

        // Storing Date which for benfit should not come same day
        GameDataHandler.Instance.GameData.lastBenfitGivenDate = JsonUtility.ToJson((GameData.JsonDateTime)PluginManager.Instance.CurrentDateTime);

        PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Subscription Benefits Given Successfully"));

        //Give Daily Benifit Here


        if (LocalNotificationHandler_BigCode.Instance)
            LocalNotificationHandler_BigCode.Instance.ScheduleNotification(1, "Hey!! Collect your subscription rewards for the day ...");

    }

    public void Yearly_Subscription_Benfit()
    {

        
    }



    public const int Weekly = 1;
    public const int Monthly = 2;
    public const int Yearly = 3;



    private CallMethod callMethod;
    public void MainThreadOnRewardedFinished()
    {

        if (Time.timeScale == 0)
            Time.timeScale = 1;


        PluginManager.Instance.RequestRewardedVideoAd();



        switch (PluginManager.Instance.RewardPlacement)
        {
            case RewardType_BigCode.Coins:


                break;
        }

        //PluginManager.Instance.ShowToast("Rewarded");
    }


    public void OnRewardVideoMiddleClosed()
    {
        PluginManager.Instance.RequestRewardedVideoAd();
    }

    public void OnNoRewardedVideoAdsFound()
    {

    }

    public void OnFacebookShare(FacebookHandler_BigCode.SignIn signIn)
    {
        switch (signIn)
        {
            case FacebookHandler_BigCode.SignIn.SHARE:

                break;
            case FacebookHandler_BigCode.SignIn.SHAREPOPUP:

                break;
        }
    }

    public void OnSignInEvent(bool isSignIn)
    {

        //if (MenuPageHandler.Instance)
        //    MenuPageHandler.Instance.ChangeSignInSprite(isSignIn);


        if (isSignIn)
        {
            
        }
        else
        {

        }
    }




    public void ToastContent(string msg)
    {
        //if (msg.Contains("Coins Added Successfully"))
        //{
        //    string[] msgs = msg.Split(null);
        //    PluginManager.Instance.toastPopUp.GetComponentInChildren<Text>().text = msgs[0] + "Coins Added Successfully";// + LanguageHandler.GetCurrentLanguageText("Coins Added Successfully");
        //}
        //else if (msg.Contains("Keys Added Successfully"))
        //{
        //    string[] msgs = msg.Split(null);
        //    PluginManager.Instance.toastPopUp.GetComponentInChildren<Text>().text = msgs[0] + "Keys Added Successfully";// + LanguageHandler.GetCurrentLanguageText("Keys Added Successfully");
        //}
        //else if (msg.Contains("Hearts Added Successfully"))
        //{
        //    string[] msgs = msg.Split(null);
        //    PluginManager.Instance.toastPopUp.GetComponentInChildren<Text>().text = msgs[0] + "Hearts Added Successfully";// + LanguageHandler.GetCurrentLanguageText("Hearts Added Successfully");
        //}
        //else if (msg.Contains("Crowns Added Successfully"))
        //{
        //    string[] msgs = msg.Split(null);
        //    PluginManager.Instance.toastPopUp.GetComponentInChildren<Text>().text = msgs[0] + "Hearts Added Successfully";// + LanguageHandler.GetCurrentLanguageText("Hearts Added Successfully");
        //}
        //else if (msg.Contains("Companions Added Successfully"))
        //{
        //    string[] msgs = msg.Split(null);
        //    PluginManager.Instance.toastPopUp.GetComponentInChildren<Text>().text = msgs[0] + "Companions Added Successfully";// + LanguageHandler.GetCurrentLanguageText("Companions Added Successfully");
        //}
        //else if (msg.Contains("MagicSticks Added Successfully"))
        //{
        //    string[] msgs = msg.Split(null);
        //    PluginManager.Instance.toastPopUp.GetComponentInChildren<Text>().text = msgs[0] + "MagicSticks Added Successfully";// + LanguageHandler.GetCurrentLanguageText("MagicSticks Added Successfully");
        //}
        //else
        //{
        PluginManager.Instance.toastPopUp.GetComponentInChildren<Text>().text = "" + msg;// + LanguageHandler.GetCurrentLanguageText(msg);
                                                                                         //}
    }



    public void ShowExitPage()
    {

    }

}



public enum RewardType_BigCode
{
    Revive,
    DoubleReward,
    Coins,
    Unlock,
    WatchToResume,
    CoinsInGame
}
