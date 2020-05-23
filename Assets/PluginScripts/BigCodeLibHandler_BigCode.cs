using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class BigCodeLibHandler_BigCode : MonoBehaviour {

    public static BigCodeLibHandler_BigCode Instance;

    public AndroidJavaObject nativeFunctionalities;
        
    private void Awake()
    {
        Instance = this;

    }


    
    
    void Start()
    {
#if !UNITY_EDITOR && UNITY_ANDROID

        nativeFunctionalities = new AndroidJavaObject("com.bigcode.nativefunctionalities.MainActivity");
        nativeFunctionalities.Call("Initilize", "PluginManager");


        CreateNotificationChannel("channel name", "channel_description", "channel_id");
        LocalNotificationHandler_BigCode.Instance.CancelNotifications();
#endif


    }

   
    public bool IsGameAlreadyInstalled(string packageName)
    {
#if UNITY_EDITOR
        return false;
#endif

#if !UNITY_EDITOR && UNITY_ANDROID
        return nativeFunctionalities.Call<bool>("isAppInstalled",packageName);
#endif
    }

    public void ShowToast(string msg)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.Call("showMessage", msg); 
#endif

    }


    public void ShowLoading()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.Call("showLoading", "Loading", "Wait");
#endif
    }

    public void DismissLoading()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.Call("dismissLoading");
#endif
    }

    public void NativeSharing(NativeShare nativeShare)
    {

#if !UNITY_EDITOR && UNITY_ANDROID
        if (PluginManager.Instance && PluginManager.Instance.isInternetAvailable)
        {
            string gameName = Application.productName;
            string description = null;

            switch (nativeShare)
            {
                case NativeShare.WHATSAPP:

                    description = ServerDataHandler.Instance.BaseData.Share.WhatsAppShare;
                    nativeFunctionalities.Call("nativeShare", "com.whatsapp", description);

                    break;
                case NativeShare.FACEBOOK:

                    description = ServerDataHandler.Instance.BaseData.Share.FacebookShare;
                    nativeFunctionalities.Call("nativeShare", "com.facebook.katana", description);

                    break;
                case NativeShare.TWITTER:

                    description = ServerDataHandler.Instance.BaseData.Share.TwitterShare;
                    nativeFunctionalities.Call("nativeShare", "com.twitter.android", description);

                    break;
                case NativeShare.COMMON:

                    description = "https://play.google.com/store/apps/details?id="+Application.identifier;
                    nativeFunctionalities.Call("nativeIntentShare", description);

                    break;
                case NativeShare.INSTAGRAM:

                    description = ServerDataHandler.Instance.BaseData.Share.FacebookShare;
                    nativeFunctionalities.Call("nativeShare", "com.instagram.android", description);

                    break;
            }


        }
        else
        {
            BigCodeLibHandler_BigCode.Instance.ShowToast("Internet Not Available");
        }
#endif
    }

    public void OnShareAppNotFound(string pkg)
    {
        switch (pkg)
        {
            case "com.facebook.katana":

                ShowToast("Facebook App Not Installed");

                break;

            case "com.whatsapp":

                ShowToast("WhatsApp  Not Installed");

                break;

            case "com.twitter.android":

                ShowToast("Twitter App Not Installed");

                break;

            case "com.instagram.android":

                ShowToast("Instagram App Not Installed");

                break;
        }
    }

    public void LoadWebView()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        if (PluginManager.Instance.isPotraitGame)
            nativeFunctionalities.Call("showWebView", ServerDataHandler.Instance.CommonData.ExitPage.Portrait_Link, "PluginManager");
        else
            nativeFunctionalities.Call("showWebView", ServerDataHandler.Instance.CommonData.ExitPage.Landscape_Link, "PluginManager");
#endif
    }

    bool isWebViewShown;
    public void HideWebView()
    {
#if !UNITY_EDITOR && UNITY_ANDROID

        if (PluginManager.Instance.isInternetAvailable)
        {
            isWebViewShown = false;
            nativeFunctionalities.Call("hideWebView");
        }
#endif
    }



    public void ShowWebView()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        if (!SceneManager.GetActiveScene().name.Equals("SampleScene") && !SceneManager.GetActiveScene().name.Equals("BaseScene"))
        {
            if (PluginManager.Instance.isInternetAvailable)
            {
                isWebViewShown = true;
                nativeFunctionalities.Call("showWebView");
            }
        }
