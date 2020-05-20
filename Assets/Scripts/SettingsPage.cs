﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}