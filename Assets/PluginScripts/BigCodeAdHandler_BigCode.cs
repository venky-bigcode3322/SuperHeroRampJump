using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class BigCodeAdHandler_BigCode : MonoBehaviour {

    public static BigCodeAdHandler_BigCode Instance;

    [HideInInspector]
    public bool isIntialized;

    private void Awake()
    {
        Instance = this;
    }

    

    public IEnumerator Initialize()
    {




        if (PlayerPrefs.HasKey(GameConstants_BigCode.PreviousAdPkg))
        {
            if (PlayerPrefs.GetString(GameConstants_BigCode.PreviousAdPkg).Equals(ServerDataHandler.Instance.CommonData.MenuAd.BigCodeAd.PackageName))
            {
                if (PlayerPrefs.HasKey(GameConstants_BigCode.MENUAD_DOWNLOADED))
                {
                    if (DateTime.Today.Subtract(DateTime.Parse(PlayerPrefs.GetString(GameConstants_BigCode.MENUAD_DOWNLOADED, DateTime.Today.ToString()))).TotalDays >= 1)
                    {
                        DownLoadAd();
                    }
                    else
                    {
                        isIntialized = true;
                        PluginManager.Instance.OnBigCodeAdLoaded();
                    }
                }
                else
                {
                    DownLoadAd();
                }
            }
            else
            {
                DownLoadAd();
            }
        }
        else
        {
            DownLoadAd();
        }


        yield return null;

    }



    public IEnumerator DownloadMiniMoreGames()
    {
        for (int i = 0; i < ServerDataHandler.Instance.CommonData.MiniMoreGames.miniGamesPkg.Count; i++)
        {
            string[] splits = ServerDataHandler.Instance.CommonData.MiniMoreGames.miniGamesPkg[i].Split('.');

            string link = null;


            if (PluginManager.Instance.isPotraitGame)
            {
                link = ServerDataHandler.Instance.CommonData.MiniMoreGames.folderLocation + splits[splits.Length - 1] + ".jpg";
            }
            else
            {
                link = ServerDataHandler.Instance.CommonData.MiniMoreGames.folderLocation + splits[splits.Length - 1] + "_L.jpg";
            }

            string icon = ServerDataHandler.Instance.CommonData.MiniMoreGames.folderLocation + splits[splits.Length - 1] + "_I.png";

            string pkg = ServerDataHandler.Instance.CommonData.MiniMoreGames.miniGamesPkg[i];

            yield return StartCoroutine(DownloadNewImage(link, icon, pkg, i));

        }
    }

    IEnumerator DownloadNewImage(string link,string icon,string pkg,int fileIndex)
    {
         DownloadMiniGamesTask downloadMiniGamesTask = new DownloadMiniGamesTask(link, icon, pkg, fileIndex);

         yield return null;
    }




    void DownLoadAd()
    {

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            WebClient client = new WebClient();
            client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(OnAdDownLoadCompleted);

            if (PluginManager.Instance.isPotraitGame)
            {
                client.DownloadFileAsync(new Uri(ServerDataHandler.Instance.CommonData.MenuAd.BigCodeAd.Portrait_Link.Replace("https", "http")), Application.persistentDataPath + "/" + GameConstants_BigCode.BigCodeAd + ".jpg");
            }
            else
            {
                client.DownloadFileAsync(new Uri(ServerDataHandler.Instance.CommonData.MenuAd.BigCodeAd.Landscape_Link.Replace("https", "http")), Application.persistentDataPath + "/" + GameConstants_BigCode.BigCodeAd + ".jpg");
            }
        }
        else
        {
            isIntialized = true;
            PluginManager.Instance.OnBigCodeAdLoaded();
        }

    }


    void OnAdDownLoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            MainThread.Call(() =>
            {
                PlayerPrefs.SetString(GameConstants_BigCode.MENUAD_DOWNLOADED, DateTime.Today.ToString());
                PlayerPrefs.SetString(GameConstants_BigCode.PreviousAdPkg, ServerDataHandler.Instance.CommonData.MenuAd.BigCodeAd.PackageName);

                isIntialized = true;
                PluginManager.Instance.OnBigCodeAdLoaded();

                //PluginManager.Instance.ShowToast("MenuAd Download Completed");
            });

        }
    }


    


}
