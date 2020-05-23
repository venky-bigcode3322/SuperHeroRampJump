using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackInstallsHandler : MonoBehaviour {

    public static TrackInstallsHandler Instance;

    [HideInInspector]
    public bool isInitialized;

    public string TrackServer = "http://bigcode.co.in";

    private void Awake()
    {
        Instance = this;
    }


    public IEnumerator Initialize()
    {
        if (!PlayerPrefs.HasKey(GameConstants_BigCode.INSTALLCOUNT_INCREASE))
        {
            WWW www = new WWW(TrackServer+"/gamedownload/installcount.php?gamesid=" + Application.identifier);
            yield return www;
            if (www.error == null)
            {
                Debug.Log("Download count increased Successfully");
                PlayerPrefs.SetString(GameConstants_BigCode.INSTALLCOUNT_INCREASE, "Increased");
            }
            else
            {
                Debug.Log("Download count couldn't be increased");
            }

            isInitialized = true;
            //PluginManager.Instance.OnPluginInitializationCompleted();
        }
        else
        {
            yield return null;

            isInitialized = true;
            //PluginManager.Instance.OnPluginInitializationCompleted();
        }

    }
}
