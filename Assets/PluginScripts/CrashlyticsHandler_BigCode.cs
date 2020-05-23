using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;


public class CrashlyticsHandler_BigCode : MonoBehaviour {

    public static CrashlyticsHandler_BigCode Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void TrackCustomEvent(string page)
    {
        Dictionary<string, object> customEvents = new Dictionary<string, object>();
        customEvents.Add("samba", "samba");

        //Answers.LogCustom(page.ToString(), customEvents);
    }

    public void TrackCustomEvent(string page,int levelNo)
    {
        Dictionary<string, object> customEvents = new Dictionary<string, object>();
        customEvents.Add(page.ToString(), levelNo.ToString());

        //Answers.LogCustom(page.ToString(), customEvents);
    }

    public void TrackPurchase(Product product)
    {
        //Answers.LogPurchase(product.metadata.localizedPrice, product.metadata.isoCurrencyCode, true, product.metadata.localizedTitle,product.definition.type.ToString(), product.definition.id);
    }

}
