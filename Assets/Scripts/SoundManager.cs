using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource BG_AudioSource;
    [SerializeField] AudioSource ButtonSource;

    [Tooltip("0 = Menu, 1 = GamePlay")]
    [SerializeField] AudioClip[] BGSoundClips;

    [SerializeField] AudioMixerGroup MusicGroup;
    [SerializeField] AudioMixerGroup SoundGroup;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CheckMusicAndSoundStatus();
        BGVolume = 1;
        StartCoroutine(PlayBG(MusicBG.MenuBG));
    }


    private bool BgChanging = false;
    public IEnumerator PlayBG(MusicBG musicBG,bool isLoop = true)
    {
        if (BG_AudioSource)
        {
            BgChanging = true;
            float startVolume = BG_AudioSource.volume;

            while(BG_AudioSource.volume > 0)
            {
                BG_AudioSource.volume -= startVolume * Time.deltaTime * 2;
                yield return null;
            }
            BG_AudioSource.Stop();
            BG_AudioSource.clip = BGSoundClips[(int)musicBG];
            if (BG_AudioSource.clip)
            BG_AudioSource.Play();

            if (!isLoop)
                BG_AudioSource.loop = false;
            else
                BG_AudioSource.loop = true;

            while (BG_AudioSource.volume <  startVolume)
            {
                BG_AudioSource.volume += startVolume * Time.deltaTime * 2;
                yield return null;
            }
            BgChanging = false;
        }
    }

    public float BGVolume = 1;

    private void Update()
    {
        if (!BgChanging)
        {
            BG_AudioSource.volume = Mathf.Lerp(BG_AudioSource.volume, BGVolume, Time.deltaTime * 4f);
        }
    }

    public void PlayButtonSound()
    {
        if (ButtonSource && ButtonSource.clip) ButtonSource.Play();
    }

    public void CheckMusicAndSoundStatus()
    {
        float soundVolume = 0;
        SoundGroup.audioMixer.GetFloat("SoundVolume",out soundVolume);

        float musicVolume = 0;
        MusicGroup.audioMixer.GetFloat("MusicVolume", out musicVolume);

        if (GlobalVariables.SoundState)
            iTween.ValueTo(gameObject, iTween.Hash("from", soundVolume, "to", 0, "time", 0.5f, "OnUpdate", "SoundVolume"));
        else
            iTween.ValueTo(gameObject, iTween.Hash("from", soundVolume, "to", -80, "time", 0.5f, "OnUpdate", "SoundVolume"));

        if (GlobalVariables.MusicState)
            iTween.ValueTo(gameObject, iTween.Hash("from", musicVolume, "to", 0, "time", 0.5f, "OnUpdate", "MusicVolume"));
        else
            iTween.ValueTo(gameObject, iTween.Hash("from", musicVolume, "to", -80, "time", 0.5f, "OnUpdate", "MusicVolume"));
    }

    void MusicVolume(float value)
    {
        MusicGroup.audioMixer.SetFloat("MusicVolume", value);

    }

    void SoundVolume(float value)
    {
        SoundGroup.audioMixer.SetFloat("SoundVolume",value);
    }
}