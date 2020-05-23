using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseAnalyticsHandler_Bigcode : MonoBehaviour
{
    public static FirebaseAnalyticsHandler_Bigcode Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //   app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.

                //Debug.LogError("Firebase Avaialble");
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }


    public void LogEvent(string eventName)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName);
    }

    public void LogEvent(string eventName,int levelNo)
    {
        Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[1];
        parameters[0] = new Firebase.Analytics.Parameter("level_no", levelNo);
        Firebase.Analytics.FirebaseAnalytics.LogEvent(eventName,parameters);
    }
}
