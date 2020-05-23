using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectLanguageHandler : MonoBehaviour {
    public Dropdown dropDownListMultiLanguage;

    private void Awake()
    {
        dropDownListMultiLanguage.value = LanguageHandler.currentLanguageIndex;
    }

    public void SelectLanguage()
    {
//		print("Getting index :"+dropDownListMultiLanguage.captionText.text);
        LanguageHandler.Instance.SetLanguageIndex(dropDownListMultiLanguage.captionText.text);
    }
}
