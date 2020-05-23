//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TwitterKit.Unity;

//public class TwitterHandler : MonoBehaviour {

//    public static TwitterHandler Instance;


//    public delegate void OnEventTwitterAuthenticated(LoginHandler.LoginType loginType);
//    public delegate void OnEventTwitterAuthenticateFailed();
//    //public delegate void OnEventFacebookShare();
//    //public delegate void OnEventFacebookLoadPicture(Sprite image);
//    public delegate void OnEventTwitterLoadName(string name);

//    public static event OnEventTwitterAuthenticated TwitterAuthenticated;
//    public static event OnEventTwitterAuthenticateFailed TwitterAuthenticateFailed;
//    //public static event OnEventFacebookShare FacebookShare;
//    //public static event OnEventFacebookLoadPicture FacebookLoadPicture;
//    public static event OnEventTwitterLoadName TwitterLoadName;

//    private string Name;

//    private void Awake()
//    {
//        Instance = this;


//    }

//    private void Start()
//    {
//        Twitter.Init();
//    }


//    public void Login()
//    {


//        Twitter.LogIn(LoginCompleteWithCompose, (ApiError error) => {

//            UnityEngine.Debug.Log(error.message);

//            //LoginHandler.Instance.OnAuthenticationFinished(false);

//            if (TwitterAuthenticateFailed != null)
//                TwitterAuthenticateFailed();


//        });
//    }

//    public string GetName()
//    {
//        return Name;
//    }

//    public void SignOut()
//    {
//        Twitter.Init();

//        Twitter.LogOut();
//    }


//    public void LoginCompleteWithCompose(TwitterSession session)
//    {

//        Name = session.userName;


//        //LoginHandler.Instance.OnAuthenticationFinished(true);


//        Debug.LogError(session.userName);


//        if (TwitterAuthenticated != null)
//            TwitterAuthenticated(LoginHandler.LoginType.TWITTER);

//        if (TwitterLoadName != null)
//            TwitterLoadName(Name);
//    }
//}
