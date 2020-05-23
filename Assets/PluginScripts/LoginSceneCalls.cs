using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSceneCalls : MonoBehaviour {

    public GameObject Loading;

    public Toggle privacyPolicyCheckbox,userAgreementCheckbox;




    private void OnEnable()
    {
        LoginHandler.Authenticated += OnAuthenticated;
        LoginHandler.AuthenticateFailed += OnAuthentionFailed;




    }


    private void OnDisable()
    {
        LoginHandler.Authenticated -= OnAuthenticated;
        LoginHandler.AuthenticateFailed -= OnAuthentionFailed;
    }

    public void Guest()
    {

        if (CheckAgreements())
            LoginHandler.Instance.Login(LoginHandler.LoginType.GUEST);

    }

    public void Twitter()
    {
        if (CheckAgreements())
            LoginHandler.Instance.Login(LoginHandler.LoginType.TWITTER);
    }


    public void Facebook()
    {
        if (CheckAgreements())
            LoginHandler.Instance.Login(LoginHandler.LoginType.FACEBOOK);
    }


    public void PrivacyPolicy()
    {
        Application.OpenURL("https://mtsfreegames.com/privacypolicy.html");
    }


    public void UserAgreements()
    {
        Application.OpenURL("https://mtsfreegames.com/userlicenseagreement-mtsfreegames.html");
    }


    public bool CheckAgreements()
    {
        if (!privacyPolicyCheckbox.isOn)
        {
            PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Please Agree Privacy Policy"));
            return false;
        }

        if (!userAgreementCheckbox.isOn)
        {
            PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Please Agree User Agreement"));
            return false;
        }


        return true;
    }



    public void OnAuthenticated()
    {
        //Load Next Scene

        Loading.SetActive(true);
        SceneManager.LoadScene(PluginManager.Instance.NextScene);
    }

    public void OnAuthentionFailed()
    {
        PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Authentication Failed"));
        SceneManager.LoadScene(PluginManager.Instance.NextScene);

    }



    public LanguageSelection LanguageSelection;
    public void OnLanguageSelectionClick()
    {
        LanguageSelection.gameObject.SetActive(true);
    }

}
