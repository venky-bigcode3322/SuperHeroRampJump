using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubscriptionHandler : MonoBehaviour {


    public static SubscriptionHandler Instance;
    public List<GameObject> subscriptions;
    public Sprite NormalSprite, SelectedSprite;
    private int SelectedIndex;

    public GameObject SubscriptionPage;

    public GameObject VIPButton;

    // Use this for initialization
    void Awake() {

        Instance = this;

    }

    public List<Text> PriceTags;
    public void LoadPrices()
    {
        PluginManager.Instance.SetProductText(9, PriceTags[0], PluginManager.InAppPriceType.Normal);
        PluginManager.Instance.SetProductText(10, PriceTags[1], PluginManager.InAppPriceType.Normal);
        PluginManager.Instance.SetProductText(11, PriceTags[2], PluginManager.InAppPriceType.Normal);
    }


    public void LoadSubscription()
    {
        //New Subscription UI
        if(PriceTags != null && PriceTags[0] != null)
        LoadPrices();

        //Back Button reference
        //CallMethod callMethod = new CallMethod();
        //callMethod.fun = Cancel;
        //callMethod.MethodType = MethodType.NONPARAMETERIZED;
        //BigCodeLibHandler_BigCode.Instance.StackManager.pages.Add(callMethod);

        //Commnted due to old ui not used for subscription
        //subscriptions[0].transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "FREE For 7 days " + PlayerPrefs.GetString(InappPurchaseHandler.Instance.InAppProducts[10], "BUY") + " per Month, autorenewable";
        //subscriptions[1].transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "FREE For 3 days " + PlayerPrefs.GetString(InappPurchaseHandler.Instance.InAppProducts[9], "BUY") + " per Week, autorenewable";
        //subscriptions[2].transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "FREE For 30 days " + PlayerPrefs.GetString(InappPurchaseHandler.Instance.InAppProducts[11], "BUY") + " per Year, autorenewable";

        //subscriptions[0].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Monthly";
        //subscriptions[1].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Weekly";
        //subscriptions[2].transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Yearly";

        //PluginManager.Instance.SetProductText(10, subscriptions[0].transform.GetChild(0).GetChild(0).GetComponent<Text>(), PluginManager.InAppPriceType.Subscription);
        //PluginManager.Instance.SetProductText(9, subscriptions[1].transform.GetChild(0).GetChild(0).GetComponent<Text>(), PluginManager.InAppPriceType.Subscription);
        //PluginManager.Instance.SetProductText(11, subscriptions[2].transform.GetChild(0).GetChild(0).GetComponent<Text>(), PluginManager.InAppPriceType.Subscription);

        //SelectionFunction(Random.Range(0, subscriptions.Count));

        //foreach (GameObject subscription in subscriptions)
        //{
        //    subscription
        //}
    }


    public void OnSubcriptionClicked(int index)
    {
        SelectionFunction(index);


    }




    public void SelectionFunction(int index)
    {
        SelectedIndex = index;

        foreach (GameObject subscription in subscriptions)
        {


            if (subscriptions.IndexOf(subscription).Equals(index))
            {

                subscription.GetComponent<Image>().sprite = SelectedSprite;
                subscription.transform.GetChild(0).gameObject.SetActive(false);
                subscription.transform.GetChild(1).gameObject.SetActive(true);
                subscription.transform.localScale = Vector3.one * 1.584144f;

            }
            else
            {
                subscription.GetComponent<Image>().sprite = NormalSprite;
                subscription.transform.GetChild(0).gameObject.SetActive(true);
                subscription.transform.GetChild(1).gameObject.SetActive(false);
                subscription.transform.localScale = Vector3.one;
            }
        }

    }


    public void Cancel()
    {

        //Should excute first
        if (BigCodeLibHandler_BigCode.Instance)
            BigCodeLibHandler_BigCode.Instance.StackManager.pages.RemoveAt(BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count - 1);

        //if (SliderMenuAdHandler.Instance && MenuPageHandler.Instance && MenuPageHandler.Instance.gameObject.activeSelf)
        //    SliderMenuAdHandler.Instance.ShowMenuAdIcon();

        SubscriptionPage.SetActive(false);
    }



    public void StartNow(int SelectedIndex)
    {
        switch (SelectedIndex)
        {
            case 0:

                PluginManager.Instance.PurchaseProduct(10, PluginManager.InAppPriceType.Subscription);

                break;

            case 1:

                PluginManager.Instance.PurchaseProduct(9, PluginManager.InAppPriceType.Subscription);
                break;
            case 2:

                PluginManager.Instance.PurchaseProduct(11, PluginManager.InAppPriceType.Subscription);
                break;
        }

        Cancel();


        //VIPButton.SetActive(false);
    }

    public void TermsAndConditions()
    {
        Application.OpenURL("http://mtsfreegames.com/userlicenseagreement-mtsfreegames.html");

    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("http://multitouchstudio.com/privacypolicy.html");
    }


    public void ShowSubscriptionPage()
    {

       

        LoadSubscription();

        SubscriptionPage.SetActive(true);
    }


    public bool isShowing
    {
        get { return SubscriptionPage.activeSelf; }
        
    }
}
