using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using GameAnalyticsSDK;

public class MenuAdHandler_BigCode : MonoBehaviour {

    public static MenuAdHandler_BigCode Instance;

    public int PresentAdIndex = 0;

    public int TotalAdsCount;

    public Transform MenuAd;

    private Texture2D menuAdTexture;

    public Image menuAd;

    public static bool isAdLoaded;


    // Use this for initialization
    void Start ()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
            LoadMenuAd();
        else
            LoadGameScene();
    }


    private void Awake()
    {
        Instance = this; 
    }

    CallMethod callMethod ;

    private void OnEnable()
    {
        //if ((callMethod == null|| BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count == 0) || (BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count > 0 && !BigCodeLibHandler_BigCode.Instance.StackManager.pages[BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count - 1].Equals(callMethod)))
        //{
        //    //Back Button Reference
        //    callMethod = new CallMethod();
        //    callMethod.fun = MenuAdHandler_BigCode.Instance.Close;
        //    callMethod.MethodType = MethodType.NONPARAMETERIZED;
        //    //callMethod.parameter = "BTN_BACK";
        //    BigCodeLibHandler_BigCode.Instance.StackManager.pages.Add(callMethod);
        //}


    }

    void LoadAd(int AdIndex)
    {
        if (PresentAdIndex < TotalAdsCount)
        {
            switch (ServerDataHandler.Instance.BaseData.MenuAds[PresentAdIndex])
            {
                case ADS.BIGCODE:

                    StartCoroutine(LoadBigCodeAd());
                    isAdLoaded = true;

                    break;
                case ADS.ADMOB:


                    if (SceneManager.GetActiveScene().buildIndex == 1)
                    {


#if UNITY_EDITOR
                            MenuAdHandler_BigCode.Instance.LoadGameScene();
#endif

#if !UNITY_EDITOR
                            //if (AdmobHandler_BigCode.Instance.interstitial.IsLoaded())
                            //{
                            //    AdmobHandler_BigCode.Instance.ShowInterstialAd();
                            //    isAdLoaded = true;
                            //}
                            //else
                            {
                        
                                PresentAdIndex++;
                                LoadAd(PresentAdIndex);
                            }
#endif

                    }



                        break;
            }
        }

    }




    public void LoadMenuAd()
    {
        if (ServerDataHandler.Instance.CommonData != null && !ServerDataHandler.Instance.CommonData.MenuAd.excludeMenuAds.Contains(Application.identifier) && !isAdLoaded)
        {

            TotalAdsCount = ServerDataHandler.Instance.BaseData.MenuAds.Count;

            LoadAd(PresentAdIndex);
        }
        else
        {


#if !UNITY_EDITOR
       
        //GooglePlayGamesHandler.Instance.Initialize();
         GameDataHandler.Instance.NoInternetLoadGameData();
         MenuAdHandler_BigCode.Instance.LoadGameScene();
#endif





#if UNITY_EDITOR

            LoadGameScene();
#endif
        }
    }


    IEnumerator LoadBigCodeAd()
    {
        string filePath = Application.persistentDataPath + "/" + GameConstants_BigCode.BigCodeAd + ".jpg";

        if (File.Exists(filePath) && !BigCodeLibHandler_BigCode.Instance.IsGameAlreadyInstalled(ServerDataHandler.Instance.CommonData.MenuAd.BigCodeAd.PackageName))
        {
            if (PluginManager.Instance.isPotraitGame)
            {
                menuAdTexture = new Texture2D(338, 600, TextureFormat.ARGB32, false);
            }
            else
            {
                menuAdTexture = new Texture2D(600, 372, TextureFormat.ARGB32, false);
            }

            menuAdTexture.LoadImage(File.ReadAllBytes(filePath));

            menuAd.sprite = Sprite.Create(menuAdTexture, new Rect(0, 0, menuAdTexture.width, menuAdTexture.height), Vector2.zero);

            MenuAd.gameObject.SetActive(true);
        }
        else
        {
            // Intialize Google Play Games MainThread
            MainThread.Call(GooglePlayGamesMainThread);

#if UNITY_EDITOR
            LoadGameScene();
#endif
        }

        yield return null;
    }


    public void GooglePlayGamesMainThread()
    {
#if !UNITY_EDITOR

        //GooglePlayGamesHandler.Instance.Initialize();

        GameDataHandler.Instance.NoInternetLoadGameData();
        MenuAdHandler_BigCode.Instance.LoadGameScene();
#endif
    }


    void DisableMenuAd()
    {
        MenuAd.gameObject.SetActive(false);
    }

    public void Close()
    {
#if !UNITY_EDITOR

        //GooglePlayGamesHandler.Instance.Initialize();

         GameDataHandler.Instance.NoInternetLoadGameData();
         MenuAdHandler_BigCode.Instance.LoadGameScene();
#endif
        DisableMenuAd();

#if UNITY_EDITOR
        LoadGameScene();
#endif

    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            if (SceneManager.GetActiveScene().name.Equals("BaseScene"))
            {
                if(MenuAd.gameObject.activeSelf)
                Close();
            }
        }
    }


    public void OnAdClick()
    {
        Application.OpenURL("market://details?id="+ServerDataHandler.Instance.CommonData.MenuAd.BigCodeAd.PackageName);
    }


    public void ShowInterstitial()
    {
        PluginManager.Instance.ShowLevelCompletedInterstitialAd();

    }


    public void ShowRewardedVideo()
    {
        PluginManager.Instance.ShowRewardedVideoAd(RewardType_BigCode.LevelUpTripleReward);


    }

    public void RequestInterstitial()
    {
        PluginManager.Instance.RequestInterstitial();

    }

    public void Purchase()
    {
        //AdmobHandler.Instance.HideBannerView();

        //Product product = InappPurchaseHandler.Instance.m_StoreController.products.WithID("com.integerfreegames.hilltopbikeracing.unlockallbikes");
        //InappPurchaseHandler.Instance.m_StoreController.InitiatePurchase(product);

        BigCodeLibHandler_BigCode.Instance.NativeSharing(NativeShare.COMMON);
    }

    public void LoadGameScene()
    {

        if (PluginManager.Instance.isInternetAvailable)
        {
            if (ServerDataHandler.Instance.CommonData.MiniMoreGames.version != GameDataHandler.Instance.GameData.miniMoregamesVersion)
                 BigCodeAdHandler_BigCode.Instance.StartCoroutine(BigCodeAdHandler_BigCode.Instance.DownloadMiniMoreGames());
        }

        if (InappPurchaseHandler.Instance)
            InappPurchaseHandler.Instance.Initialize();


        //BridgeManager_Bigcode.Instance.RequestToUnlockAllTrains();


        //if (!LoginHandler.Instance.isAuthenticated())
        //    SceneManager.LoadScene("LoginScene");
        //else
            SceneManager.LoadScene(PluginManager.Instance.NextScene);
    }
}
