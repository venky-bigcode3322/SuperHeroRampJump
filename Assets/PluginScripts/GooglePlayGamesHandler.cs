using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GooglePlayGames.BasicApi;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi.SavedGame;
using System.Text;
using UnityEngine.SceneManagement;

public class GooglePlayGamesHandler : MonoBehaviour {

    public static GooglePlayGamesHandler Instance;

    [HideInInspector]
    public bool isInitialized;

    [HideInInspector]
    public SignInType signInType = SignInType.NONE;

    [HideInInspector]
    public bool isSignIn;

    //public delegate void PlayGamesSiginIn(bool isSuccess);
    //public static event PlayGamesSiginIn PlayGamesSignInEvent;


    // Use this for initialization
    void Awake () {

        Instance = this;

	}

    
    public void Initialize(SignInType signInType = SignInType.NONE)
    {
        SignIn(signInType);

    }



    public void SignIn(SignInType signInType = SignInType.NONE)
    {


        //if (PluginManager.Instance.isInternetAvailable)
        //{
        //    this.signInType = signInType;

        //    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        //        //.EnableSavedGames()
        //        .Build();

        //    PlayGamesPlatform.InitializeInstance(config);

        //    // recommended for debugging:
        //    PlayGamesPlatform.DebugLogEnabled = true;

        //    // Activate the Google Play Games platform
        //    PlayGamesPlatform.Activate();

        //    Social.localUser.Authenticate(OnAuthenticated);
        //}
        //else if(signInType != SignInType.NONE)
        //{
        //    PluginManager.Instance.ShowToast(LanguageHandler.GetCurrentLanguageText("Internet Not Available"));
        //}
    }


    void OnAuthenticated(bool isSuccess,string strings)
    {
        isSignIn = isSuccess;

        PluginManager.Instance.OnSignIn(isSignIn);

        Debug.LogError("Authentication::::" + isSuccess+strings);

       // PluginManager.Instance.ShowToast("Success");

        if (isSuccess)
        {
            switch (signInType)
            {
                case SignInType.CLOUD_DATA:

                    OpenSavedGames("GameName");
                    Debug.Log("Login Success");

                    break;
                case SignInType.ACHEIVMENTS:

                    Social.ShowAchievementsUI();

                    break;
                case SignInType.LEADERBOARD:

                    Social.ShowLeaderboardUI();

                    break;

                case SignInType.NONE:

                  
                   
                 /*   GameDataHandler.Instance.NoInternetLoadGameData();

                    isInitialized = true;
                    //PluginManager.Instance.OnDataLoaded();

                    if (!SceneManager.GetActiveScene().name.Equals(PluginManager.Instance.NextScene))
                        MenuAdHandler_BigCode.Instance.LoadGameScene();  */


                    break;
            }
        }
        else
        {
            if (signInType == SignInType.NONE || signInType == SignInType.CLOUD_DATA)
            {
                /* GameDataHandler.Instance.NoInternetLoadGameData();

                isInitialized = true;
                //PluginManager.Instance.OnDataLoaded();

                MenuAdHandler_BigCode.Instance.LoadGameScene();  */
            }
        }
    }



    public void ReportScore(string leaderboardID,long score)
    {
        if(PluginManager.Instance.isInternetAvailable)
            Social.ReportScore(score, leaderboardID, OnLeaderboardReported);
    }

    public void ReportProgress(string achievmentID, double progress)
    {
        if (PluginManager.Instance.isInternetAvailable)
            Social.ReportProgress(achievmentID, progress, OnAchievmentsReported);
    }

    public void IncrementAchievement(string achievmentID, int steps)
    {
        //if (PluginManager.Instance.isInternetAvailable)
        //    PlayGamesPlatform.Instance.IncrementAchievement(achievmentID, steps,OnAchievmentsIncrementedReported);
    }

    public void OpenSavedGames(string filename)
    {
        //ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        //savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
        //    ConflictResolutionStrategy.UseMostRecentlySaved, OnSavedGameOpened);
    }


    private void OnLeaderboardReported(bool Success)
    {
        
    }

    private void OnAchievmentsReported(bool Success)
    {

    }

    private void OnAchievmentsIncrementedReported(bool Success)
    {

    }

    //ISavedGameMetadata game;
    //ISavedGameClient savedGameClient;

    //private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    //{
    //    if (status == SavedGameRequestStatus.Success)
    //    {
    //        this.game = game;

