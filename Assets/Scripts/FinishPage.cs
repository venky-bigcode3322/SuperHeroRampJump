using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}