using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatePopUp_Bigcode : MonoBehaviour {

    public int selectedStars;

    public List<Transform> Stars;

    public static RatePopUp_Bigcode Instance;

    public GameObject RateButton, ThankYouButton, LaterButton;


    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        
	}

    private void OnEnable()
    {

        if (ServerDataHandler.Instance && ServerDataHandler.Instance.BaseData.RatePopUp.isNotNowButton)
        {
            LaterButton.SetActive(true);
        }
        else
        {
            LaterButton.SetActive(false);
        }

        // Disable All Stars
        foreach (Transform star in RatePopUp_Bigcode.Instance.Stars)
        {
            star.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        selectedStars = 0;
    }

    
    public void EnableThankYou ()
    {
        RateButton.SetActive(false);
        ThankYouButton.SetActive(true);
	}

    public void EnableRate()
    {
        ThankYouButton.SetActive(false);
        RateButton.SetActive(true);
    }

    public void RateButtonClicked(GameObject obj)
    {
        obj.SetActive(false);

        GameDataHandler.Instance.GameData.isRated = true;



        PluginManager.Instance.SetReachedPage("New_RatePop_Rate_Button_Clicked");

        PluginManager.Instance.RateUS();
    }

    public void ThankYouButtonClicked(GameObject obj)
    {
        obj.SetActive(false);

        GameDataHandler.Instance.GameData.isRated = true;




        PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Thank You for Your Feedback"));

        PluginManager.Instance.SetReachedPage("New_RatePop_Thankyou_Button_Clicked");
    }


    public void CloseButton(GameObject obj)
    {
        obj.SetActive(false);



        PluginManager.Instance.SetReachedPage("New_RatePop_later_Button_Clicked");
    }

}
