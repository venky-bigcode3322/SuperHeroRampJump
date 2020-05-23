using UnityEngine.UI;
using UnityEngine;


public class UITextObserver3D : Observer {

	//public text textHolder;
    public TextMesh textHolder;

    public override void OnLanguageChanged (string value)
	{
        textHolder.text = value;
	}
}
