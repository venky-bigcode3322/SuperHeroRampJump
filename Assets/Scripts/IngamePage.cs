using UnityEngine;
using UnityEngine.UI;

public class IngamePage : PopupBase
{
    public static IngamePage Instance;

    public override AllPages CurrentPage => AllPages.Ingame;

    public override bool IsActive => gameObject.activeSelf;

    public Text DistanceText;

    public Text AirTimeText;

    public Text AirTimeScoreText;

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    private IngamePage() => Instance = this;

    private void OnEnable()
    {
        DistanceText.text = "0m";
    }
}