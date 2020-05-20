using UnityEngine;

public class IngamePage : PopupBase
{
    public override AllPages CurrentPage => AllPages.Ingame;

    public override bool IsActive => gameObject.activeSelf;

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }
}