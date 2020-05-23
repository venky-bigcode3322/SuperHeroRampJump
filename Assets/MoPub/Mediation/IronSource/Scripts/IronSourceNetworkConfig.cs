#if mopub_manager
using UnityEngine;
using UnityEngine.Serialization;

public class IronSourceNetworkConfig : MoPubNetworkConfig
{
    public override string AdapterConfigurationClassName
    {
        get { return Application.platform == RuntimePlatform.Android
                  ? "com.mopub.mobileads.IronSourceAdapterConfiguration"
                  : "IronSourceAdapterConfiguration"; }
    }

    [Tooltip("Enter your application key to be used to initialize the IronSource SDK.")]
    [Config.Optional]
    [FormerlySerializedAs("appKey")]
    public PlatformSpecificString applicationKey;
}
#endif
