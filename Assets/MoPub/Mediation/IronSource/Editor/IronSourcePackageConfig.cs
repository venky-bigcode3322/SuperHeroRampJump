using System.Collections.Generic;

public class IronSourcePackageConfig : PackageConfig
{
    public override string Name
    {
        get { return "IronSource"; }
    }

    public override string Version
    {
        get { return /*UNITY_PACKAGE_VERSION*/"1.1.27"; }
    }

    public override Dictionary<Platform, string> NetworkSdkVersions
    {
        get {
            return new Dictionary<Platform, string> {
                { Platform.ANDROID, /*ANDROID_SDK_VERSION*/"6.16.1" },
                { Platform.IOS, /*IOS_SDK_VERSION*/"6.16.1.0" }
            };
        }
    }

    public override Dictionary<Platform, string> AdapterClassNames
    {
        get {
            return new Dictionary<Platform, string> {
                { Platform.ANDROID, "com.mopub.mobileads.IronSource" },
                { Platform.IOS, "IronSource" }
            };
        }
    }
}
