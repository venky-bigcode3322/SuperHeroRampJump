using UnityEngine;
using UnityEngine.UI;

public class ImageObserver : Observer {
	public Image targetImg;
	public Sprite[] sourceSprites;
 
	public override void OnLanguageChanged(string val) {
		targetImg.sprite = sourceSprites [LanguageHandler.currentLanguageIndex];
	}
}