#endif
            
    }


    public const string BACKBUTONCODE = "BTN_BACK";

    public StackManager StackManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButton();
        }
    }


    public void OnBackButton()
    {
        if (StackManager && StackManager.pages.Count > 0)
        {

            CallMethod callMethod = StackManager.pages[StackManager.pages.Count - 1];


            switch (callMethod.MethodType)
            {
                case MethodType.NONPARAMETERIZED:

                    callMethod.fun();


                    break;
                case MethodType.PARAMETERIZED:

                    callMethod.function(callMethod.parameter);

                    break;
                case MethodType.GAMEOBJECTPARAM:

                    GameObject backButton = new GameObject();
                    backButton.name = BACKBUTONCODE;

                    callMethod.functionGameObject(backButton);


                    break;
            }
            

        }
        else if (PluginManager.Instance.isInternetAvailable && ServerDataHandler.Instance)
        {

            switch (ServerDataHandler.Instance.BaseData.exitPageAdType)
            {
                case 0:

                    ShowAlertDialog("Do you want to exit?", "Yes", "No", NativeDialogType.EXITPAGE);
                    break;
                case 1:

                    if (isWebViewShown)
                        HideWebView();
                    else
                        ShowWebView();
                    break;
                case 2:

                    PluginManager.Instance.ExitPage();

                    break;
            }

            
        }
        else
        {
            ShowAlertDialog("Do you want to exit?", "Yes", "No", NativeDialogType.EXITPAGE);

        }
    }


    public void WebViewCallback(string action)
    {
       

        switch (action)
        {
            case "close":

                GameDataHandler.Instance.SetLocalData(GameConstants_BigCode.LocalGameDataPath, GameDataHandler.Instance.GameData);
                Application.Quit();

                break;
            case "no":

                HideWebView();

                break;
            case "rateit":

                PluginManager.Instance.RateUS();

                break;
            default:
                Application.OpenURL("market://details?id=" + action);


                break;
        }

    }
    
    [HideInInspector]
    public NativeDialogType nativeDialogType = NativeDialogType.NONE;
    public void ShowAlertDialog(string msg, string YesNameButton, string NoNameButton,NativeDialogType nativeDialogType)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        this.nativeDialogType = nativeDialogType;
        nativeFunctionalities.Call("showAlertDialog", Application.productName, msg, YesNameButton, NoNameButton, false);
#endif
    }

    public void ShowAlertPromoCode(NativeDialogType nativeDialogType)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        this.nativeDialogType = nativeDialogType;
        nativeFunctionalities.Call("showAlertDialog", Application.productName, "Enter PromoCode", "Apply", "Cancel", true);
#endif
    }

    public void ShowAlertPromoCode(string msg, string YesNameButton, string NoNameButton, NativeDialogType nativeDialogType)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        this.nativeDialogType = nativeDialogType;
        nativeFunctionalities.Call("showAlertDialog", Application.productName, msg, YesNameButton, NoNameButton, true);
