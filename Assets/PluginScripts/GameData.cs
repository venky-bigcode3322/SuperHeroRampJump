using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData  {


    
    public bool NoadsPurchased = false;
    public bool isPromoCodeApplied = false;
    public bool isRated = false;

    public bool IsSubscription50Purchased = false;



    public string SubScriptionExpiryDate;

    public string GoldSubScriptionExpiryDate;
    public string DiamondSubScriptionExpiryDate;
    public string PlatinumSubScriptionExpiryDate;





    public string lastBenfitGivenDate;

    public string GoldlastBenfitGivenDate;
    public string DiamondlastBenfitGivenDate;
    public string PlatinumlastBenfitGivenDate;




    public int SubscriptionType;

    List<string> PurchasedNonConsumableProducts;

    public List<string> miniMoregames;
    public int miniMoregamesVersion;


    public struct JsonDateTime
    {
        public long value;
        public static implicit operator DateTime(JsonDateTime jdt)
        {
            //Debug.Log("Converted to time");
            return DateTime.FromFileTimeUtc(jdt.value);
        }
        public static implicit operator JsonDateTime(DateTime dt)
        {
            // Debug.Log("Converted to JDT");
            JsonDateTime jdt = new JsonDateTime();
            jdt.value = dt.ToFileTimeUtc();
            return jdt;
        }
    }


}
