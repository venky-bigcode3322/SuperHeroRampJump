using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderView_Item_bigcode : MonoBehaviour
{

    public Transform MainView;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(MainView.position, this.transform.position);

        if (SliderView_Bigcode.Instance.isDragging && distance < Screen.width / 2)
        {
            //SliderView_Bigcode.Instance.ShowingItem = SliderView_Bigcode.Instance.items.IndexOf(this.transform);
        }
    }


    public void DownloadNow()
    {
        Application.OpenURL("market://details?id=" + SliderMenuAdHandler.Instance.pkgs[SliderView_Bigcode.Instance.ShowingItem]);
    }

}
