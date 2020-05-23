using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dummy_GameManagerHandler : MonoBehaviour {

    public Text text;

    private float timeValue;

    public bool isTimeStart;



    public List<GameObject> UI;
       

	// Use this for initialization
	void Start () {

        StartGame();

        Time.timeScale = 1;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isTimeStart)
        {
            timeValue += Time.deltaTime;
            text.text = timeValue.ToString();

            if (timeValue > 15)
            {
                GameCompleted();
            }

            
        }
        
        
		
	}


    


    public void StartGame()
    {
        StartTimer();

        PluginManager.Instance.RequestInterstitial();
    }

    public void StartTimer()
    {
        timeValue = 0;
        isTimeStart = true;
    }


    public void StopTimer()
    {
        isTimeStart = false;
    }

    public void GameCompleted()
    {
        StopTimer();
        ShowPopup("GameCompleted_UI");

        PluginManager.Instance.ShowLevelCompletedInterstitialAd();
    }

    public void GameFailed()
    {
        StopTimer();
        ShowPopup("GameFailed_UI");

        PluginManager.Instance.ShowLevelFailedInterstitialAd();
    }

    public void ShowPopup(string pageName)
    {
        foreach (GameObject UIOBJ in UI)
        {
            if (UIOBJ.name.Equals(pageName))
                UIOBJ.SetActive(true);
            else
                UIOBJ.SetActive(false);
        }
    }


    public void Pause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            ShowPopup("pausePage_UI");
        }
        else
        {
            Time.timeScale = 1;

            ShowPopup("");
        }
    }

    public void Home()
    {
        SceneManager.LoadScene("MenuScene");
    }


    public void playAgain()
    {
        ShowPopup("");

        Start();
    }
}
