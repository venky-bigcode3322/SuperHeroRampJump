using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnlockProgressPage : PopupBase
{
    public override AllPages CurrentPage => AllPages.UnlockProgressPage;

    public override bool IsActive => gameObject.activeSelf;

    private float[] TargetDistanceMeters = new float[] { 1500,3000,9000,12000};

    [SerializeField] Image CharacterProgressBar;

    [SerializeField] Button ClaimButton;

    [SerializeField] GameObject ContinueButton;

    [SerializeField] Text DescriptionText;

    [SerializeField] GameObject[] CharObjs;

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    public void Back()
    {
        if (UiHandler.Instance) UiHandler.Instance.CheckAndClosePopup();
    }

    private void OnEnable()
    {
        ContinueButton.SetActive(false);
        ShowBar();
    }

    void ShowBar()
    {
        ClaimButton.interactable = false;
        CharObjs[GlobalVariables.DistanceTarget].SetActive(true);
        DescriptionText.text = "Reach " + TargetDistanceMeters[GlobalVariables.DistanceTarget] + " meters to unlock this character";
        var val = GlobalVariables.CurrentJumpingDistance / TargetDistanceMeters[GlobalVariables.DistanceTarget];
        Debug.Log("CurrentDistance:: " + GlobalVariables.CurrentJumpingDistance +" TargetDistance:: " + TargetDistanceMeters[GlobalVariables.DistanceTarget]);
        Debug.Log("Result:: " + val);
        iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", val, "time", 1, "easeType", iTween.EaseType.linear,"OnUpdate", "AnimateBar","OnComplete", "CheckProgress","OnUpdateTarget",this.gameObject));
    }

    void AnimateBar(float _value)
    {
        CharacterProgressBar.fillAmount = _value;
    }

    void CheckProgress()
    {
        if(GlobalVariables.CurrentJumpingDistance >= TargetDistanceMeters[GlobalVariables.DistanceTarget])
        {
            ClaimButton.interactable = true;
        }
        else
        {
            ContinueButton.SetActive(true);
            iTween.ScaleFrom(ContinueButton, iTween.Hash("scale", Vector3.zero, "time", 0.75f, "easeType", iTween.EaseType.easeOutBack));
        }
    }

    public void ClaimButtonClick()
    {
        GlobalVariables.DistanceTarget += 1;
        GlobalVariables.unlockedCharacters += 1;

        ContinueClick();
    }

    public void ContinueClick()
    {
        GlobalVariables.NextSceneToLoad = SceneNames.GameScene;
        SceneManager.LoadScene(SceneNames.Loading);
    }
}