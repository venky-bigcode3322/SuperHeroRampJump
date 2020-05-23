using UnityEngine;

public abstract class Observer : MonoBehaviour
{
	public string key;

	public void OnEnable()
    {
		LanguageHandler.observers.Add (this);

		if (!LanguageHandler.isSceneLoading)
        {
			OnLanguageChanged (LanguageHandler.GetCurrentLanguageText (key));
		}
	}

	void OnDisable()
    {
		LanguageHandler.observers.Remove (this);
	}

	public abstract void OnLanguageChanged (string value);
}

