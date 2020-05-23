using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickRate : MonoBehaviour {


    Button button;


    private void Awake()
    {
        button = this.GetComponent<Button>();
    }

    // Use this for initialization
    void Start () {


        //Star On Click Listner
        button.onClick.AddListener(()=> 
        {

            //if (int.Parse(this.gameObject.name) + 1 > RatePopUp_Bigcode.Instance.selectedStars)
            {

                RatePopUp_Bigcode.Instance.selectedStars = int.Parse(this.gameObject.name) + 1;

                if (RatePopUp_Bigcode.Instance.selectedStars <= 3)
                {
                    RatePopUp_Bigcode.Instance.EnableThankYou();
                }
                else
                {
                    RatePopUp_Bigcode.Instance.EnableRate();
                }

                // Disable All Stars
                foreach (Transform star in RatePopUp_Bigcode.Instance.Stars)
                {
                    star.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }

                //Enable Selcted stars
                for (int i = 0; i < RatePopUp_Bigcode.Instance.selectedStars; i++)
                {
                    RatePopUp_Bigcode.Instance.Stars[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
            }


        });
        
    }

}
