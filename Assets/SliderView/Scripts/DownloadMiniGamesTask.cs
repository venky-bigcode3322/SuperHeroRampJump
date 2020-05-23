using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;

public class DownloadMiniGamesTask 
{

    string link,icon,pkg;
    int fileIndex;

    public DownloadMiniGamesTask(string link,string icon,string pkg,int fileIndex)
    {

        this.link = link;
        this.pkg = pkg;
        this.icon = icon;
        this.fileIndex = fileIndex;

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            WebClient client = new WebClient();
            client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(OnMiniAdDownLoadCompleted);
            client.DownloadFileAsync(new Uri(link.Replace("https", "http")), Application.persistentDataPath + "/" + "AD"+fileIndex+".jpg");
        }
    }



    void OnMiniAdDownLoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            MainThread.Call(() =>
            {
                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    WebClient client = new WebClient();
                    client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(OnMiniAdIconDownLoadCompleted);
                    client.DownloadFileAsync(new Uri(icon.Replace("https", "http")), Application.persistentDataPath + "/" + "AD"+fileIndex+"_I.png");
                }
            });
        }
    }

    static int count;

    void OnMiniAdIconDownLoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Error == null)
        {
            MainThread.Call(() =>
            {
                count++;

                if (count == ServerDataHandler.Instance.CommonData.MiniMoreGames.miniGamesPkg.Count)
                {
                    GameDataHandler.Instance.GameData.miniMoregames = ServerDataHandler.Instance.CommonData.MiniMoreGames.miniGamesPkg;
                    GameDataHandler.Instance.GameData.miniMoregamesVersion = ServerDataHandler.Instance.CommonData.MiniMoreGames.version;
                }

            });
        }
    }
}
