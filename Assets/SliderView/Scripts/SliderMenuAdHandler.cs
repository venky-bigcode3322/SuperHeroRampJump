using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SliderMenuAdHandler : MonoBehaviour
{

    public GameObject SliderMenuAd;
    public GameObject SliderMenuAdIcon;

    public static SliderMenuAdHandler Instance;

    public int iconIndex;

    public List<string> pkgs;

    public List<Image> Dots;


    private void Awake()
    {
        Instance = this;
    }




    public void ShowMenuAdIcon()
    {
        if (PluginManager.Instance.isInternetAvailable && ServerDataHandler.Instance.CommonData.MiniMoreGames.isEnabled)
        {
            if (GameDataHandler.Instance.GameData.miniMoregames != null)
            {
                if (GameDataHandler.Instance.GameData.miniMoregamesVersion == ServerDataHandler.Instance.CommonData.MiniMoreGames.version)
                {
                    for (int i = 0; i < GameDataHandler.Instance.GameData.miniMoregames.Count; i++)
                    {
                        if (!BigCodeLibHandler_BigCode.Instance.IsGameAlreadyInstalled(GameDataHandler.Instance.GameData.miniMoregames[i]))
                        {
                            if(!pkgs.Contains(GameDataHandler.Instance.GameData.miniMoregames[i]))
                            pkgs.Add(GameDataHandler.Instance.GameData.miniMoregames[i]);
                        }
                    }

                    StartCoroutine(LoadIcon(SliderMenuAdIcon));
                }
            }
        }
        else
        {
            if (GameDataHandler.Instance.GameData.miniMoregames != null && GameDataHandler.Instance.GameData.miniMoregames.Count > 0)
            {


                for (int i = 0; i < GameDataHandler.Instance.GameData.miniMoregames.Count; i++)
                {
                    if (!BigCodeLibHandler_BigCode.Instance.IsGameAlreadyInstalled(GameDataHandler.Instance.GameData.miniMoregames[i]))
                    {
                        if (!pkgs.Contains(GameDataHandler.Instance.GameData.miniMoregames[i]))
                            pkgs.Add(GameDataHandler.Instance.GameData.miniMoregames[i]);
                    }
                }

                StartCoroutine(LoadIcon(SliderMenuAdIcon));
            }
        }
    }

    // Start is called before the first frame update
    //void Start()
    public void ShowMenuAdSlider()
    {
        if (PluginManager.Instance.isInternetAvailable && ServerDataHandler.Instance.CommonData.MiniMoreGames.isEnabled)
        {
            if (GameDataHandler.Instance.GameData.miniMoregames != null)
            {
                if (GameDataHandler.Instance.GameData.miniMoregamesVersion == ServerDataHandler.Instance.CommonData.MiniMoreGames.version)
                {
                    SliderMenuAd.SetActive(true);

                }
            }
        }
        else
        {
            if (GameDataHandler.Instance.GameData.miniMoregames != null && GameDataHandler.Instance.GameData.miniMoregames.Count > 0)
            {
                SliderMenuAd.SetActive(true);
            }
        }





    }

    IEnumerator LoadIcon(GameObject SliderMenuAdIcon)
    {
        iconIndex = Random.Range(0, pkgs.Count);

        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture("file://" + Application.persistentDataPath + "/" + "AD" + GameDataHandler.Instance.GameData.miniMoregames.IndexOf(pkgs[iconIndex]) + "_I.png"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            Texture2D tex = DownloadHandlerTexture.GetContent(webRequest);
            SliderMenuAdIcon.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
            SliderMenuAdIcon.transform.parent.gameObject.SetActive(true);
            SliderMenuAdIcon.transform.gameObject.SetActive(true);

            if (SubscriptionHandler.Instance && SubscriptionHandler.Instance.isShowing)
            {
                ShowMoreGamesSideOff();
            }
        }

        yield return null;
    }

    public void OnSliderAdClose()
    {
        //Should excute first
        if (BigCodeLibHandler_BigCode.Instance)
            BigCodeLibHandler_BigCode.Instance.StackManager.pages.RemoveAt(BigCodeLibHandler_BigCode.Instance.StackManager.pages.Count - 1);

        SliderMenuAd.SetActive(false);
        StartCoroutine(LoadIcon(SliderMenuAdIcon));
    }


    public void OnIconClick()
    {

        SliderMenuAd.SetActive(true);

        SliderView_Bigcode.Instance.isDragging = true;
        SliderView_Bigcode.Instance.ShowingItem = iconIndex;
        SliderView_Bigcode.Instance.isDragging = false;



        //Back Button reference
        CallMethod callMethod = new CallMethod();
        callMethod.fun = OnSliderAdClose;
        callMethod.MethodType = MethodType.NONPARAMETERIZED;
        BigCodeLibHandler_BigCode.Instance.StackManager.pages.Add(callMethod);

    }

    public void ShowMoreGamesSideOff()
    {
        SliderMenuAdIcon.transform.parent.gameObject.SetActive(false);
        SliderMenuAdIcon.transform.gameObject.SetActive(false);
    }
}
