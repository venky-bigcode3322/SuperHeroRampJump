using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static float FuelPercentage = 21;

    public delegate void CoinsUpdate(int coins);

    public static event CoinsUpdate CoinsUpdateEvent;

    public delegate void DiamondsUpdate(int Diamonds);

    public static event DiamondsUpdate DiamondUpdateEvent;

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