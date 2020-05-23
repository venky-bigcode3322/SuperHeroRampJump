using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;



public class InappPurchaseHandler : MonoBehaviour,IStoreListener
{

    public List<string> InAppProducts;

    public List<InAppWithDiscountProduct> DiscountProducts;

    public IStoreController m_StoreController;

    public  IExtensionProvider m_StoreExtensionProvider;

    public string Base64Key;

    [HideInInspector]
    public bool isIntialized;

    public static InappPurchaseHandler Instance;

    [HideInInspector]
    public Dictionary<string, UnityEngine.Purchasing.Product> products = new Dictionary<string, UnityEngine.Purchasing.Product>();

    [HideInInspector]
    public Dictionary<string, SubscriptionInfo> subscriptions = new Dictionary<string, SubscriptionInfo>();


    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }

        
    }





    public void InitializePurchasing()
    {
        
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.GooglePlay));

        builder.Configure<IGooglePlayConfiguration>().SetPublicKey(Base64Key);

        //// InAppProducts
        //foreach (InAppProduct inappProduct in InAppProducts)
        //{
        //    if (inappProduct.productType == InAppProductType.Consumable)
        //    {
        //        builder.AddProduct(inappProduct.ProductID, ProductType.Consumable);
        //    }
        //    else if (inappProduct.productType == InAppProductType.NonConsumable)
        //    {
        //        builder.AddProduct(inappProduct.ProductID, ProductType.NonConsumable);
        //    }
        //    else if (inappProduct.productType == InAppProductType.Subscription)
        //    {
        //        builder.AddProduct(inappProduct.ProductID, ProductType.Subscription, new IDs
        //        {
        //            {inappProduct.ProductID, GooglePlay.Name},
        //            {inappProduct.ProductID, AppleAppStore.Name}
        //        });
        //    }
        //}


       //Discount Productss
        foreach (InAppWithDiscountProduct inappProduct in DiscountProducts)
        {
            if (inappProduct.productType == InAppProductType.Consumable)
            {
                foreach (string innerProduct in inappProduct.products)
                {
                    builder.AddProduct(innerProduct, ProductType.Consumable);
                }
            }
            else if (inappProduct.productType == InAppProductType.NonConsumable)
            {
                foreach (string innerProduct in inappProduct.products)
                {
                    builder.AddProduct(innerProduct, ProductType.NonConsumable);
                }
            }
            else if (inappProduct.productType == InAppProductType.Subscription)
            {
                foreach (string innerProduct in inappProduct.products)
                {
                    builder.AddProduct(innerProduct, ProductType.Subscription, new IDs
                    {
                        {innerProduct, GooglePlay.Name},
                        {innerProduct, AppleAppStore.Name}
                    });
                }
            }
        }

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    Dictionary<string, string> introductory_info_dict;
    private IAppleExtensions m_AppleExtensions;

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS " + controller.products.all.Length);

        m_StoreController = controller;

        m_StoreExtensionProvider = extensions;

        m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();

        introductory_info_dict = m_AppleExtensions.GetIntroductoryPriceDictionary();

        LoadProductDetails(controller);

        // If DisCount popup in game we need to initialize unity ads here
        //UnityAdsHandler_BigCode.Instance.Initialize();

        isIntialized = true;
    }

    [HideInInspector]
    public bool isAnyOneSubscribed;

    public void LoadProductDetails(IStoreController controller)
    {
        foreach (UnityEngine.Purchasing.Product nativeProduct in controller.products.all)
        {


            Debug.Log(nativeProduct.definition.id);
            
            products.Add(nativeProduct.definition.id, nativeProduct);

            if (nativeProduct.definition.type == ProductType.NonConsumable)
            {
                if (nativeProduct.hasReceipt)
                {
                    PluginManager.Instance.OnProductPurchased(nativeProduct);
                }
            }

            //Subscription
            if (nativeProduct.definition.type == ProductType.Subscription)
            {
                if (nativeProduct.receipt != null)
                {
                    if (CheckIfProductIsAvailableForSubscriptionManager(nativeProduct.receipt))
                    {
                        string intro_json = (introductory_info_dict == null || !introductory_info_dict.ContainsKey(nativeProduct.definition.storeSpecificId)) ? null : introductory_info_dict[nativeProduct.definition.storeSpecificId];
                        SubscriptionManager p = new SubscriptionManager(nativeProduct, intro_json);
                        SubscriptionInfo info = p.getSubscriptionInfo();

                        if (!InappPurchaseHandler.Instance.subscriptions.ContainsKey(nativeProduct.definition.id))
                            subscriptions.Add(nativeProduct.definition.id, info);

                        Debug.Log("isFreeTrail::::" + (info.isFreeTrial() == Result.True));
                        Debug.Log("isSubscribed:::" + (info.isSubscribed() == Result.True));
                        Debug.Log("isExpired::::::" + (info.isExpired() == Result.False));

                        if ((info.isFreeTrial() == Result.True || info.isSubscribed() == Result.True) && info.isExpired() == Result.False && info.isCancelled() == Result.False)
                        {
                            PluginManager.Instance.OnProductPurchased(nativeProduct);

                            isAnyOneSubscribed = true;
                        }
                        else
                        {
                            if (!isAnyOneSubscribed)
                            {
                                GameDataHandler.Instance.GameData.IsSubscription50Purchased = false;

                                BridgeManager_Bigcode.Instance.OnSubscriptionExpire();

                            }
                        }
                    }
                }
                else
                {
                    if (!isAnyOneSubscribed)
                    {
                        GameDataHandler.Instance.GameData.IsSubscription50Purchased = false;

                        BridgeManager_Bigcode.Instance.OnSubscriptionExpire();


                    }
                }
            }

            PlayerPrefs.SetString(nativeProduct.definition.id, nativeProduct.metadata.localizedPriceString);

        }


        if (!isAnyOneSubscribed)
        {

            //Show Subscription popup Here
            if (SubscriptionHandler.Instance)
                SubscriptionHandler.Instance.ShowSubscriptionPage();
        }


    }


    public DateTime GetSubscriptionPurchasedDate(string id)
    {
        return subscriptions[id].getPurchaseDate();
    }

    public DateTime GetSubscriptionExpireDate(string id)
    {
        return subscriptions[id].getExpireDate();
    }

    public bool IsSubscriptionCancelled(string id)
    {
        return subscriptions[id].isCancelled() == Result.True ? true : false;
    }

    public bool IsSubscriptionAutoRenewing(string id)
    {
        return subscriptions[id].isAutoRenewing() == Result.True ? true : false;
    }





    //#if SUBSCRIPTION_MANAGER
    private bool CheckIfProductIsAvailableForSubscriptionManager(string receipt)
    {
        var receipt_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(receipt);
        if (!receipt_wrapper.ContainsKey("Store") || !receipt_wrapper.ContainsKey("Payload"))
        {
            Debug.Log("The product receipt does not contain enough information");
            return false;
        }
        var store = (string)receipt_wrapper["Store"];
        var payload = (string)receipt_wrapper["Payload"];

        if (payload != null)
        {
            switch (store)
            {
                case GooglePlay.Name:
                    {
                        var payload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(payload);
                        if (!payload_wrapper.ContainsKey("json"))
                        {
                            Debug.Log("The product receipt does not contain enough information, the 'json' field is missing");
                            return false;
                        }
                        var original_json_payload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode((string)payload_wrapper["json"]);
                        if (original_json_payload_wrapper == null || !original_json_payload_wrapper.ContainsKey("developerPayload"))
                        {
                            Debug.Log("The product receipt does not contain enough information, the 'developerPayload' field is missing");
                            return false;
                        }
                        var developerPayloadJSON = (string)original_json_payload_wrapper["developerPayload"];
                        var developerPayload_wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(developerPayloadJSON);
                        if (developerPayload_wrapper == null || !developerPayload_wrapper.ContainsKey("is_free_trial") || !developerPayload_wrapper.ContainsKey("has_introductory_price_trial"))
                        {
                            Debug.Log("The product receipt does not contain enough information, the product is not purchased using 1.19 or later");
                            return false;
                        }
                        return true;
                    }
                case AppleAppStore.Name:
                case AmazonApps.Name:
                case MacAppStore.Name:
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }
        return false;
    }
    //#endif


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("OnInitializeFailed InitializationFailureReason:" + error);

        isIntialized = true;
        //PluginManager.Instance.OnPluginInitializationCompleted();
    }

    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
    {
        if (failureReason == PurchaseFailureReason.DuplicateTransaction)
        {
            PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Already Purchased"));
        }
    }



    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        UnityEngine.Purchasing.Product purchasedProduct = args.purchasedProduct;

        if (purchasedProduct.definition.type == ProductType.Subscription)
        {
            if (purchasedProduct.receipt != null)
            {
                if (InappPurchaseHandler.Instance.CheckIfProductIsAvailableForSubscriptionManager(purchasedProduct.receipt))
                {
                    string intro_json = (InappPurchaseHandler.Instance.introductory_info_dict == null || !InappPurchaseHandler.Instance.introductory_info_dict.ContainsKey(purchasedProduct.definition.storeSpecificId)) ? null : InappPurchaseHandler.Instance.introductory_info_dict[purchasedProduct.definition.storeSpecificId];
                    UnityEngine.Purchasing.SubscriptionManager p = new UnityEngine.Purchasing.SubscriptionManager(purchasedProduct, intro_json);
                    UnityEngine.Purchasing.SubscriptionInfo info = p.getSubscriptionInfo();

                    if (!InappPurchaseHandler.Instance.subscriptions.ContainsKey(purchasedProduct.definition.id))
                        InappPurchaseHandler.Instance.subscriptions.Add(purchasedProduct.definition.id, info);

                    if ((info.isFreeTrial() == Result.True || info.isSubscribed() == Result.True) && info.isExpired() == Result.False && info.isCancelled() == Result.False)
                    {
                        PluginManager.Instance.OnProductPurchased(purchasedProduct);

                        //Track InApp Purchase
                        AnalyticsHandler_BigCode.Instance.TrackPurchase(purchasedProduct);
                    }
                    else
                    {
                        GameDataHandler.Instance.GameData.IsSubscription50Purchased = false;
                    }
                }
            }
        }
        else
        {
            PluginManager.Instance.OnProductPurchased(args.purchasedProduct);

            //Track InApp Purchase
            AnalyticsHandler_BigCode.Instance.TrackPurchase(args.purchasedProduct);
        }

        return PurchaseProcessingResult.Complete;
    }


    public void PurchaseProduct(string productID)
    {
        if (PluginManager.Instance.isInternetAvailable && m_StoreController != null)
        {
            UnityEngine.Purchasing.Product product = m_StoreController.products.WithID(productID);

            if (product != null)
                m_StoreController.InitiatePurchase(product);
            else
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
        }
        else
        {
            PluginManager.Instance.ShowToast("Internet Not Avaialable");
        }
    }




    


    [System.Serializable]
    public class InAppWithDiscountProduct
    {
        public List<string> products;

        public InAppProductType productType;

    }



    public enum InAppProductType
    {
        Consumable,
        NonConsumable,
        Subscription
    }


    //[System.Serializable]
    //public class Product
    //{
    //    public string price;
    //}
}


