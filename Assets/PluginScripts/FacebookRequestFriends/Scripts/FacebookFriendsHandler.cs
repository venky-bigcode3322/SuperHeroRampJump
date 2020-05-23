using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacebookFriendsHandler : MonoBehaviour
{
    public static FacebookFriendsHandler Instance;

    public ScrollRect ScrollView;

    public Transform Content;

    public GameObject ItemPrefab;


    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        FacebookRequestFriendsHandler.OnFacebookFriendsLoaded += OnFacebookFriendsLoaded;
    }

    private void OnDisable()
    {
        FacebookRequestFriendsHandler.OnFacebookFriendsLoaded -= OnFacebookFriendsLoaded;
    }

    void OnFacebookFriendsLoaded(FacebookFriends facebookFriends)
    {

        // Enable related Button when friends not empty
        if (facebookFriends.data.Count > 0)
        {
            //Enable Wanted Buttons
            FacebookRequestFriendsHandler.Instance.SelectAll.interactable = true;
            FacebookRequestFriendsHandler.Instance.AskSelectedButton.interactable = true;
        }



        foreach (User user in facebookFriends.data)
        {
            Transform itemObj = GameObject.Instantiate(ItemPrefab.transform);
            itemObj.parent = Content;
            itemObj.localScale = Vector3.one;

            UserItem userItem = itemObj.GetComponent<UserItem>();

            userItem.itemIndex = facebookFriends.data.IndexOf(user);
            userItem.Name.text = user.name;
            userItem.userID = user.id;
            userItem.LoadProfilePicture();

            if (FacebookRequestFriendsHandler.Instance.AppRequestsByFriend.ContainsKey(userItem.userID))
            {
                AppRequestsAskGet appRequestsAskGet = FacebookRequestFriendsHandler.Instance.AppRequestsByFriend[userItem.userID];

                if (appRequestsAskGet.asks.Count > 0)
                {
                    userItem.SendButton.interactable = true;
                }

                if(appRequestsAskGet.gives.Count > 0)
                {
                    userItem.ReceiveButton.interactable = true;
                }

            }

        }
    }
}
