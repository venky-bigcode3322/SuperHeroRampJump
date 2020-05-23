using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelection : MonoBehaviour {

	// Use this for initialization
	public Transform Board;
	public Sprite normaltx,hilighttx;


    private CallMethod callMethod;

    void OnEnable ()
    {
		//iTween.ScaleFrom(transform.gameObject,iTween.Hash("x",0.5f,"y",0.5f,"easetype",iTween.EaseType.easeOutBack,"time",0.5f));

		HighLightButton();


        if ((callMethod == null|| BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count == 0) || (BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count > 0 && !BigCodeLibHandler_BigCode.Instance.StackManager.pages[BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count - 1].Equals(callMethod)))
        {
            //Back Button Reference
            callMethod = new CallMethod();
            callMethod.fun = OnCloseClick;
            callMethod.MethodType = MethodType.NONPARAMETERIZED;
            //callMethod.parameter = "BTN_BACK";
            BigCodeLibHandler_BigCode.Instance.StackManager.pages.Add(callMethod);
        }


    }

	void HighLightButton()
	{
		for (int i = 0; i < Board.childCount; i++) { // 0 indx bg
			if (i != LanguageHandler.currentLanguageIndex) {
				Board.GetChild (i).GetComponent<Image> ().sprite = normaltx;  //.localScale = new Vector3 (1, 1, 1);
			} else {
				Board.GetChild (i).GetComponent<Image> ().sprite = hilighttx;//.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
			}
		}

	}

	public void OnLanguageSelect(int indx){
		LanguageHandler.Instance.SetLanguageIndex(indx);
		HighLightButton ();

        BridgeManager_Bigcode.Instance.OnSignInEvent(GooglePlayGamesHandler.Instance.isSignIn);

    }



    public void OnCloseClick()
    {
        //Should excute first
        if (BigCodeLibHandler_BigCode.Instance)
            BigCodeLibHandler_BigCode.Instance.StackManager.pages.RemoveAt(BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count - 1);

        this.gameObject.SetActive(false);

        if(SliderMenuAdHandler.Instance)
        SliderMenuAdHandler.Instance.ShowMoreGamesSideOff();



    }





}
