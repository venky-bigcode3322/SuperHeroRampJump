using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DailyBonusPage : MonoBehaviour
{
    public CanvasGroup[] ClaimObjects;

    public Button ClaimButton;

    private DateTime lasttime;

    private string Key = "DailyCoins";

    [SerializeField] GameObject[] AllButtons;

    private Vector3[] DefaultButtonPositions;

    //void Start()
    //{
    //    CheckDailyBonus();
    //}

    private void Awake()
    {
        DefaultButtonPositions = new Vector3[AllButtons.Length];

        for (int i = 0; i < DefaultButtonPositions.Length; i++)
            DefaultButtonPositions[i] = AllButtons[i].transform.localPosition;
    }

    public void Open()
    {
        gameObject.SetActive(true);

        StartCoroutine(AnimatePage(0));
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private int currentDailyBonus = 0;

    void SetCliamButton(int index)
    {
        Open();
        for (int i = 0; i < ClaimObjects.Length; i++)
        {
            if (i > index)
            {
                ClaimObjects[i].alpha = 0.5f;
            }
            else 
            {
                ClaimObjects[i].alpha = 1;
            }
        }
    }

    public void StartTimer()
    {
        lasttime = DateTime.Now;
        PlayerPrefs.SetString(Key, lasttime.ToString());
    }

    public TimeSpan GetFromLastTimer()
    {
        String val = "n";
        TimeSpan tm = new TimeSpan();
        DateTime old;

        Debug.Log("--Has keyy " + PlayerPrefs.HasKey(Key));

        if (PlayerPrefs.HasKey(Key))
        {
            val = PlayerPrefs.GetString(Key);
            old = DateTime.Parse(val);
            tm = DateTime.Now - old;
        }
        else
        {
            print("Not saved time..");
        }
        return tm;
    }

    public void CheckDailyBonus()
    {
        TimeSpan ts = GetFromLastTimer();

        if (PlayerPrefs.GetInt("DayKey", 0) >= 7)
        {
            if (ts.Days >= 1)
            {
                PlayerPrefs.SetInt("DayKey", 7);
                SetCliamButton(6);
                return;
            }
        }

        if (ts.Minutes == 0 && ts.Seconds == 0 && ts.Hours == 0 && ts.Days == 0)
        {
            print("TS Min : " + PlayerPrefs.GetInt("DayKey", 0));
            //coins = daycoins[0];
            PlayerPrefs.SetInt("DayKey", 1);
            SetCliamButton(0);
        }
        else if (ts.Days >= 1)
        {
            //	if(ts.Minutes>=1){// testing..
            int id = PlayerPrefs.GetInt("DayKey", 0);
            Debug.Log("Day Index" + id);
            PlayerPrefs.SetInt("DayKey", id + 1);
            //coins = daycoins[id];
            SetCliamButton(id);
        }
        else
        {
            Close();
        }
    }

    public void ClaimButtonClick()
    {
        switch (currentDailyBonus)
        {
            case 0:
                GlobalVariables.AddCoins(500);
                break;
            case 1:
                GlobalVariables.AddDiamonds(50);
                break;
            case 2:
                GlobalVariables.UnlockBike(2);
                break;
            case 3:
                GlobalVariables.AddCoins(3000);
                break;
            case 4:
                GlobalVariables.AddDiamonds(100);
                break;
            case 5:
                GlobalVariables.AddCoins(5000);
                break;
            case 6:
                GlobalVariables.AddCoins(7000);
                GlobalVariables.UnlockBike(6);
                break;
        }

        StartTimer();
        ClaimButton.interactable = false;
        Close();
    }

    // private Vector3 dir = Vector3.zero;

    IEnumerator AnimatePage(float waitTime)
    {
        for (int i = 0; i < AllButtons.Length; i++)
            AllButtons[i].SetActive(false);

        for (int i = 0; i < DefaultButtonPositions.Length; i++)
            AllButtons[i].transform.localPosition = DefaultButtonPositions[i];

        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < AllButtons.Length; i++)
        {
            AllButtons[i].SetActive(true);
            //dir = AllButtons[i].transform.localPosition;

            //if (i < 3)
            //{
            //    dir.y = -1000;
            //}
            //else if (i > 2 && i < 5)
            //{
            //    dir.x = -1000;
            //}
            //else if (i % 2 == 0)
            //{
            //    dir.x = 1000;
            //}
            //else
            //{
            //    dir.x = -1000;
            //}

            iTween.ScaleFrom(AllButtons[i], iTween.Hash("Scale", Vector3.zero, "time", 0.65f, "isLocal", true, "easeType", iTween.EaseType.easeOutBack));
            iTween.RotateFrom(AllButtons[i], iTween.Hash("z", 45, "time", 0.65f, "isLocal", true, "easeType", iTween.EaseType.easeOutBack));

            yield return new WaitForSeconds(0.1f);
        }
    }
}