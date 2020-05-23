using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginHandler : MonoBehaviour {

    public static LoginHandler Instance;

    public delegate void OnEventAuthenticated();
    public delegate void OnEventAuthenticateFailed();
    public delegate void OnEventShare();
    public delegate void OnEventLoadPicture(Sprite image);
    public delegate void OnEventLoadName(string name);




    public static event OnEventAuthenticated Authenticated;
    public static event OnEventAuthenticateFailed AuthenticateFailed;
    public static event OnEventShare Share;
    public static event OnEventLoadPicture LoadPicture;
    public static event OnEventLoadName LoadName;

    private void Awake()
    {
        Instance = this;
    }


    private void OnEnable()
    {
        //Enable Facebook Events
        FacebookHandler_BigCode.FacebookAuthenticated += OnFacebookAuthenticated;
        FacebookHandler_BigCode.FacebookAuthenticateFailed += OnFacebookAuthenticationFailed;
        FacebookHandler_BigCode.FacebookLoadPicture += OnFacebookLoadPicture;
        FacebookHandler_BigCode.FacebookLoadName += OnFacebookLoadName;
        FacebookHandler_BigCode.FacebookShare += FacebookShare;

        //Enable Twitter Events
        //TwitterHandler.TwitterAuthenticated += OnTwitterAuthenticated;
        //TwitterHandler.TwitterAuthenticateFailed += OnTwitterAuthenticationFailed;
        //TwitterHandler.TwitterLoadPicture += OnFacebookLoadPicture;
        //TwitterHandler.TwitterLoadName += OnTwitterLoadName;
        //TwitterHandler.TwitterShare += FacebookShare;


    }

    private void OnDisable()
    {
        //Disable Facebook Events
        FacebookHandler_BigCode.FacebookAuthenticated -= OnFacebookAuthenticated;
        FacebookHandler_BigCode.FacebookAuthenticateFailed -= OnFacebookAuthenticationFailed;
        FacebookHandler_BigCode.FacebookLoadPicture -= OnFacebookLoadPicture;
        FacebookHandler_BigCode.FacebookLoadName -= OnFacebookLoadName;
        FacebookHandler_BigCode.FacebookShare -= FacebookShare;


        //Enable Twitter Events
        //TwitterHandler.TwitterAuthenticated -= OnTwitterAuthenticated;
        //TwitterHandler.TwitterAuthenticateFailed -= OnTwitterAuthenticationFailed;
        //TwitterHandler.TwitterLoadPicture -= OnTwitterLoadPicture;
        //TwitterHandler.TwitterLoadName -= OnTwitterLoadName;
        //TwitterHandler.TwitterShare -= TwitterShare;

    }

    public enum LoginType
    {
        GUEST,
        FACEBOOK,
        TWITTER
    }

    [HideInInspector]
    public LoginType loginType;

    public void Login(LoginType loginType)
    {

        this.loginType = loginType;

        switch (loginType)
        {
            case LoginType.GUEST:

                //PlayerPrefs.SetInt("login_page_authetication_type", (int)LoginType.GUEST);

                PlayerPrefs.GetString("player_name", "Guest");


                if (Authenticated != null)
                    Authenticated();

                if (LoadName != null)
                    LoadName("Guest");

                if (LoadPicture != null)
                    LoadPicture(null);


                break;
            case LoginType.FACEBOOK:

                if (PluginManager.Instance.isInternetAvailable)
                    FacebookHandler_BigCode.Instance.Login();
                else
                    PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Network Not Available"));


                break;
            case LoginType.TWITTER:

                //if (PluginManager.Instance.isInternetAvailable)
                //    TwitterHandler.Instance.Login();
                //else
                //    PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Network Not Available"));

                break;
        }
        
    }



    public LoginType getAuthenticatedType()
    {
        
        int type = PlayerPrefs.GetInt("login_page_authetication_type",0);

        Debug.LogError(type);

        return (LoginType)type;
    }

    public bool isAuthenticated()
    {
        if (PlayerPrefs.HasKey("login_page_authetication_type"))
        {
            int type = PlayerPrefs.GetInt("login_page_authetication_type", 0);

            return true;
        }
        else
        {
            return false;
        }
    }

    public void SignOut()
    {

        switch (getAuthenticatedType())
        {
            case LoginType.FACEBOOK:

                FacebookHandler_BigCode.Instance.SignOut();

                break;
            case LoginType.TWITTER:

                //TwitterHandler.Instance.SignOut();

                break;
            case LoginType.GUEST:

                break;

        }

        PlayerPrefs.DeleteKey("login_page_authetication_type");

        PlayerPrefs.DeleteKey("player_name");

        Debug.LogError("Sign OUt Successfully");

    }















    //Facebook CallBacks
    private void OnFacebookAuthenticated(LoginType loginType)
    {
        

        PlayerPrefs.SetInt("login_page_authetication_type", (int)LoginType.FACEBOOK);

        if (Authenticated != null)
            Authenticated();


    }
    private void OnFacebookAuthenticationFailed()
    {
        if (AuthenticateFailed != null)
            AuthenticateFailed();

    }

    private void OnFacebookLoadName(string name)
    {
        PlayerPrefs.GetString("player_name", FacebookHandler_BigCode.Instance.userName);

        if (LoadName != null)
            LoadName(name);

    }
    private void OnFacebookLoadPicture(Sprite image)
    {
        if (LoadPicture != null)
            LoadPicture(image);
    }
    private void FacebookShare()
    {
        if (Share != null)
            Share();
    }














    //Twitter CallBacks
    private void OnTwitterAuthenticated(LoginType loginType)
    {

        PlayerPrefs.SetInt("login_page_authetication_type", (int)LoginType.TWITTER);

        if (Authenticated != null)
            Authenticated();


    }
    private void OnTwitterAuthenticationFailed()
    {
        if (AuthenticateFailed != null)
            AuthenticateFailed();

    }

    private void OnTwitterLoadName(string name)
    {
        PlayerPrefs.GetString("player_name", FacebookHandler_BigCode.Instance.userName);

        if (LoadName != null)
            LoadName(name);

    }
    private void OnTwitterLoadPicture(Sprite image)
    {
        if (LoadPicture != null)
            LoadPicture(image);
    }
    private void twitterShare()
    {
        if (Share != null)
            Share();
    }


}
