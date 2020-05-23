//using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserItem : MonoBehaviour
{

    public Text Name;
    public Image ProfilePicture;
    [HideInInspector]
    public string userID;
    //[HideInInspector]
    public int itemIndex;

    [HideInInspector]
    public Toggle checkBox;

    // Start is called before the first frame update
    void Start()
    {
        checkBox.onValueChanged.AddListener((isSelected) => {

            if (isSelected)
            {
                FacebookRequestFriendsHandler.Instance.selectedFriends.Add(userID);
            }
            else
            {
                if(FacebookRequestFriendsHandler.Instance.selectedFriends.Contains(userID))
                FacebookRequestFriendsHandler.Instance.selectedFriends.Remove(userID);
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [HideInInspector]
    public Texture2D PlayerProfilePic;

    public void LoadProfilePicture()
    {
        //string query = "/"+userID+"/picture?g&width=128&height=128&redirect=false";
        //FB.API(query, HttpMethod.GET, result => {
        //    if (result.Error != null)
        //    {
        //        Debug.LogError(result.Error);
        //        return;
        //    }

        //    string pictureUrl = DeserializePictureUrl(result.RawResult);
        //    StartCoroutine(DownloadPicture(pictureUrl, MyPictureCallback));
        //});
    }

    public string DeserializePictureUrl(string result)
    {
        //var pictureUrlObj = Facebook.MiniJSON.Json.Deserialize(result);
        //var pictureData = (Dictionary<string, object>)(((Dictionary<string, object>)pictureUrlObj)["data"]);
        //object pictureUrl = null;
        //if (pictureData.TryGetValue("url", out pictureUrl))
        //{
        //    return (string)pictureUrl;
        //}
        return string.Empty;
    }

    IEnumerator DownloadPicture(string url, FacebookHandler_BigCode.PictureCallback callback)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            callback(www.texture);
        }
    }

    public void MyPictureCallback(Texture2D texture)
    {
        PlayerProfilePic = texture;

        ProfilePicture.sprite = Sprite.Create(PlayerProfilePic, new Rect(0, 0, PlayerProfilePic.width, PlayerProfilePic.height), new Vector2(PlayerProfilePic.width / 2, PlayerProfilePic.height / 2)); 
    }

    public Button ReceiveButton, SendButton;



    public void Receive()
    {

        ReceiveButton.interactable = false;

        // Delete benfit given ask requests of friend
        foreach (AppRequest apprequest in FacebookRequestFriendsHandler.Instance.AppRequestsByFriend[userID].gives)
        {
            //Give ids
            Debug.LogError(apprequest.id);
            FacebookRequestFriendsHandler.Instance.DeleteRequest(apprequest.id);
        }
    }


    public void Send()
    {
        //FB.AppRequest("Come play this great game!", new List<string>() { userID }, null/*new List<object>() { "app_non_users" }*/, null, null, "give", null, (result) =>
        //{
        //    if (!result.Cancelled)
        //    {
        //        SendButton.interactable = false;

        //        // delete benefit given ask requests of friend
        //        foreach (AppRequest apprequest in FacebookRequestFriendsHandler.Instance.AppRequestsByFriend[userID].asks)
        //        {
        //            // ask ids
        //            Debug.LogError(apprequest.id);
        //            FacebookRequestFriendsHandler.Instance.DeleteRequest(apprequest.id);
        //        }
        //    }

        //    Debug.LogError(result.RawResult);
        //});
    }

    

}
