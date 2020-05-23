#if UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3
#define PRE_UNITY_5_4
#endif

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Localization;

public class LanguageHandler : MonoBehaviour {

	public static int currentLanguageIndex {
        get
        {
            return PlayerPrefs.GetInt(GameConstants_BigCode.LANGUAGE_INDEX, 0);
        }
        set
        {
            PlayerPrefs.SetInt(GameConstants_BigCode.LANGUAGE_INDEX, value);
        }
    }
	public static List<Observer> observers = new List<Observer> ();

	public static bool isSceneLoading = true;

	void OnEnable() {
#if !PRE_UNITY_5_4
        SceneManager.sceneLoaded += OnSceneWasLoaded;
        SceneManager.sceneUnloaded += OnSceneWasUnloaded;
#endif
    }

	void OnDisable() {
#if !PRE_UNITY_5_4
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
#endif
    }

    private static LanguageHandler _instance;
    public static LanguageHandler Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<LanguageHandler>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
		SetLanguageIndex ();
    }

    public void SetLanguageIndex() {

        if (!PlayerPrefs.HasKey(GameConstants_BigCode.LANGUAGE_INDEX))
        {

            SetLanguageIndex(Application.systemLanguage.ToString());
        }
        else
        {

            UpdateLanguageText();

        }
    }

	public void SetLanguageIndex(string language) {
		language = language.ToUpper ();
		for (int i = 0; i < TextReader.numberOfLanguages; i++) {
			if (language.Equals (TextReader.Instance.allLanguages [i].name)) {
				currentLanguageIndex = i;
				break;
			}
		}
		UpdateLanguageText ();
	}

	public void SetLanguageIndex(int indx){
		currentLanguageIndex = indx;
		UpdateLanguageText ();
	}

	public static string GetCurrentLanguageText(string key)
	{
       
		return TextReader.Instance.allLanguages [currentLanguageIndex].ValueOfKey (key);
	}

	void OnLanguageChanged() {
		UpdateLanguageText ();
	}

	void UpdateLanguageText()
    {
		for (int i = 0; i < observers.Count; i++)
        {
			observers [i].OnLanguageChanged (GetCurrentLanguageText(observers[i].key));
		}
	}

	void OnSceneWasLoaded(Scene scene, LoadSceneMode sceneMode) {
		UpdateLanguageText ();
		isSceneLoading = false;
	}

	void OnSceneWasUnloaded(Scene scene) {
		isSceneLoading = true;
	}
#if PRE_UNITY_5_4
    private int currentUnloadedScene = -1;
    private void OnLevelWasLoaded(int level)
    {
        if(currentUnloadedScene >= 0)
        {
            OnSceneWasUnloaded(SceneManager.GetSceneAt(currentUnloadedScene));
        }
        currentUnloadedScene = level;
        OnSceneWasLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
#endif
}



