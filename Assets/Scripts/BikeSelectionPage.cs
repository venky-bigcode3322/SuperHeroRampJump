using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeSelectionPage : PopupBase
{
    public override AllPages CurrentPage => AllPages.BikeSelection;

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