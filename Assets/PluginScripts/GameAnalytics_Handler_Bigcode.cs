//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GameAnalyticsSDK;
//using UnityEngine.Purchasing;

//public class GameAnalytics_Handler_Bigcode : MonoBehaviour {


//    public static GameAnalytics_Handler_Bigcode Instance;


//    private void Awake()
//    {
//        Instance = this;
//    }

//    // Use this for initialization
//    void Start () {

//        GameAnalytics.Initialize();
		
//	}
	
//    public void TrackCustomEvent(string page)
//    {
//        GameAnalytics.NewDesignEvent(page);
//    }

//    public void TrackCustomEvent(string page, int levelNo)
//    {
//        GameAnalytics.NewDesignEvent(page,levelNo);

//    }

//    public void TrackPurchase(Product product)
//    {
//        GameAnalytics.NewBusinessEventGooglePlay(product.metadata.isoCurrencyCode, (int)product.metadata.localizedPrice, product.definition.type.ToString(), product.definition.id, "InApp", product.receipt, InappPurchaseHandler.Instance.Base64Key);
//    }
//}
