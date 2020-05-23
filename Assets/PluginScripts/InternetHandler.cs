using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InternetHandler : MonoBehaviour {

    public static InternetHandler Instance;

    float timeOut = 10;
    float timer;
    bool failed = false;

    private void Awake()
    {
        Instance = this;
    }


    public IEnumerator CheckInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("https://www.google.com");

        while (!www.isDone)
        {
            if (timer > timeOut)
            {
                failed = true;

                break;
            }

            timer += Time.deltaTime;
            yield return null;
        }


        if (www.error != null || failed)
        {
            action(false);
        }
        else if (!failed)
        {
            action(true);
        }

        if (failed) www.Dispose();
    }
}
