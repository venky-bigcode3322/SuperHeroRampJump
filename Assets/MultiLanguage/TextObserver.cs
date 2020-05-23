using UnityEngine;

public class TextObserver : Observer {

	public TextMesh textHolder;

	public override void OnLanguageChanged (string value)
	{
		textHolder.text = value;
	}
}