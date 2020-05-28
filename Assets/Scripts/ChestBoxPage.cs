using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestBoxPage : PopupBase
{
    [System.Serializable]
    private struct ChestBoxProperty
    {
        [SerializeField] string Name;
        public GameObject ChestBoxLocked;
        public GameObject ChestBoxUnLocked;
        public Image Icon;
        public Text QuantityText;
        public int Quanitity;
    }

    public override AllPages CurrentPage => AllPages.ChestBoxPage;

    public override bool IsActive => gameObject.activeSelf;

    [SerializeField] Sprite[] IconSprites;

    [SerializeField] Image[] KeyImages;

    [SerializeField] Sprite[] KeyActiveInactiveImage; // 0=InActive , 1=Active

    [SerializeField] ChestBoxProperty[] ChestBoxProperties;

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
        showKeys();
        ArrangeGiftsInChestBoxs();
    }

    private int[] randomeNumbers = new int[] {0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0};

    void ArrangeGiftsInChestBoxs()
    {
        for (int i = 0; i < ChestBoxProperties.Length; i++)
        {
            ChestBoxProperties[i].ChestBoxUnLocked.SetActive(false);
            ChestBoxProperties[i].ChestBoxLocked.SetActive(true);
            var randomeNumber = randomeNumbers[Random.Range(0, randomeNumbers.Length)];
            ChestBoxProperties[i].Icon.sprite = IconSprites[randomeNumber];
            if(randomeNumber == 0)
            {
                ChestBoxProperties[i].Quanitity = Random.Range(1, 10);
            }
            else
            {
                ChestBoxProperties[i].Quanitity = Random.Range(50, 250);
            }
            ChestBoxProperties[i].QuantityText.text = "x"+ChestBoxProperties[i].Quanitity;
        }
    }

    void showKeys()
    {
        for (int i = 0; i < KeyImages.Length; i++)
        {
            if(i < GlobalVariables.CollectedKeys)
            {
                KeyImages[i].sprite = KeyActiveInactiveImage[1];
            }
            else
            {
                KeyImages[i].sprite = KeyActiveInactiveImage[0];
            }
        }
    }

    public void OpenBox(int index)
    {
        if (GlobalVariables.CollectedKeys > 0)
        {
            GlobalVariables.CollectedKeys -= 1;
            ChestBoxProperties[index].ChestBoxUnLocked.SetActive(true);
            ChestBoxProperties[index].ChestBoxLocked.SetActive(false);
            if (index == 0)
            {
                GlobalVariables.AddDiamonds(ChestBoxProperties[index].Quanitity);
            }
            else
            {
                GlobalVariables.AddCoins(ChestBoxProperties[index].Quanitity);
            }
            showKeys();
        }
    }
}