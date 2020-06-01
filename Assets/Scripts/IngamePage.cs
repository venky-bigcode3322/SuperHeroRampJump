using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IngamePage : PopupBase
{
    public static IngamePage Instance;

    public override AllPages CurrentPage => AllPages.Ingame;

    public override bool IsActive => gameObject.activeSelf;

    public Text DistanceText;

    public Text AirTimeText;

    public Text AirTimeScoreText;

    [SerializeField] GameObject[] AllButtons;

    private Vector3[] DefaultButtonPositions;

    private void Awake()
    {
        DefaultButtonPositions = new Vector3[AllButtons.Length];

        for (int i = 0; i < DefaultButtonPositions.Length; i++)
            DefaultButtonPositions[i] = AllButtons[i].transform.localPosition;
    }

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
        StartCoroutine(AnimatePage(0));
    }

    private Vector3 dir = Vector3.zero;

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
            dir = AllButtons[i].transform.localPosition;

            if (i < 1)
            {
                dir.x = 1000;
            }
            else
            {
                dir.y = 1000;
            }

            iTween.MoveFrom(AllButtons[i], iTween.Hash("position", dir, "time", 0.75f, "isLocal", true, "easeType", iTween.EaseType.easeOutBack));

            yield return new WaitForSeconds(0.15f);
        }
    }
}