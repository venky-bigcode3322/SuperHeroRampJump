using UnityEngine;
using UnityEngine.UI;

public class ChestBoxGiftPage : MonoBehaviour
{
    [SerializeField] Image giftIcon;

    [SerializeField] Text giftAmount;

    [SerializeField] Sprite[] giftIconSprites;//1 - coins 0 - diamonds

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetDetails(int iconIndex,int Amount)
    {
        giftIcon.sprite = giftIconSprites[iconIndex];

        if(iconIndex == 0)
            giftAmount.text = "x" + Amount + " Diamonds";
        else
            giftAmount.text = "x" + Amount + " Coins";

        Open();
    }

    public void Claim()
    {
        Close();
    }
}