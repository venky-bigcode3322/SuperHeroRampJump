using UnityEngine;

public class SpriteeObserver : Observer {
	public SpriteRenderer targetImg;
	public Sprite[] sourceSprites;
 
	public override void OnLanguageChanged(string val) {
		targetImg.sprite = sourceSprites [LanguageHandler.currentLanguageIndex];
	}
}
