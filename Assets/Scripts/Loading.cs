using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    AsyncOperation _async;
    public Text LoadingText;
    public Image BarImage;

    void Start()
    {
        BarImage.fillAmount = 0;
        LoadingText.text = "Loading 0 %";
        RequestToLoadNextScene();
    }

    void RequestToLoadNextScene()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        if (GlobalVariables.NextSceneToLoad == "")
            yield break;
		
        _async = SceneManager.LoadSceneAsync(GlobalVariables.NextSceneToLoad);
        yield return _async;
    }
    
    void Update()
    {
        if (_async != null)
        {
            BarImage.fillAmount = _async.progress + 0.1f;
            LoadingText.text = "Loading " + (int)((_async.progress + 0.1f) * 100) + " %";
        }
    }
}
