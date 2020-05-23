using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ToastPopupScript : MonoBehaviour {


    public static ToastPopupScript Instance;

    private RectTransform rectTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.transform.parent.gameObject);
        }
        else
        {
            Destroy(this.transform.parent.gameObject);
        }
    }



    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ShowToast()
    {
        



        if (PluginManager.Instance.toastPopUp != null)
        {
            rectTransform.DOAnchorPosY(85f, 0.5f, false).SetDelay(0.5f).SetEase(Ease.OutBack);
            rectTransform.DOAnchorPosY(-64f, 0.5f, false).SetDelay(2f).SetEase(Ease.InBack);

            //iTween.MoveTo(transform.gameObject, iTween.Hash("y", 200f, "easetype", iTween.EaseType.easeOutBack, "time", 0.5f));
            //iTween.MoveTo(transform.gameObject, iTween.Hash("y", -250, "easetype", iTween.EaseType.easeInBack, "time", 0.5f, "delay", 2));

            //Invoke("hidetoast", 2.5f);
        }
    }


    void hidetoast()
	{
		gameObject.SetActive (false);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
