using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppFlyerHandler : MonoBehaviour
{
    public string AppFlyersID;

    public static AppFlyerHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AppsFlyer.setAppsFlyerKey(AppFlyersID);


#if UNITY_IOS
          /* Mandatory - set your apple app ID
           NOTE: You should enter the number only and not the "ID" prefix */
          AppsFlyer.setAppID ("YOUR_APP_ID_HERE");
          AppsFlyer.trackAppLaunch ();
#elif UNITY_ANDROID
        /* Mandatory - set your Android package name */
        AppsFlyer.setAppID(Application.identifier);
        /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
        AppsFlyer.init(AppFlyersID, "AppsFlyerTrackerCallbacks");

#endif
    }


    public void LogEvent(string eventName)
    {
        Dictionary<string, string> eventValues = new Dictionary<string, string>();
        eventValues.Add(eventName, "BigCode");
        AppsFlyer.trackRichEvent(eventName, eventValues);
    }


    public void trackEvent(string eventName, Dictionary<string, string> eventValues)
    {
        AppsFlyer.trackRichEvent(eventName, eventValues);
    }




}
