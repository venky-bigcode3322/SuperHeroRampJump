using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenjinAnalyticsHadler_Bigcode : MonoBehaviour
{
    public static TenjinAnalyticsHadler_Bigcode Instance;
    public string API_Key;
    private BaseTenjin BaseTenjin;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        BaseTenjin = Tenjin.getInstance(API_Key);
        BaseTenjin.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            //do nothing
        }
        else
        {
            BaseTenjin = Tenjin.getInstance(API_Key);
            BaseTenjin.Connect();
        }
    }

    public void sendEvent(string eventName)
    {
        BaseTenjin.SendEvent(eventName);
    }

    public void sendEvent(string eventName,string value)
    {
        BaseTenjin.SendEvent(eventName,value);
    }

    public void purchaseEvent()
    {
        
    }
}
