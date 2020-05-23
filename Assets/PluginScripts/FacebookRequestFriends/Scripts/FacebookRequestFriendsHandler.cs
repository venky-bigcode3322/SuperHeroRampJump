using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Facebook.Unity;
using UnityEngine.UI;

public class FacebookRequestFriendsHandler : MonoBehaviour
{
    public Transform m_FacebookLoginButton;
    public Transform m_FacebookRequestButton;

    public Toggle SelectAll;
    public Button AskSelectedButton;

    public static FacebookRequestFriendsHandler Instance;

    public delegate void FacebookFriendsLoaded(FacebookFriends facebookFriends);
    public static event FacebookFriendsLoaded OnFacebookFriendsLoaded;


    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (LoginHandler.Instance.getAuthenticatedType() == LoginHandler.LoginType.FACEBOOK)
            LoginHandler.Instance.Login(LoginHandler.LoginType.FACEBOOK);

        //if (LoginHandler.Instance.getAuthenticatedType() == LoginHandler.LoginType.FACEBOOK)
        //{
        //    m_FacebookLoginButton.gameObject.SetActive(false);

        //    OnAuthenticated();
        //}
        //else
        //{
        //    m_FacebookLoginButton.gameObject.SetActive(true);
        //}



        SelectAll.onValueChanged.AddListener(((isSelected) => {


            if (isSelected)
            {
                foreach (Transform child in FacebookFriendsHandler.Instance.Content.transform)
                {
                    UserItem userItem = child.transform.GetComponent<UserItem>();
                    userItem.checkBox.isOn = isSelected;
                }
            }
            else
            {
                foreach (Transform child in FacebookFriendsHandler.Instance.Content.transform)
                {
                    UserItem userItem = child.transform.GetComponent<UserItem>();
                    userItem.checkBox.isOn = isSelected;
                }
            }

        }));
    }

    private void OnEnable()
    {
        LoginHandler.Authenticated += OnAuthenticated;
        LoginHandler.AuthenticateFailed += OnAuthenticateFailed;
        LoginHandler.LoadName += LoadName; ;

    }

    private void OnDisable()
    {
        LoginHandler.Authenticated -= OnAuthenticated;
        LoginHandler.AuthenticateFailed -= OnAuthenticateFailed;
        LoginHandler.LoadName -= LoadName; ;

    }

    public void LoginButton()
    {
        LoginHandler.Instance.Login(LoginHandler.LoginType.FACEBOOK);
    }

    void OnAuthenticated()
    {
        //Disable Login Button
        m_FacebookLoginButton.gameObject.SetActive(false);

        //Enable App Request Button
        m_FacebookRequestButton.gameObject.SetActive(true);



        //Get List of all gamerequests
        GetAllAppRequests();
    }

    void OnAuthenticateFailed()
    {
        m_FacebookLoginButton.gameObject.SetActive(false);
    }


    public void LoadName(string name)
    {
        Debug.LogError(name);
    }


    public void SendGameRequestToFriends()
    {
        //FB.AppRequest("Come play this great game!", null, null/*new List<object>() { "app_non_users" }*/, null, null, null, null, (result) =>
        //{

        //});
    }


    //[HideInInspector]
    public AppRequests AppRequests;

    //[HideInInspector]
    public FacebookFriends FacebookFriends;
 
    public Dictionary<string, AppRequestsAskGet> AppRequestsByFriend = new Dictionary<string, AppRequestsAskGet>();

    private string AccessToken = "EAAFG9ixsBrYBADnSzZAoikP5O04ZAdmYzC6MUdtuBQn9voE1mlZBz2iWGMtooE73SKYwnJRtTDDZBkbTnszg5J1QQo940Uk7JU6fAE7orKxsEgfTF8wlw9azNQFgZCLWqOZA1BVDtC67sYhqoFSFIQQKIMBO3QpwxyyXzrpc39WEn6UH4iwPBCOcGU0oZB8kT9rlvsZCe6VDC2y5EMZCKS3QsWPZCzu92VxD4ZD";
    //private string AccessToken = "EAAFG9ixsBrYBABXAnMBZBx2ivPg2w64n11ZAGXkGbwikHQwFmPavsZAL2ZAA1jLDvD6Irs3ROIOv3gZCU9eAOpnbbmqjurwCfZAHbR7ljjp9FWZCgpghjCI09hmZAiLZCBzxAXMXmLZASuLLB3aZACvc8uEkmbeORHjIrZBZCCD6gsc7NhO0YJcsJTRGcJIZClFcEpZBElcOuinDT2hnlx8kC1ZAQ0h0Ygt2golVrWcZD";



    private void GetAllAppRequests()
    {
        //FB.API("/me/apprequests/"/*?access_token="+AccessToken*/, HttpMethod.GET, (result) => 
        //{

        //    Debug.LogError(result.RawResult);

        //    AppRequests = JsonUtility.FromJson<AppRequests>(result.RawResult);

        //    // Filter AppRequests By Friends
        //    DivideAppRequestByFriend(AppRequests);


        //    // Delete Empty Requests
        //    deleteEmptyRequests();

        //    // Get List Of Friends who Already Installed Game.
        //    GetGameAuthenticatedAllFriends();

        //});
    }

    public void DivideAppRequestByFriend(AppRequests appRequests)
    {
        foreach (AppRequest apprequest in AppRequests.data)
        {
            if (AppRequestsByFriend.ContainsKey(apprequest.from.id))
            {

                AppRequestsAskGet appRequestsAskGet = AppRequestsByFriend[apprequest.from.id];
                if (apprequest.data == null || apprequest.data.Equals(""))
                {
                    appRequestsAskGet.empty.Add(apprequest);
                }
                else if (apprequest.data.Equals("ask"))
                {
                    appRequestsAskGet.asks.Add(apprequest);
                }
                else if (apprequest.data.Equals("give"))
                {
                    appRequestsAskGet.gives.Add(apprequest);

                }

                AppRequestsByFriend[apprequest.from.id] = appRequestsAskGet;
            }
            else
            {
                AppRequestsAskGet appRequestsAskGet = new AppRequestsAskGet();
                appRequestsAskGet.asks = new List<AppRequest>();
                appRequestsAskGet.gives = new List<AppRequest>();
                appRequestsAskGet.empty = new List<AppRequest>();

                if (apprequest.data == null || apprequest.data.Equals(""))
                {
                    appRequestsAskGet.empty.Add(apprequest);
                }
                else if(apprequest.data.Equals("ask"))
                {
                    appRequestsAskGet.asks.Add(apprequest);
                }
                else if (apprequest.data.Equals("give"))
                {
                    appRequestsAskGet.gives.Add(apprequest);
                }

                AppRequestsByFriend.Add(apprequest.from.id, appRequestsAskGet);
            }
        }
    }


    public void DeleteEmptyRequests(string userId)
    {
        foreach (AppRequest appRequest in AppRequestsByFriend[userId].empty)
        {
            DeleteRequest(appRequest.id);
        }
    }

    private void GetGameAuthenticatedAllFriends()
    {
        //FB.API("/me/friends/"/*?access_token="+AccessToken*/, HttpMethod.GET, (result) =>
        //{
        //    FacebookFriends = JsonUtility.FromJson<FacebookFriends>(result.RawResult);

        //    OnFacebookFriendsLoaded(FacebookFriends);
        //});
    }
    
    public void DeleteRequest(string id)
    {
        //FB.API(id, HttpMethod.DELETE, (result) =>
        //{
        //    if (result.Error == null)
        //    {

        //    }
        //});
    }



    //[HideInInspector]
    public List<string> selectedFriends = new List<string>();
    public void AskSelected()
    {

        List<string> friends = null;

        if (selectedFriends.Count > 0)
        {
            friends = selectedFriends;
        }
        else
        {
            // i thought we can use frinds of serverdata but no need
            //foreach (User user in FacebookFriends.data)
            //{
            //    friends.Add(user.id);
            //}

            PluginManager.Instance.ShowToast("Please Select At Least One Friend");

        }


        //FB.AppRequest("Come play this great game!", friends, null/*new List<object>() { "app_non_users" }*/, null, null, "ask", null, (result) =>
        //{
        //    Debug.LogError(result.RawResult);
        //});

        ////delete empty requests
        //foreach (string userID in friends)
        //{
        //    FacebookRequestFriendsHandler.Instance.DeleteEmptyRequests(userID);
        //}
    }


    public void deleteEmptyRequests()
    {
        foreach (string userID in AppRequestsByFriend.Keys)
        {
            FacebookRequestFriendsHandler.Instance.DeleteEmptyRequests(userID);
        }
    }
}
