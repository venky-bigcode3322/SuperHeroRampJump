//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using FlurrySDK;
//using UnityEngine.Purchasing;

//public class FlurryAnalyticsHandler_BigCode : MonoBehaviour

//{ 

//#if UNITY_ANDROID
//private string FLURRY_API_KEY = "ANDROID_API_KEY";
//#elif UNITY_IPHONE
//    private string FLURRY_API_KEY = "IOS_API_KEY";
//#else
//    private string FLURRY_API_KEY = null;
//#endif

//    public static FlurryAnalyticsHandler_BigCode Instance;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    public string APIKey;

//    // Start is called before the first frame update
//    void Start()
//    {

//        FLURRY_API_KEY = APIKey;

//        // Initialize Flurry.
//        new Flurry.Builder()
//                  .WithCrashReporting(true)
//                  .WithLogEnabled(true)
//                  .WithLogLevel(Flurry.LogLevel.VERBOSE)
//                  .WithMessaging(true)
//                  .Build(FLURRY_API_KEY);
//    }


//    public void LogEvent(string page)
//    {
//        Flurry.LogEvent(page);
//    }

//    public void LogEvent(string page, int levelNo)
//    {
//        Dictionary<string, string> customEvents = new Dictionary<string, string>();
//        customEvents.Add(page.ToString(), levelNo.ToString());

//        Flurry.LogEvent(page.ToString(), customEvents);
//    }

//    public void TrackPurchase(Product  product)
//    {
//        Flurry.LogPayment(product.definition.id, product.definition.id, 1, (double)product.metadata.localizedPrice, product.metadata.isoCurrencyCode, product.transactionID, null);
//    }
//}
