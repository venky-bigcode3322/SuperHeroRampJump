using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    [SerializeField] Text DistanceText;

    [SerializeField] Text DistanceRewardText;
    [SerializeField] Text BonusRewardText;
    [SerializeField] Text AirTimeBonusText;
    [SerializeField] Text TotalText;
    [SerializeField] Text TripleRewardTotalText;

    [SerializeField] GameObject ContinueButton;

    private void OnEnable()
    {
        ContinueButton.SetActive(false);
        DisplayScores();
    }

    float currentTotalScore = 0;

    void DisplayScores()
    {
        currentTotalScore = 0;

        DistanceText.text = GlobalVariables.CurrentJumpingDistance+"m";

        DistanceRewardText.text = GlobalVariables.DistanceReward.ToString();

        BonusRewardText.text = GlobalVariables.BonusReward.ToString();

        AirTimeBonusText.text = GlobalVariables.AirTime.ToString();

        currentTotalScore += GlobalVariables.DistanceReward + GlobalVariables.AirTime + GlobalVariables.BonusReward;

        TotalText.text = currentTotalScore.ToString();
        TripleRewardTotalText.text = (currentTotalScore * 3).ToString();

        GlobalVariables.GameCoins += Mathf.RoundToInt(currentTotalScore);

        Invoke("EnableNextButton",3);
    }

    void EnableNextButton()
    {
        ContinueButton.SetActive(true);
        iTween.ScaleFrom(ContinueButton, iTween.Hash("scale", Vector3.zero, "time", 0.85f, "easeType", iTween.EaseType.easeOutBack));
    }

    public void Continue()
    {
        if (UiHandler.Instance) UiHandler.Instance.ShowPopup(CurrentPage, AllPages.UnlockProgressPage);
        //GlobalVariables.NextSceneToLoad = SceneNames.GameScene;
        //SceneManager.LoadScene(SceneNames.Loading);
    }

    public void TripperRewardButton()
    {

    }

    public void TrippleRewardSuccess()
    {
        GlobalVariables.GameCoins += Mathf.RoundToInt(currentTotalScore * 3);
    }
}