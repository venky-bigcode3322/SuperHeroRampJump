using UnityEngine.UI;
using UnityEngine;


public class UITextObserver : Observer
{

    public Text textHolder;

    public override void OnLanguageChanged(string value)
    {
        textHolder = GetComponent<Text>();

        if (textHolder)
        {
            textHolder.text = value;
        }
        else
        {
            TextMesh textMesh = GetComponent<TextMesh>();
            textMesh.text = value;
        }
    }
}
