using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Purchasing;

public class UnityAnalyticsHandler_BigCode : MonoBehaviour {

    public static UnityAnalyticsHandler_BigCode Instance;

	// Use this for initialization
	void Start () {

        Instance = this;
	}

    public void TrackEvent(string page)
    {
        Analytics.CustomEvent(page.ToString());
    }

    public void TrackEvent(string page,int levelNo)
    {
        Dictionary<string, object> customEvents = new Dictionary<string, object>();
        customEvents.Add(page.ToString(), levelNo);
        Analytics.CustomEvent(page.ToString(),customEvents);
    }

    public void TrackPurchase(Product product)
    {
        Analytics.Transaction(product.definition.id, product.metadata.localizedPrice, product.metadata.isoCurrencyCode);
    }
}