    //        savedGameClient = PlayGamesPlatform.Instance.SavedGame;
    //        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);

    //    }
    //    else
    //    {
    //        isInitialized = true;
    //        //GameDataHandler.Instance.NoInternetLoadGameData();
    //        //PluginManager.Instance.OnDataLoaded();

    //        MenuAdHandler_BigCode.Instance.LoadGameScene();
    //    }
    //}


    //public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    //{
    //    if (status == SavedGameRequestStatus.Success)
    //    {
    //        string cloudData = Encoding.ASCII.GetString(data);

    //        string localData = GameDataHandler.Instance.getLocalGameData(GameConstants_BigCode.LocalGameDataPath);

    //        if (cloudData.Equals(string.Empty) && localData.Equals(string.Empty))
    //        {
    //            GameDataHandler.Instance.InitGameData();

    //            GameDataHandler.Instance.SetLocalData(GameConstants_BigCode.LocalGameDataPath, GameDataHandler.Instance.GameData);

    //            byte[] dataToSave = Encoding.ASCII.GetBytes(JsonUtility.ToJson(GameDataHandler.Instance.GameData));
    //            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
    //            SavedGameMetadataUpdate updatedMetadata = builder.Build();
    //            savedGameClient.CommitUpdate(game, updatedMetadata, dataToSave, OnSavedGameWritten);

    //            Debug.LogError("Local Data::::" + "New Data");
    //            Debug.LogError("Cloud Data::::" + "New Data");

    //        }
    //        else if (cloudData.Equals(string.Empty) && !localData.Equals(string.Empty))
    //        {
    //            GameDataHandler.Instance.GameData = JsonUtility.FromJson<GameData>(localData);

    //            GameDataHandler.Instance.CheckInItSave();


    //            byte[] dataToSave = Encoding.ASCII.GetBytes(JsonUtility.ToJson(GameDataHandler.Instance.GameData));
    //            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
    //            SavedGameMetadataUpdate updatedMetadata = builder.Build();
    //            savedGameClient.CommitUpdate(game, updatedMetadata, dataToSave, OnSavedGameWritten);

    //            Debug.LogError("Local Data::::" + localData);

    //        }
    //        else if (!cloudData.Equals(string.Empty) && localData.Equals(string.Empty))
    //        {
    //            GameDataHandler.Instance.GameData = JsonUtility.FromJson<GameData>(cloudData);

    //            GameDataHandler.Instance.CheckInItSave();

    //            //GameDataHandler.Instance.SetLocalData(GameConstants.LocalGameDataPath, GameDataHandler.Instance.GameData);

    //            Debug.LogError("Cloud Data::::" + cloudData);

    //        }
    //        else if (!cloudData.Equals(string.Empty) && !localData.Equals(string.Empty))
    //        {
    //            GameDataHandler.Instance.GameData = JsonUtility.FromJson<GameData>(localData);

    //            GameDataHandler.Instance.CheckInItSave();

    //            byte[] dataToSave = Encoding.ASCII.GetBytes(JsonUtility.ToJson(GameDataHandler.Instance.GameData));
    //            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
    //            SavedGameMetadataUpdate updatedMetadata = builder.Build();
    //            savedGameClient.CommitUpdate(game, updatedMetadata, dataToSave, OnSavedGameWritten);

    //            Debug.LogError("Local Data::::" + localData);
    //            Debug.LogError("Cloud Data::::" + cloudData);


    //        }
    //        else
    //        {
    //            //GameDataHandler.Instance.NoInternetLoadGameData();
    //        }



    //        isInitialized = true;
    //        //PluginManager.Instance.OnDataLoaded();

    //        MenuAdHandler_BigCode.Instance.LoadGameScene();
    //    }
    //    else
    //    {
    //        isInitialized = true;

    //        //GameDataHandler.Instance.NoInternetLoadGameData();
    //        //PluginManager.Instance.OnDataLoaded();

    //        MenuAdHandler_BigCode.Instance.LoadGameScene();
    //    }
    //}

    //public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    //{

    //}

    public enum SignInType
    {
        LEADERBOARD,
        ACHEIVMENTS,
        CLOUD_DATA,
        NONE
    }



    

    public void Logout()
    {
#if !UNITY_EDITOR && !UNITY_STANDALONE_WIN
        //PlayGamesPlatform.Instance.SignOut();
#endif
        isSignIn = false;

        PluginManager.Instance.OnSignOut(isSignIn);
    }
}
