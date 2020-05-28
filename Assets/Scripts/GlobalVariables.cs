using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static string NextSceneToLoad = "GameScene";

    public static float FuelPercentage = 21;

    public static float LevelReward;

    public static int BonusReward;

    public static float AirTime;

    public delegate void CoinsUpdate(int coins);

    public static event CoinsUpdate CoinsUpdateEvent;

    public delegate void DiamondsUpdate(int Diamonds);

    public static event DiamondsUpdate DiamondUpdateEvent;

    private static string[] BikeKeys = new string[] { "b1", "b2", "b3", "b4", "b5", "b6", "b7", "b8", "b9", "b10", "b11", "b12" };

    public static bool CheckBikeUnlockedStatus(int index)
    {
        if (PlayerPrefs.GetInt(BikeKeys[0]) == 0) PlayerPrefs.SetInt(BikeKeys[0], 1);

        return PlayerPrefs.GetInt(BikeKeys[index], 0) == 1 ? true : false;
    }

    public static void UnlockBike(int index)
    {
        if (PlayerPrefs.GetInt(BikeKeys[index]) != 1) PlayerPrefs.SetInt(BikeKeys[index], 1);
    }

    public static void ResetScoreValues()
    {
        LevelReward = BonusReward = 0;
        AirTime = 0;
    }

    public static int selectedCharacter
    {
        get => PlayerPrefs.GetInt("SelectedCharacter",0);
        set => PlayerPrefs.SetInt("SelectedCharacter", value);
    }

    public static bool MusicState
    {
        get => PlayerPrefs.GetInt("MusicState",1) == 1 ? true : false;
        set => PlayerPrefs.SetInt("MusicState",  value == true ? 1 : 0);
    }

    public static bool SoundState
    {
        get => PlayerPrefs.GetInt("SoundState", 1) == 1 ? true : false;
        set => PlayerPrefs.SetInt("SoundState", value == true ? 1 : 0);
    }

    public static int GameCoins
    {
        get => PlayerPrefs.GetInt("GameCoins", 0);
        set => PlayerPrefs.SetInt("GameCoins",value);
    }

    public static int GameDiamonds
    {
        get => PlayerPrefs.GetInt("GameDiamonds", 0);
        set => PlayerPrefs.SetInt("GameDiamonds", value);
    }

    public static int UpgradeLevel
    {
        get => PlayerPrefs.GetInt("UpgradeLevel", 0);
        set => PlayerPrefs.SetInt("UpgradeLevel", value);
    }

    public static int UpgradeLevelPrice
    {
        get => PlayerPrefs.GetInt("UpgradeLevelPrice", 1000);
        set => PlayerPrefs.SetInt("UpgradeLevelPrice", value);
    }

    public static int CollectedKeys
    {
        get => PlayerPrefs.GetInt("CollectedKeys", 0);
        set => PlayerPrefs.SetInt("CollectedKeys", value);
    }

    public static void AddCoins(int amount)
    {
        GameCoins += amount;

        if (CoinsUpdateEvent != null)
            CoinsUpdateEvent(GameCoins);
    }

    public static void DeductCoins(int amount)
    {
        GameCoins -= amount;

        if (CoinsUpdateEvent != null)
            CoinsUpdateEvent(GameCoins);
    }

    public static void AddDiamonds(int amount)
    {
        GameDiamonds += amount;

        if (DiamondUpdateEvent != null)
            DiamondUpdateEvent(GameCoins);
    }

    public static void DeductDiamonds(int amount)
    {
        GameDiamonds -= amount;

        if (DiamondUpdateEvent != null)
            DiamondUpdateEvent(GameCoins);
    }
}