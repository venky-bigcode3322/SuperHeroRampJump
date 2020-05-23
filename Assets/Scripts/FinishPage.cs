using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPage : PopupBase
{
    public override AllPages CurrentPage => AllPages.FinishPage;

    public override bool IsActive => gameObject.activeSelf;

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    [SerializeField] Text LevelRewardText;
    [SerializeField] Text BonusRewardText;
    [SerializeField] Text AirTimeBonusText;
    [SerializeField] Text TotalText;

    [SerializeField] GameObject ContinueButton;

    private void OnEnable()
    {
        ContinueButton.SetActive(false);
        DisplayScores();
    }

    void DisplayScores()
    {
        Invoke("EnableNextButton",3);
    }

    void EnableNextButton()
    {
        ContinueButton.SetActive(true);
        iTween.ScaleFrom(ContinueButton, iTween.Hash("scale", Vector3.zero, "time", 0.85f, "easeType", iTween.EaseType.easeOutBack));
    }

    public void Continue()
    {
        GlobalVariables.NextSceneToLoad = SceneNames.GameScene;
        SceneManager.LoadScene(SceneNames.Loading);
    }
}