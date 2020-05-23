using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class GameDataHandler : MonoBehaviour {

    [HideInInspector]
    public GameData GameData;

    public static GameDataHandler Instance;


    private void Awake()
    {
        Instance = this;

        GameData = null;
    }

    private void Start()
    {
        NoInternetLoadGameData();
    }

    public void NoInternetLoadGameData()
    {
        string localData = getLocalGameData(GameConstants_BigCode.LocalGameDataPath);

        if (localData.Equals(string.Empty))
        {
            InitGameData();

            SetLocalData(GameConstants_BigCode.LocalGameDataPath, GameData);
        }
        else
        {
            GameData = JsonUtility.FromJson<GameData>(localData);

            CheckInItSave();
        }
    }

    public string getLocalGameData(string filepath)
    {
        GameData gameData = null;

        if (File.Exists(filepath))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(filepath, FileMode.Open);

                gameData = (GameData)bf.Deserialize(file);
                file.Close();
            }
            catch
            {
                gameData = null;
            }



        }
        
        return JsonUtility.ToJson(gameData);
    }

    public void SetLocalData(string filePath,GameData gameData)
    {
        if (GameData != null)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(filePath);

            //hundreds of values
            bf.Serialize(file, gameData);
            file.Close();
        }


    }

    public void InitGameData()
    {
        GameData = new GameData();

        GameData.lastBenfitGivenDate = string.Empty;


    }

    public const int thisUpdateac = 11;

    public void CheckInItSave()
    {
        // init checking code




        ////////////////////////





        try
        {
            if (GameData.lastBenfitGivenDate.Equals(null))
            {
                GameData.lastBenfitGivenDate = string.Empty;
            }
        }
        catch (NullReferenceException exception)
        {
            GameData.lastBenfitGivenDate = string.Empty;
        }

        SetLocalData(GameConstants_BigCode.LocalGameDataPath, GameDataHandler.Instance.GameData);

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            GameDataHandler.Instance.SetLocalData(GameConstants_BigCode.LocalGameDataPath,GameDataHandler.Instance.GameData);
        }
        
    }
}
