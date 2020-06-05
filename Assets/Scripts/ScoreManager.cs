using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public CanvasGroup CG;

    public Text ScoreText;

    public RectTransform textHolder;

    public Queue<string> ScoreQueue = new Queue<string>();

    private bool isAnimationPlaying = false;

    private void Awake()
    {
        Instance = this;
    }

    public void AddToQueue(string str)
    {
        ScoreQueue.Enqueue(str);
    }

    public void ExecuteQueue()
    {
        var value = ScoreQueue.Dequeue();
        ScoreText.text = value;

        StartCoroutine(AnimateText());
    }

    private void Update()
    {
        if (isAnimationPlaying || ScoreQueue.Count <= 0)
            return;

        ExecuteQueue();
    }

    public IEnumerator AnimateText()
    {
        isAnimationPlaying = true;
        textHolder.anchoredPosition = new Vector2(-Screen.width, 210);
        iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", 0.3f, "OnUpdate", "CanvasGroupAnim"));
        iTween.MoveTo(textHolder.gameObject, iTween.Hash("x", 0,"islocal",true, "time", 0.3f));
        yield return new WaitForSeconds(1.3f);
        iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", 0, "time", 0.3f, "OnUpdate", "CanvasGroupAnim"));
        iTween.MoveTo(textHolder.gameObject, iTween.Hash("x", Screen.width, "islocal", true, "time", 0.3f));
        yield return new WaitForSeconds(1.3f);
        isAnimationPlaying = false;
    }

    void CanvasGroupAnim(float value)
    {
        CG.alpha = value;
    }
}
