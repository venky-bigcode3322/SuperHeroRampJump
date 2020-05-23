using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
//using GameAnalyticsSDK;

public class AnalyticsHandler_BigCode : MonoBehaviour {

    public static AnalyticsHandler_BigCode Instance;

    private Page ReachedPage = Page.None;

    private void Awake()
    {
        Instance = this;
    }

    public void SetReachedPage(Page page)
    {
        //CrashlyticsHandler_BigCode.Instance.TrackCustomEvent(page.ToString());
        FirebaseAnalyticsHandler_Bigcode.Instance.LogEvent(page.ToString());
        FacebookHandler_BigCode.Instance.LogEvent(page.ToString());
        UnityAnalyticsHandler_BigCode.Instance.TrackEvent(page.ToString());
        //GameAnalytics_Handler_Bigcode.Instance.TrackCustomEvent(page.ToString());
        //FlurryAnalyticsHandler_BigCode.Instance.LogEvent(page.ToString());
        //AdjustAnalytics_Bigcode.Instance.LogEvent(page.ToString());
        TenjinAnalyticsHadler_Bigcode.Instance.sendEvent(page.ToString());
        AppFlyerHandler.Instance.LogEvent(page.ToString());

    }

    public void SetReachedPage(string page)
    {
        //CrashlyticsHandler_BigCode.Instance.TrackCustomEvent(page);
        FirebaseAnalyticsHandler_Bigcode.Instance.LogEvent(page);
        FacebookHandler_BigCode.Instance.LogEvent(page);
        UnityAnalyticsHandler_BigCode.Instance.TrackEvent(page);
        //GameAnalytics_Handler_Bigcode.Instance.TrackCustomEvent(page.ToString());
        //FlurryAnalyticsHandler_BigCode.Instance.LogEvent(page);
        //AdjustAnalytics_Bigcode.Instance.LogEvent(page);
        TenjinAnalyticsHadler_Bigcode.Instance.sendEvent(page);
        AppFlyerHandler.Instance.LogEvent(page);


    }

    public void SetReachedPage(Page page,int levelNo)
    {
        //CrashlyticsHandler_BigCode.Instance.TrackCustomEvent(page.ToString(), levelNo);
        FirebaseAnalyticsHandler_Bigcode.Instance.LogEvent(page.ToString(),levelNo);
        FacebookHandler_BigCode.Instance.LogEvent(page.ToString(), levelNo);
        UnityAnalyticsHandler_BigCode.Instance.TrackEvent(page.ToString(), levelNo);
        //GameAnalytics_Handler_Bigcode.Instance.TrackCustomEvent(page.ToString(), levelNo);
        //FlurryAnalyticsHandler_BigCode.Instance.LogEvent(page.ToString(),levelNo);
        //AdjustAnalytics_Bigcode.Instance.LogEvent(page.ToString(), levelNo);
        TenjinAnalyticsHadler_Bigcode.Instance.sendEvent(page.ToString(),levelNo.ToString());




        Dictionary<string, string> eventValues = new Dictionary<string, string>();
        eventValues.Add(page.ToString(), levelNo.ToString());
        AppFlyerHandler.Instance.trackEvent(page.ToString(), eventValues);

    }

    public void SetReachedPage(string page, int levelNo)
    {
        //CrashlyticsHandler_BigCode.Instance.TrackCustomEvent(page, levelNo);
        FirebaseAnalyticsHandler_Bigcode.Instance.LogEvent(page, levelNo);
        FacebookHandler_BigCode.Instance.LogEvent(page, levelNo);
        UnityAnalyticsHandler_BigCode.Instance.TrackEvent(page, levelNo);
        //GameAnalytics_Handler_Bigcode.Instance.TrackCustomEvent(page,levelNo);
        //FlurryAnalyticsHandler_BigCode.Instance.LogEvent(page, levelNo);
        //AdjustAnalytics_Bigcode.Instance.LogEvent(page, levelNo);
        TenjinAnalyticsHadler_Bigcode.Instance.sendEvent(page.ToString(), levelNo.ToString());

        Dictionary<string, string> eventValues = new Dictionary<string, string>();
        eventValues.Add(page, levelNo.ToString());
        AppFlyerHandler.Instance.trackEvent(page, eventValues);

    }

    public void TrackPurchase(Product product)
    {
        //CrashlyticsHandler_BigCode.Instance.TrackPurchase(product);
        UnityAnalyticsHandler_BigCode.Instance.TrackPurchase(product);
        //GameAnalytics_Handler_Bigcode.Instance.TrackPurchase(product);
        FacebookHandler_BigCode.Instance.TrackPurchase((float)product.metadata.localizedPrice, product.metadata.isoCurrencyCode, product.definition.id, product.definition.type.ToString());
        //FlurryAnalyticsHandler_BigCode.Instance.TrackPurchase(product);
        //AdjustAnalytics_Bigcode.Instance.trackPurchase(product);


        //AppFlyer Purchase
        Dictionary<string, string> eventValues = new Dictionary<string, string>();
        eventValues.Add("product_id", product.definition.id);
        eventValues.Add("local_price", product.metadata.localizedPrice.ToString());
        eventValues.Add("curreny_code", product.metadata.isoCurrencyCode);
        eventValues.Add("purchase_type", product.definition.type.ToString());
        AppFlyerHandler.Instance.trackEvent("inapp_purchase", eventValues);

    }

    public enum Page
    {
        Menu_Page,
        Game_Page,
        LC_page,
        LF_page,
        Pause_To_Home,
        Revive_Clicked,
        Revive_Success,
        BigCodeAd_Clicked,
        None
    }
}
