using UnityEngine;
using UnityEngine.UI;

public class SettingsPage : PopupBase
{
    public override AllPages CurrentPage => AllPages.Settings;

    public override bool IsActive => gameObject.activeSelf;

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    [SerializeField] Image SoundImage;
    [SerializeField] Image MusicImage;

    [SerializeField] Text SoundText;
    [SerializeField] Text MusicText;

    [SerializeField] Sprite[] ButtonOnAndOffSprite; // 0 - Off 1 - On

    public void SoundButton()
    {
        if (GlobalVariables.SoundState)
        {
            GlobalVariables.SoundState = false;
            SoundImage.sprite = ButtonOnAndOffSprite[0];
            SoundText.text = "Off";
        }
        else
        {
            GlobalVariables.SoundState = true;
            SoundImage.sprite = ButtonOnAndOffSprite[1];
            SoundText.text = "On";
        }
        CheckButtonsHUD();
    }

    public void MusicButton()
    {
        if (GlobalVariables.MusicState)
        {
            GlobalVariables.MusicState = false;
        }
        else
        {
            GlobalVariables.MusicState = true;
        }

        CheckButtonsHUD();
    }

    private void OnEnable() => CheckButtonsHUD();

    void CheckButtonsHUD()
    {
        if (!GlobalVariables.MusicState)
        {
            MusicImage.sprite = ButtonOnAndOffSprite[0];
            MusicText.text = "Off";
        }
        else
        {
            MusicImage.sprite = ButtonOnAndOffSprite[1];
            MusicText.text = "On";
        }

        if (!GlobalVariables.SoundState)
        {
            SoundImage.sprite = ButtonOnAndOffSprite[0];
            SoundText.text = "Off";
        }
        else
        {
            SoundImage.sprite = ButtonOnAndOffSprite[1];
            SoundText.text = "On";
        }
    }

    public void Back()
    {
        if (UiHandler.Instance) UiHandler.Instance.CheckAndClosePopup();
    }
}