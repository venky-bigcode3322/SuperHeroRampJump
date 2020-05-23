using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Facebook.Unity;
using System;

public class FacebookHandler_BigCode : MonoBehaviour {

    public static FacebookHandler_BigCode Instance;

    public delegate void OnEventFacebookAuthenticated(LoginHandler.LoginType loginType);
    public delegate void OnEventFacebookAuthenticateFailed();
    public delegate void OnEventFacebookShare();
    public delegate void OnEventFacebookLoadPicture(Sprite image);
    public delegate void OnEventFacebookLoadName(string name);


    public static event OnEventFacebookAuthenticated FacebookAuthenticated;
    public static event OnEventFacebookAuthenticateFailed FacebookAuthenticateFailed;
    public static event OnEventFacebookShare FacebookShare;
    public static event OnEventFacebookLoadPicture FacebookLoadPicture;
    public static event OnEventFacebookLoadName FacebookLoadName;



    [HideInInspector]
    public bool isInitialized;
    
    private void Awake()
    {
        Instance = this;
    }


    public void Initialize()
    {
        //if (FB.IsInitialized)
        //{
        //    FB.ActivateApp();

        //    isInitialized = true;
        //    //PluginManager.Instance.OnPluginInitializationCompleted();
        //}
        //else
        //{
        //    FB.Init(() =>
        //    {
        //        FB.ActivateApp();

        //        //PluginManager.Instance.ShowToast("Facebook Init Success");

        //        isInitialized = true;
        //        //PluginManager.Instance.OnPluginInitializationCompleted();
        //    });
        //}
    }


    public void Login()
    {
        //if(!BigCodeLibHandler_BigCode.Instance.IsGameAlreadyInstalled("com.facebook.katana"))
        //    FB.Mobile.ShareDialogMode = ShareDialogMode.WEB;

        //if (PluginManager.Instance.isInternetAvailable)
        //{

        //    if (!FB.IsLoggedIn)
        //    {
        //        FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, AuthCallback);
        //    }
        //    else
        //    {
        //        AuthCallback(null);
        //    }
        //}
        //else
        //{
        //    PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Internet Not Available"));
        //}

    }


    //private void AuthCallback(ILoginResult result)
    //{

    //    if (FB.IsLoggedIn)
    //    {
    //        //Callback For Facebook Authenticated
    //        if (FacebookAuthenticated != null)
    //            FacebookAuthenticated(LoginHandler.LoginType.FACEBOOK);

    //        // To Load Profile Picture
    //        if (PluginManager.Instance.isInternetAvailable)
    //            LoadProfilePicture();

    //        // To Load Profile Name
    //        if (PluginManager.Instance.isInternetAvailable)
    //            LoadName();

    //        //Callback For Share
    //        if (FacebookShare != null)
    //            FacebookShare();
    //    }
    //    else
    //    {
    //        //Callback For Facebook Authentication Failed
    //        if (FacebookAuthenticateFailed != null)
    //            FacebookAuthenticateFailed();
    //    }

    //}



    private void LoadName()
    {
        //FB.API("me?fields=name", HttpMethod.GET, NameCallBack);
    }

    [HideInInspector]
    public string userName;

    //public void NameCallBack(IGraphResult result)
    //{
    //    IDictionary dict = Facebook.MiniJSON.Json.Deserialize(result.RawResult) as IDictionary;
    //    userName = dict["name"].ToString();


    //    if (FacebookLoadName != null)
    //        FacebookLoadName(userName);
    //}

    public void LogEvent(string page)
    {
        //if (FB.IsInitialized )
        //{
        //    Dictionary<string, object> customParametres = new Dictionary<string, object>();
        //    FB.LogAppEvent(page.ToString(), 1, customParametres);
        //}

    }

    public void LogEvent(string page,int levelNo)
    {
        //if (FB.IsInitialized )
        //{
        //    Dictionary<string, object> customParametres = new Dictionary<string, object>();
        //    customParametres.Add(page.ToString(), levelNo);

        //    FB.LogAppEvent(page.ToString(), 1, customParametres);
        //}

    }


    [HideInInspector]
    public SignIn signIn = SignIn.NONE;

    public enum SignIn
    {
        NONE,
        Authenticattion,
        SHARE,
        SHAREPOPUP
    }


    public delegate void PictureCallback(Texture2D texture);


    

    public void LoadProfilePicture()
    {
        //string query = "/me/picture?g&width=128&height=128&redirect=false";
        //FB.API(query, HttpMethod.GET, result => {
        //    if (result.Error != null)
        //    {
        //        Debug.LogError(result.Error);
        //        return;
        //    }

        //    string pictureUrl = DeserializePictureUrl(result.RawResult);
        //    StartCoroutine(DownloadPicture(pictureUrl, MyPictureCallback));
        //});
    }

    public string DeserializePictureUrl(string result)
    {
        //var pictureUrlObj = Facebook.MiniJSON.Json.Deserialize(result);
        //var pictureData = (Dictionary<string, object>)(((Dictionary<string, object>)pictureUrlObj)["data"]);
        //object pictureUrl = null;
        //if (pictureData.TryGetValue("url", out pictureUrl))
        //{
        //    return (string)pictureUrl;
        //}
        return string.Empty;
    }

    IEnumerator DownloadPicture(string url, PictureCallback callback)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            callback(www.texture);
        }
    }

    public void MyPictureCallback(Texture2D texture)
    {


        Sprite image = ConvertToSprite(texture);

        if (FacebookLoadPicture != null)
            FacebookLoadPicture(image);

       

    }

    public Sprite ConvertToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }



    public void FacebookshareMainThread()
    {
        //if (ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData != null)
        //    FB.ShareLink(new Uri(ServerDataHandler.Instance.BaseData.Share.FacebookShare), callback: ShareCallback);

        //FB.FeedShare(link: new Uri(ServerDataHandler.Instance.BaseData.Share.FacebookShare), callback: ShareCallback);

       
    }

    //private void ShareCallback(IShareResult result)
    //{

    //    //PluginManager.Instance.ShowToast(result.Error);

    //    if (result.Error == null && !result.Cancelled)
    //    {
    //        BridgeManager_Bigcode.Instance.OnFacebookShare(signIn);
    //    }
    //    else
    //    {
    //        Debug.LogError(result.Error);
    //    }
    //}

    public void SignOut()
    {
        //if (FB.IsLoggedIn)
        //{
        //    FB.LogOut();
        //}
    }



    public void TrackPurchase(float currencyAmount, string CurrencyCode, string purchaseId, string purchaseType)
    {

        //Dictionary<string, object> parameters = new Dictionary<string, object>();
        //parameters.Add("purchaseId", purchaseId);
        //parameters.Add("purchaseType", purchaseType);

        //FB.LogPurchase(currencyAmount, CurrencyCode, parameters);
    }
}
