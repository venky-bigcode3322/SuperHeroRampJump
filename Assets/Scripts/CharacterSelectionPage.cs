using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionPage : PopupBase
{
    [SerializeField] Text CoinsText;
    [SerializeField] Text DiamondsText;

    public override AllPages CurrentPage => AllPages.CharacterSelection;

    public override bool IsActive => gameObject.activeSelf;

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

    private void OnEnable()
    {
        GlobalVariables.CoinsUpdateEvent += UpdateCoins;
        GlobalVariables.DiamondUpdateEvent += UpdateDiamonds;

        UpdateCoins(GlobalVariables.GameCoins);
        UpdateDiamonds(GlobalVariables.GameDiamonds);

        SetPageDetails();

        StartCoroutine(AnimatePage(0));
    }

    private void OnDisable()
    {
        GlobalVariables.CoinsUpdateEvent -= UpdateCoins;
        GlobalVariables.DiamondUpdateEvent -= UpdateDiamonds;
    }

    [SerializeField] private GameObject[] SuperHeroObjs;

    [SerializeField] private Button[] SelectButtons;
    
    void SetPageDetails()
    {
        for (int i = 0; i < SuperHeroObjs.Length; i++)
            SuperHeroObjs[i].SetActive(false);

        SuperHeroObjs[GlobalVariables.selectedCharacter].SetActive(true);

        for (int i = 0; i < SelectButtons.Length; i++)
        {
            if(i < GlobalVariables.unlockedCharacters)
            {
                SelectButtons[i].interactable = true;
            }
            else
            {
                SelectButtons[i].interactable = false;
            }
        }
    }

    public void SelectCharacter(int index)
    {
        GlobalVariables.selectedCharacter = index;
        SetPageDetails();
        if (BikeController.instance) BikeController.instance.ActivateSuperHeroCharacter();
    }

    public void BackButton()
    {
        if (UiHandler.Instance) UiHandler.Instance.CheckAndClosePopup();
    }

    void UpdateCoins(int coins)
    {
        CoinsText.text = coins.ToString();
    }

    void UpdateDiamonds(int diamonds)
    {
        DiamondsText.text = diamonds.ToString();
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

            iTween.ScaleFrom(AllButtons[i], iTween.Hash("y", 0, "time", 0.75f, "isLocal", true, "easeType", iTween.EaseType.easeOutBack));
           // iTween.RotateFrom(AllButtons[i], iTween.Hash("z", 45, "time", 0.65f, "isLocal", true, "easeType", iTween.EaseType.easeOutBack));

            yield return new WaitForSeconds(0.15f);
        }
    }
}