using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class ServerDataHandler : MonoBehaviour
{

    //[HideInInspector]
    public CommonData CommonData;

    //[HideInInspector]
    public BaseData BaseData;

    [HideInInspector]
    public bool isInitilized;


    private void Start()
    {


    }


    public static ServerDataHandler Instance;

    private void Awake()
    {
        Instance = this;
    }


    public string CommomDataFileName;
    public string BaseDataFileName;



    public IEnumerator LoadServerData()
    {



        if (PluginManager.Instance.isInternetAvailable)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(PluginManager.Instance.CommonDataURL))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();



                if (webRequest.isNetworkError)
                {

#if !UNITY_EDITOR
                    UnityWebRequest localCommonDataRequest = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets/" + CommomDataFileName + ".txt");
                    yield return localCommonDataRequest.SendWebRequest();
                    if (!localCommonDataRequest.isNetworkError)
                    {
                        CommonData = JsonUtility.FromJson<CommonData>(localCommonDataRequest.downloadHandler.text);

                    }
                    else
                    {
                        Debug.LogError(localCommonDataRequest.error);
                    }

#endif


#if UNITY_EDITOR
                    //www = new WWW("file://"+Application.streamingAssetsPath + "/train.txt");
                    //yield return www;
                    //CommonData = JsonUtility.FromJson<CommonData>(www.text);
#endif
                }
                else
                {

                    CommonData = JsonUtility.FromJson<CommonData>(webRequest.downloadHandler.text);

                }
            }















            using (UnityWebRequest webRequest = UnityWebRequest.Get(PluginManager.Instance.BaseDataURL))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();


                if (webRequest.isNetworkError)
                {
#if !UNITY_EDITOR
                    UnityWebRequest localBaseDataRequest = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets/" + BaseDataFileName + ".txt");
                    yield return localBaseDataRequest.SendWebRequest();
                    if (!localBaseDataRequest.isNetworkError)
                    {
                        BaseData = JsonUtility.FromJson<BaseData>(localBaseDataRequest.downloadHandler.text);

                    }
                    else
                    {
                        Debug.LogError(localBaseDataRequest.error);

                    }

#endif
#if UNITY_EDITOR
                    //www = new WWW("file://" + Application.streamingAssetsPath+"/trainracingmultiplayer.txt");
                    //yield return www;
                    //BaseData = JsonUtility.FromJson<BaseData>(www.text);
#endif
                }
                else
                {

                    BaseData = JsonUtility.FromJson<BaseData>(webRequest.downloadHandler.text);

                }
            }




        }
        else
        {


            UnityWebRequest localCommonDataRequest = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets/" + CommomDataFileName + ".txt");
            yield return localCommonDataRequest.SendWebRequest();
            if (!localCommonDataRequest.isNetworkError)
            {
                CommonData = JsonUtility.FromJson<CommonData>(localCommonDataRequest.downloadHandler.text);

            }
            else
            {
                Debug.LogError(localCommonDataRequest.error);
            }


            UnityWebRequest localBaseDataRequest = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets/" + BaseDataFileName + ".txt");
            yield return localBaseDataRequest.SendWebRequest();
            if (!localBaseDataRequest.isNetworkError)
            {
                BaseData = JsonUtility.FromJson<BaseData>(localBaseDataRequest.downloadHandler.text);
            }
            else
            {
                Debug.LogError(localBaseDataRequest.error);
            }

        }











        yield return null;

        // StartCoroutine(SmileyKidoosHandler.Instance.GetSmileyKidoosCouponData()); 

        if (PluginManager.Instance.isInternetAvailable)
            BigCodeLibHandler_BigCode.Instance.LoadWebView();


        PluginManager.Instance.lastShown = BaseData.AdsDelay.AdToAdDelay;




#if UNITY_EDITOR



        //BaseData.InterstitialAds[0] = ADS.ADMOB;

        //BaseData.InterstitialAds[1] = ADS.IRONSOURCE;

        //BaseData.InterstitialAds[2] = ADS.UNITY;

        //BaseData.ExitGame.gameLink = "https://bigcode.s3.us-west-2.amazonaws.com/mg/trainracingmultiplayer.jpg";
        //BaseData.ExitGame.gamePackage = "com.mtsfreegames.trainracingmultiplayer";

#endif


        isInitilized = true;
        PluginManager.Instance.OnServerDataLoaded();
    }




}
