using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGamePage : MonoBehaviour {

	public Image exitTexture;
	// Use this for initialization
	void Start () {

        exitTexture.sprite = Sprite.Create(PluginManager.Instance.gameExitTexture,
            new Rect(0, 0, PluginManager.Instance.gameExitTexture.width, PluginManager.Instance.gameExitTexture.height), Vector2.zero);
    }

    private CallMethod callMethod;

    private void OnEnable()
    {

        //if ((callMethod == null || BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count == 0) || (BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count > 0 && !BigCodeLibHandler_BigCode.Instance.StackManager.pages[BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count - 1].Equals(callMethod)))
        //{
        //    //Back Button Reference
        //    callMethod = new CallMethod();
        //    callMethod.fun = cancleQuit;
        //    callMethod.MethodType = MethodType.NONPARAMETERIZED;
        //    //callMethod.parameter = "BTN_BACK";
        //    BigCodeLibHandler_BigCode.Instance.StackManager.pages.Add(callMethod);
        //}
    }

    public void onExitPage()
	{
		Application.Quit ();
	}
	public void cancleQuit()
	{


        if (SliderMenuAdHandler.Instance)
            SliderMenuAdHandler.Instance.ShowMenuAdIcon();

        //Should excute first
        if (BigCodeLibHandler_BigCode.Instance)
            BigCodeLibHandler_BigCode.Instance.StackManager.pages.RemoveAt(BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count - 1);

        gameObject.SetActive(false);
	}
	public void openExitgameURL ()
    {
        Application.OpenURL("market://details?id=" + ServerDataHandler.Instance.BaseData.ExitGame.gamePackage);

	}
}
