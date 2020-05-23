using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;
using UnityEngine.Networking;

public class SliderView_Bigcode : MonoBehaviour
{

    public Transform Content;
    public int childCount = 3;
    public GameObject ItemPrefab;
    public Transform ScrollView;

    public bool isDragging;

    public List<Transform> items;

    public int ShowingItem = 0;

    public static SliderView_Bigcode Instance;

    public CanvasScaler CanvasScaler;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < GameDataHandler.Instance.GameData.miniMoregames.Count; i++)
        {
            if (!BigCodeLibHandler_BigCode.Instance.IsGameAlreadyInstalled(GameDataHandler.Instance.GameData.miniMoregames[i]))
            {
                GameObject item = GameObject.Instantiate(ItemPrefab);
                item.GetComponent<SliderView_Item_bigcode>().MainView = ScrollView;
                item.AddComponent<BoxCollider2D>().isTrigger = true;
                item.transform.parent = Content;
                item.GetComponent<RectTransform>().localScale = Vector3.one;
                items.Add(item.transform);

                SliderMenuAdHandler.Instance.Dots[i].gameObject.SetActive(true);

                //pkgs.Add(GameDataHandler.Instance.GameData.miniMoregames[i]);

                StartCoroutine(LoadMenuAd(item,i));
            }
        }


        childCount = items.Count;

        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(childCount * CanvasScaler.referenceResolution.x, 0);



    }


    private void OnEnable()
    {
        delta.x = 100;
    }



    IEnumerator LoadMenuAd(GameObject item,int i)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture("file://" + Application.persistentDataPath + "/" + "AD" + i + ".jpg"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            Texture2D tex = DownloadHandlerTexture.GetContent(webRequest);

            item.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
        }
    }



     public Vector3 delta  = Vector3.zero;
     private Vector3 lastPos  = Vector3.zero;
    private Vector3 firstPos = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    isDragging = true;
        //}
        //else if (Input.GetKeyUp(KeyCode.Mouse0))
        //{
        //    isDragging = false;
        //}


        //if (!isDragging)
        //{
        //    // Automatic Smooth Scrolling for item change.
        //    Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.MoveTowards(Content.GetComponent<RectTransform>().anchoredPosition.x, -CanvasScaler.referenceResolution.x * ShowingItem, 100f), 0);
        //}




        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
            firstPos = Input.mousePosition;

            isDragging = true;
        }
        else if (Input.GetMouseButton(0))
        {
            delta = Input.mousePosition - lastPos;

            // Do Stuff here

            Debug.Log("delta X : " + delta.x);
            Debug.Log("delta Y : " + delta.y);

            // End do stuff


            lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {




            if (delta.x > 0)
            {
                ShowingItem--;

                if (ShowingItem < 0)
                {
                    ShowingItem = items.Count - 1;
                }

            }
            else if (delta.x < 0)
            {
                ShowingItem++;

                if (ShowingItem >= items.Count)
                {
                    ShowingItem = 0;
                }

            }

            if (Mathf.Abs(delta.x) == 0)
            {
                Vector3 tempdelta = Input.mousePosition - firstPos;

                if (tempdelta.x > 0)
                {
                    if (Mathf.Abs(tempdelta.x) > (Screen.width/4))
                    {
                        ShowingItem--;

                        if (ShowingItem < 0)
                            ShowingItem = items.Count-1;
                    }

                }
                else if (tempdelta.x < 0)
                {
                    if (Mathf.Abs(tempdelta.x) > (Screen.width / 4))
                    {

                        ShowingItem++;

                        if (ShowingItem >= items.Count)
                            ShowingItem = 0;
                    }

                }

                delta.x = 50;
            }
            if (Mathf.Abs(delta.x) < 30)
            {
                delta.x = 50;
            }
            else if (Mathf.Abs(delta.x) > 100)
            {
                delta.x = 100;

            }




            isDragging = false;
        }

        if (!isDragging)
        {
            // Automatic Smooth Scrolling for item change.
            Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.MoveTowards(Content.GetComponent<RectTransform>().anchoredPosition.x, -CanvasScaler.referenceResolution.x * ShowingItem, Mathf.Abs(delta.x)), 0);
        }


        if (ShowingItem != PreviousItem)
        {
            PreviousItem = ShowingItem;

            for (int i = 0; i < SliderMenuAdHandler.Instance.Dots.Count; i++)
            {
                if (i == ShowingItem)
                {
                    SliderMenuAdHandler.Instance.Dots[i].sprite = CheckedImage;

                }
                else
                {
                    SliderMenuAdHandler.Instance.Dots[i].sprite = UnCheckedImage;
                }
            }
        }


        Debug.Log("");

    }

    public Sprite CheckedImage, UnCheckedImage;

    int PreviousItem = -1;

    public void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(((Vector2)ScrollView.transform.InverseTransformPoint(Content.position)
            - (Vector2)ScrollView.transform.InverseTransformPoint(target.position)).x - Screen.width / 2, 0);
    }





    public void Arrow(string arrowType)
    {


        switch (arrowType)
        {
            case "Left":
                ShowingItem--;

                if (ShowingItem < 0)
                    ShowingItem = items.Count - 1;

                break;
            case "Right":
                ShowingItem++;

                if (ShowingItem >= items.Count)
                    ShowingItem = 0;

                break;
        }

       
    }

}