#endif
    }

    private void OnApplicationQuit()
    {
        if(GameDataHandler.Instance)
        GameDataHandler.Instance.SetLocalData(GameConstants_BigCode.LocalGameDataPath, GameDataHandler.Instance.GameData);
    }

    public void PromoCallback(string promocode)
    {
        if (PluginManager.Instance.isInternetAvailable)
        {
            if (promocode.ToLower().Equals(ServerDataHandler.Instance.BaseData.Promo.PromoCode.ToLower()))
            {
                if (!GameDataHandler.Instance.GameData.isPromoCodeApplied)
                {
                    PluginManager.Instance.OnPromoCodeSuccess(ServerDataHandler.Instance.BaseData.Promo.PromoCoins);
                    GameDataHandler.Instance.GameData.isPromoCodeApplied = true;

                    BigCodeLibHandler_BigCode.Instance.ShowToast("Promo Code Applied Successfully..!");
                }
                else
                {
                    BigCodeLibHandler_BigCode.Instance.ShowToast("Promo Code is valid for one time..!");
                }
            }
            else
            {
                BigCodeLibHandler_BigCode.Instance.ShowToast("Invalid PromoCode");
            }
        }
        else
        {
            BigCodeLibHandler_BigCode.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Internet Not Available"));
        }

    }

    public void AlertCallback(string eventName)
    {
        switch (nativeDialogType)
        {
            case NativeDialogType.EXITPAGE:

                if (eventName.Equals("yes"))
                {
                    Application.Quit();
                }

                break;
            case NativeDialogType.PROMOPAGE:

                break;
            case NativeDialogType.RATEPOPUP:

                if (eventName.Equals("yes"))
                {
                    if (PluginManager.Instance)
                    {
                        PluginManager.Instance.RateUS();

                        if (GameDataHandler.Instance && GameDataHandler.Instance.GameData != null)
                            GameDataHandler.Instance.GameData.isRated = true;


                    }
                       

                }

                break;
            case NativeDialogType.SHAREPOPUP:

                if (eventName.Equals("yes"))
                {
                    if (PluginManager.Instance)
                        PluginManager.Instance.NativeShare(NativeShare.COMMON);
                }
                break;

            case NativeDialogType.UPDATEPOPUP:

                if (eventName.Equals("yes"))
                {
                    PluginManager.Instance.RateUS();
                }
                else if (eventName.Equals("no"))
                {
                    nativeDialogType = NativeDialogType.NONE;
                    PluginManager.Instance.OnUpdateVersionCancel();
                }

                break;
        }
    }

    public void HideIcon(int HideDelayTime,int UnHideDelaytime)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.Call("hideIcon", HideDelayTime, UnHideDelaytime);
#endif
    }

    public void UnHideIcon()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        nativeFunctionalities.Call("unHideIcon");
#endif
    }


   

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //HideIcon(10, 40);
        }
        else
        {
            if (nativeDialogType == NativeDialogType.UPDATEPOPUP)
            {
                if (PluginManager.Instance)
                    PluginManager.Instance.OnUpdateVersionCancel();

                nativeDialogType = NativeDialogType.NONE;
            }
        }
    }





    public void SetLocalNotification(int id, long timeInMilliSeconds, string message)
    {
#if !UNITY_EDITOR && UNITY_ANDROID && !UNITY_STANDALONE_WIN
                    


#if UNITY_5_6_OR_NEWER
        nativeFunctionalities.Call("SetNotification",id,timeInMilliSeconds,Application.productName,message,"ic_onesignal_large_icon_default","ic_stat_onesignal_default",Application.identifier,"channel_id");
#else
        nativeFunctionalities.Call("SetNotification", id, timeInMilliSeconds, Application.productName, message,"ic_onesignal_large_icon_default","ic_stat_onesignal_default", Application.bundleIdentifier,"channel_id");
#endif
#endif
    }

    public void CancelNotification(int id)
    {
#if !UNITY_EDITOR && UNITY_ANDROID && !UNITY_STANDALONE_WIN
        nativeFunctionalities.Call("CancelNotification", id);
#endif
    }

    public void CreateNotificationChannel(string channel_name, string channel_description, string channel_id)
    {
#if !UNITY_EDITOR && UNITY_ANDROID && !UNITY_STANDALONE_WIN
        nativeFunctionalities.Call("createNotificationChannel",channel_name,channel_description,channel_id);
#endif
    }

    public string GetArchitecture()
    {
#if !UNITY_EDITOR
        using (var system = new AndroidJavaClass("java.lang.System"))
        {
            string arch = system.CallStatic<string>("getProperty", "os.arch");
            return arch.Substring(0, 3).ToUpper();
        }
#endif
#if UNITY_EDITOR

        return "NONE";

#endif
    }


}

public enum NativeShare
{
    FACEBOOK,
    TWITTER,
    WHATSAPP,
    INSTAGRAM,
    COMMON
}

public enum NativeDialogType
{
    EXITPAGE,
    PROMOPAGE,
    SHAREPOPUP,
    RATEPOPUP,
    UPDATEPOPUP,
    WATCHVIDEO,
    NONE
}
