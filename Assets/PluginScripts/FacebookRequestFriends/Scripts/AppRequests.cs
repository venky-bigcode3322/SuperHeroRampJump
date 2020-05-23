using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AppRequests 
{
    public List<AppRequest> data;
}

[System.Serializable]
public class AppRequest
{
    public FacebookApplication application;
    public string created_time;
    public string data;
    public User from;
    public User to;
    public string id;
    public string message;

}



[System.Serializable]
public class FacebookApplication
{
    public string id;
    public string name;
    public string category;
    public string link;

}

[System.Serializable]
public class User
{
    public string name;
    public string id;

}


[System.Serializable]
public class FacebookFriends
{
    public List<User> data;
}


[System.Serializable]
public class AppRequestUser
{
    public string id;
    public List<FacebookFriends> apprequests;
}


[System.Serializable]
public class AppRequestsByFriend
{
    public List<AppRequestUser> friends;
}

[System.Serializable]
public class AppRequestsAskGet
{
    public List<AppRequest> asks;
    public List<AppRequest> gives;
    public List<AppRequest> empty;

}


