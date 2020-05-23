using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSignalHandler_BigCode : MonoBehaviour {

    public string OneSignalID;

    
    private static bool requiresUserPrivacyConsent = false;
    // Use this for initialization
    void Start () {

        //if (SystemInfo.deviceModel.Contains("LAVA"))
        //    return;

        //OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE, OneSignal.LOG_LEVEL.NONE);

        
        //OneSignal.SetRequiresUserPrivacyConsent(requiresUserPrivacyConsent);

       
        //OneSignal.StartInit(OneSignalID)
        //         .EndInit();
        //OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
