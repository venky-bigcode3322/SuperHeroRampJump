using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBoxGiftPage : PopupBase
{
    public override AllPages CurrentPage => AllPages.ChestBoxGiftPage;

    public override bool IsActive => gameObject.activeSelf;

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
}
